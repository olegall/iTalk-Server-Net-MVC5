using System;

namespace WebApplication1.Models
{
    public class CategoryImage : File
    {
        public long CategoryId { get; set; }

        public CategoryImage(long categoryId, byte[] bytes, string fileName, long size, DateTime date) // ! параметры - модели
        {
            categoryId = CategoryId;
            bytes = Bytes;
            fileName = FileName;
            size = Size;
            date = Date;
        }    
    }
}