using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Interfaces
{
    public interface IFile<out TKey>
    {
        TKey Id { get; }
        byte[] Bytes { get; set; }
        DateTime Date { get; set; }
        string FileName { get; set; }
        long Size { get; set; }
    }
}