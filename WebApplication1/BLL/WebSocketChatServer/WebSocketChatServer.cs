//-----------------------------------------------------------------------------------
// <copyright file="WebSocketChatServer.cs" company="Hochschule Bremen">
//     Copyright (c) 2012 Steffen Schuette. All rights reserved.
// </copyright>
// <author>Steffen Schuette</author><email>steffen.schuette@web.de</email>
// <version>0.2.0-beta</version>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Fleck;
using Newtonsoft.Json;
using WebApplication1.Utils;
using WebApplication1.Models;
using WebApplication1.DAL;

namespace WebApplication1.BLL.WebSocketChatServer
{
    /// <summary>
    /// Simple WebSocket Chat Server example using the Fleck C# WebSocket implementation. 
    /// This example also utilises the Newtonsoft.Json library to build JSON objects.
    /// </summary>
    public class WebSocketChatServer
    {
        /// <summary>
        /// Stores the connection parameters localhost and port used for the WebSocketChatServer.
        /// </summary>
        private string[] args;

        /// <summary>
        /// Hostname to bind the server socket to.
        /// </summary>
        private string hostname = string.Empty;

        /// <summary>
        /// Port to bind the server socket to.
        /// </summary>
        private int port = 0;

        /// <summary>
        /// Dictionary with key Fleck.IWebSocketConnection and the associated client user name.
        /// </summary>
        private Dictionary<Fleck.IWebSocketConnection, string> connectedSockets =
            new Dictionary<IWebSocketConnection, string>();

        /// <summary>
        /// Fleck WebSocketServer object.
        /// </summary>
        private Fleck.WebSocketServer server = null;

        /// <summary>
        /// Sets the connection information how to open the WebSocket.
        /// </summary>
        private string connectionInfo = string.Empty;

        /// <summary>
        /// Variable for the server console input.
        /// </summary>
        private string consoleInput = string.Empty;

        private readonly DataContext _db = new DataContext();
        private readonly GenericRepository<Client> clientRep = Reps.Clients;
        private readonly GenericRepository<PrivateConsultant> privateRep = Reps.Privates;
        private readonly GenericRepository<JuridicConsultant> juridicRep = Reps.Juridics;
        private readonly GenericRepository<Order> orderRep = Reps.Orders;
        private readonly GenericRepository<ChatActivity> chatActivityRep = Reps.ChatActivities;
        private readonly GenericRepository<ChatMsg> chatMsgRep = Reps.ChatMsgs;

        private long LastMessageId { get { return chatMsgRep.Get()
                                                            .OrderByDescending(x => x.DateTime)
                                                            .First()
                                                            .Id; } }

        private const int HISTORY_LIMIT = 5;

        /// <summary>
        /// Initializes a new instance of the WebSocketChatServer class. It takes the parameter localhost and port.
        /// </summary>
        /// <param name="args">Arguments containing the localhost and port.</param>
        public WebSocketChatServer(string[] args)
        {
            this.args = args;

            // Set the fleck logging level
            FleckLog.Level = LogLevel.Info;
        }

        public WebSocketChatServer()
        {
        }

        /// <summary>
        /// Method starting the WebSocket listening to the defined hostname and port. It's waiting
        /// for incoming client connections.
        /// </summary>
        public void Start()
        {
            // Check given arguments
            if (this.args.Length < 2 || this.args.Length > 2)
            {
                Console.WriteLine("Illegal arguments. Usage: <hostname> <port>");
                Console.WriteLine("Press <RETURN> to close WebSocket server...");

                // Wait until key is pressed
                Console.ReadLine();

                throw new ArgumentException("Illegal arguments. Usage: <hostname> <port>");
            }

            try
            {
                this.hostname = this.args[0];

                this.port = int.Parse(this.args[1]);

                this.connectionInfo = "ws://" + this.hostname + ":" + this.port;

                // Instatiate WebSocketServer object
                this.server = new Fleck.WebSocketServer(this.connectionInfo);

                // Open the WebSocket on the given hostname and port
                this.server.Start(socket =>
                {
                    // Method called when a client connects
                    socket.OnOpen = () => this.OnOpen(socket);

                    // Method called when a client sends a message
                    socket.OnMessage = message => this.OnMessage(socket, message);
                    //socket.OnMessage = json => this.OnMessage(socket, json);
                    
                    // Method called when an error occurs
                    socket.OnError = message => this.OnError(socket, message);

                    // Method called when a client disconnects
                    socket.OnClose = () => this.OnClose(socket);
                });
            }
            catch (Exception e)
            {
                FleckLog.Error(e.ToString());
            }
        }

