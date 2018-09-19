using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ChatActivity
    {
        public long Id { get; set; }
        public long ConnecteeId { get; set; }
        public long OrderId { get; set; }
        public Guid SocketId { get; set; }
    }
}