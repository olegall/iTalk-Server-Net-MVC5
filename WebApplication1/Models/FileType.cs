namespace WebApplication1.Models
{
    public enum FileTypes { Photo = 1, Logo, PassportScan, PrivateDoc, JuridicDoc};

    public class FileType
    {
        public long Id { get; set; }
        public string Type { get; set; }
    }
}