        /// <summary>
        /// Method called when the WebSocket receives a message.
        /// </summary>
        /// <param name="socket">The client socket the message comes from.</param>
        /// <param name="message">The client message.</param>
        private void OnMessage(IWebSocketConnection socket, string message)
        {
            Guid socketId = socket.ConnectionInfo.Id;
            if (IsConnectRequest(message))
            {
                dynamic obj = new JavaScriptSerializer().Deserialize<dynamic>(message);
                ChatActivity newChattee = new ChatActivity();
                newChattee.ConnecteeId = obj["connecteeId"];
                newChattee.OrderId = obj["orderId"];
                newChattee.SocketId = socketId;
                chatActivityRep.CreateAsync(newChattee);
            }
            // Timestamp containing the current time with the pattern: 2012-07-06 19:04:23
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Get user id from receivedJSON object
            string uid = string.Empty;

            // The chat message object with the ts (timestamp), uid (user id) and msg (message) properties.
            ChatMessage chatMessage = new ChatMessage();

            try
            {
                if (this.connectedSockets.ContainsKey(socket))
                {
                    // Get the client user name
                    uid = this.connectedSockets[socket];

                    // Build the JSON object which will be send to all connected clients.
                    // It contains the current timestamp, the user name the message is 
                    // came from and finally the message itself.
                    chatMessage.ts = timestamp;
                    chatMessage.uid = uid;

                    AddMessage(GetOrderId(socketId), socketId, message);

                    FleckLog.Info("Msg rcv: " + uid + " @ " + timestamp + " => " + message);
                    chatMessage.msg = new StringBuilder("{").
                                      Append("Id: " + LastMessageId).Append(",").
                                      Append("Text: " + message).Append(",").
                                      Append("Image: xxx.jpg").Append(",").
                                      Append("SenderName: " + GetSenderName(socketId)).Append(",").
                                      Append("SenderId: " + GetSenderId(socketId)).Append(",").
                                      Append("Date: " + ServiceUtil.DateTimeToUnixTimestamp(GetDate())).Append("}").
                                      ToString();

                    foreach (var socketId_ in GetReceiversSocketIds(socketId))
                    {
                        KeyValuePair<IWebSocketConnection, string> client = connectedSockets.SingleOrDefault(x => x.Key.ConnectionInfo.Id == socketId_);
                        client.Key.Send(JsonConvert.SerializeObject(chatMessage));
                    }
                }
                else
                {
                    // First message from the socket => message contains the client user name.
                    // Check now if user name is available. If not, add socket to the connected 
                    // sockets containing its user name.
                    if (!this.connectedSockets.ContainsValue(message))
                    {
                        // Store new connected client with its send user name to the connected sockets.
                        this.connectedSockets.Add(socket, message);
                        FleckLog.Info("Client <" + socket.ConnectionInfo.ClientIpAddress + "> set user name to <" + message + ">");
                    }
                    else
                    {
                        // Send client that the user name is already in use. The server now has
                        // to close the WebSocket to the client
                        chatMessage.ts = timestamp;
                        chatMessage.uid = message;
                        chatMessage.msg = "Error: the user name <" + message + "> is already in use!";

                        // Serialise ChatMessage object to JSON
                        socket.Send(JsonConvert.SerializeObject(chatMessage));
                        socket.Close();

                        // If socket is stored in connected sockets list, remove it
                        if (this.connectedSockets.ContainsKey(socket))
                        {
                            this.connectedSockets.Remove(socket);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // WebSocket could not be bind. E.g. if the socket is already in use.
                FleckLog.Error("Error opening WebSocket on <" + this.connectionInfo + ">. WebSocket maybe in use?");
                FleckLog.Error("Exception string: \n" + e.ToString());

                // Wait until key is pressed
                Console.ReadLine();

                // Close application
                System.Environment.Exit(-1);
            }
        }

        private bool IsConnectRequest(string message)
        {
            return message.Contains("{") && 
                   message.Contains("connecteeId") && 
                   message.Contains("orderId") && 
                   message.Contains("}");
        }

        private IEnumerable<Guid> GetReceiversSocketIds(Guid socketId)
        {
            long orderId = chatActivityRep.Get().SingleOrDefault(x => x.SocketId == socketId).OrderId;
            return chatActivityRep.Get().Where(x => x.OrderId == orderId).Select(x => x.SocketId);
        }

        private long GetSenderId(Guid socketId)
        {
            return chatActivityRep.Get().SingleOrDefault(x => x.SocketId == socketId).ConnecteeId;
        }

        private long GetOrderId(Guid socketId)
        {
            return chatActivityRep.Get().SingleOrDefault(x => x.SocketId == socketId).OrderId;
        } 

        private void AddMessage(long orderId, Guid socketId, string text)
        {
            ChatMsg msg = new ChatMsg();
            msg.OrderId = orderId;
            msg.SenderId = GetSenderId(socketId);
            msg.SenderName = GetSenderName(socketId);
            msg.Text = text;
            msg.DateTime = GetDate();
            msg.Image = "xxx.jpg";
            chatMsgRep.CreateAsync(msg);
        }

        private void RemoveConnectee(Guid socketId)
        {
            ChatActivity connectee = _db.ChatActivity.SingleOrDefault(x => x.SocketId == socketId);
            _db.ChatActivity.Remove(connectee);
            _db.SaveChanges();
            //ChatActivity connectee = chatActivityRep.Get().SingleOrDefault(x => x.SocketId == socketId);
            //chatActivityRep.Delete(connectee);
        }

        private DateTime GetDate()
        {
            return DateTime.Now;
        }

        private string GetSenderName(Guid socketId)
        {
            long senderId = chatActivityRep.Get().SingleOrDefault(x => x.SocketId == socketId).ConnecteeId;
            Client clientSender = clientRep.Get().SingleOrDefault(x => x.Id == senderId);
            if (clientSender != null)
            {
                return clientSender.Name;
            }
            PrivateConsultant privateSender = privateRep.Get().SingleOrDefault(x => x.Id == senderId);
            if (privateSender != null)
            {
                return privateSender.Name;
            }
            JuridicConsultant juridicSender = juridicRep.Get().SingleOrDefault(x => x.Id == senderId);
            if (juridicSender != null)
            {
                return juridicSender.LTDTitle;
            }
            return null;
        }

        private IEnumerable<ChatMsg> GetHistory(long orderId, long lastMessageId)
        {
            return chatMsgRep.Get().Where(x => x.OrderId == orderId && x.Id <= lastMessageId)
                                   .OrderByDescending(x => x.Id)
                                   .Take(HISTORY_LIMIT);
        }

        public IEnumerable<ChatHistoryVM> GetHistoryVMs(long orderId, long lastMessageId)
        {
            IList<ChatHistoryVM> chatHistoryVM = new List<ChatHistoryVM>();
            foreach (ChatMsg message in GetHistory(orderId, lastMessageId))
            {
                chatHistoryVM.Add(new ChatHistoryVM
                                  {
                                      Id = message.Id,
                                      SenderId = message.SenderId,
                                      SenderName = message.SenderName,
                                      Text = message.Text,
                                      Image = message.Image,
                                      Date = message.DateTime
                                  });
            }
            return chatHistoryVM;
        }

        /// <summary>
        /// Method called when a client connects to the server WebSocket.
        /// </summary>
        /// <param name="socket">The client that connects.</param>
        private void OnOpen(IWebSocketConnection socket)
        {
            FleckLog.Info("Client <" + socket.ConnectionInfo.ClientIpAddress + "> connected");
        }

        /// <summary>
        /// Method called when a client disconnects from the server WebSocket.
        /// </summary>
        /// <param name="socket">The client that disconnects.</param>
        private void OnClose(IWebSocketConnection socket)
        {
            // If socket is stored in connected sockets list, remove it
            if (this.connectedSockets.ContainsKey(socket))
            {
                this.connectedSockets.Remove(socket);
                RemoveConnectee(socket.ConnectionInfo.Id);
            }
        }

        /// <summary>
        /// Method called when an error occurs.
        /// </summary>
        /// <param name="socket">The client that disconnects.</param>
        /// <param name="exception">The exception that occured.</param>
        private void OnError(IWebSocketConnection socket, Exception exception)
        {
            FleckLog.Info("Error with client <" + socket.ConnectionInfo.ClientIpAddress + ">: " + exception.ToString());
        }
    } // END class WebSocketExample
}