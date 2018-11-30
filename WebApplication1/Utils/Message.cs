namespace WebApplication1.Utils
{
    public static class Message
    {
        public static string ClientNotFound { get { return "Нет клиента с таким Id"; } }
        public static string ServerOrConnectionFailed { get { return "Пробелы с сервером или подключением"; } }



        public static string UnrecognizedProblem { get { return "Неизвестная проблема"; } }
    }
}