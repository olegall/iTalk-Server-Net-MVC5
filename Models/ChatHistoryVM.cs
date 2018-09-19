using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ChatHistoryVM
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public string Image  { get; set; }
        public DateTime Date { get; set; }
    }
}