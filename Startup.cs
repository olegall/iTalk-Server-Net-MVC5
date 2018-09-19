using Owin;
using System;
using System.Threading;
using System.Web.Hosting;

namespace WebApplication1
{
    public partial class Startup : IRegisteredObject
    {
        public Startup()
        {
            HostingEnvironment.RegisterObject(this);
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigAuth(app);
            //ConfigHangfire(app);
        }

        public void Stop(bool immediate)
        {
            Thread.Sleep(TimeSpan.FromSeconds(30));
            HostingEnvironment.UnregisterObject(this);
        }
    }
}