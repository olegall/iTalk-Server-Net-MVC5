using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Misc
{
    public class BaseUrl
    {
        public static string Get(string path)
        {
            var request = HttpContext.Current?.Request;

            string baseUrl = null;
            if (request != null)
            {
                var authUrl = request?.Url?.Authority;
                var appUrl = HttpRuntime.AppDomainAppVirtualPath;
                baseUrl = $"{request.Url.Scheme}://{authUrl}{appUrl}{path}";
            }
            else
            {
                //var hostUrl = Settings.Get<string>(SettingCode.HostUrl);

                //baseUrl = $"{hostUrl}/{path}";
            }

            return baseUrl.Replace("//", "/").Replace(":/", "://");
        }
    }
}