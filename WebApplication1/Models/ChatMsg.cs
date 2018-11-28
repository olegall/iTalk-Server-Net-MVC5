using System;

namespace WebApplication1.Models
{
    public class ChatMsg //: Base
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime DateTime { get; set; }
    }
}