using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class File
    {
        #region Fields
        public long Id { get; set; }
        public byte[] Bytes { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(250)]
        public string FileName { get; set; }
        public long Size { get; set; }
        #endregion
    }
}