using System;
using System.Web.Http;
using WebApplication1.BLL.ChatServer;
using WebApplication1.BLL.WebSocketChatServer;
using WebApplication1.Utils;
using WebApplication1.Misc;

namespace WebApplication1.Controllers
{
    public class ChatController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        static ServerObject server; // сервер !!! избавиться от подчёркивания

        /// <summary>
        /// Запустить websocket серверу
        /// </summary>
        [HttpGet]
        [Route("api/chat/start")]
        public Object Start()
        {
            try
            {
                WebSocketChatServer webSocketChatServer;
                string[] args = { Settings.localIP, Settings.localPort };
                try
                {
                    // Create object. Constructor starts the server
                    webSocketChatServer = new WebSocketChatServer(args);
                    webSocketChatServer.Start();
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("1" + e.ToString());
                }
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine("2" + ex.Message);
            }
            return Ok(true);
        }

        /// <summary>
        /// Получить историю сообщений
        /// </summary>
        [HttpGet]
        [Route("api/chat/history/{orderId}/{lastMessageId}")] // !!! вернуть изображение
        public Object GetHistory(long orderId, long lastMessageId)
        {
            return Ok(new WebSocketChatServer().GetHistoryVMs(orderId, lastMessageId));
        }
    }
}