using System;
namespace WebApplication1.Models
{
    public class ServiceImage : File
    {
        public long ServiceId { get; set; }

        public ServiceImage(byte[] bytes, string fileName, int size, DateTime date)
        {
            Bytes = bytes;
            FileName = fileName;
            Size = size;
            Date = date;
        }
    }
}