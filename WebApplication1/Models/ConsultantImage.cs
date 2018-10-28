using System;

namespace WebApplication1.Models
{
    public class ConsultantImage : File
    {
        public long ConsultantId { get; set; }
        public long FileTypeId { get; set; }

        public ConsultantImage(long consultantId, 
                               byte[] bytes, 
                               string fileName, 
                               int size, 
                               DateTime date, 
                               long fileTypeId)
        {
            ConsultantId = consultantId;
            Bytes = bytes;
            FileName = fileName;
            Size = size;
            Date = date;
            FileTypeId = fileTypeId;
        }
    }
}