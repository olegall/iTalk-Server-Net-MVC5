using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Utils
{
    public class Settings
    {
        public readonly static string localIP = "127.0.0.1";
        public readonly static string localPort = "8000";
        public readonly static string remotePort = "8000";

        public readonly static string remoteIP = "0.0.0.0";


        public readonly static string noConsWithId = "Консультанта с таким Id нет в БД";

        public readonly static string createJurEx = "Не удалось добавить юр. консультанта при регистрации. Текст исключения: ";
        public readonly static string createjurImagesEx = "Не удалось добавить изображение(я) юр. консультанта при регистрации. Текст исключения: ";

        public readonly static string createPrivateEx = "Не удалось добавить физлицо при регистрации. Текст исключения: ";
        public readonly static string createPrivateImagesEx = "Не удалось добавить изображение(я) физлица при регистрации. Текст исключения: ";

        public readonly static string updatePrivateEx = "Не удалось редактировать физлицо. Текст исключения: ";
        public readonly static string updateJurEx = "Не удалось редактировать юрлицо. Текст исключения: ";

        public readonly static string[] docKeys = { "doc1", "doc2", "doc3", "doc4", "doc5" };
    }
}