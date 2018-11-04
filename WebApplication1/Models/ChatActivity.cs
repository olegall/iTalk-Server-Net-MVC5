using System;
using WebApplication1.BLL;
namespace WebApplication1.Models
{
    public class ChatActivity //: Base
    {
        public long Id { get; set; }
        public long ConnecteeId { get; set; }
        public long OrderId { get; set; }
        public Guid SocketId { get; set; }
    }
}