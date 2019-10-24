using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PracConsoleOwinHost4
{
class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var address = "http://localhost:1234";
                var webapp = WebApp.Start<Init>(address);

                Console.ReadLine();
            }
            catch(Exception ex)
            {
                var s = ex;
            }
        }
    }
    
    class Init
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            Assembly.Load("AspNetApiControllerApp");
            var configuration = new HttpConfiguration();

            configuration.MapHttpAttributeRoutes();
            configuration.EnableCors();

            appBuilder.UseCors(CorsOptions.AllowAll);

            configuration.Routes.MapHttpRoute(
                name:"DefaultApi",
                routeTemplate:"api/{controller}/{id}",
                defaults:new {id=RouteParameter.Optional}
                );



            configuration.EnsureInitialized();

            var httpListener = (HttpListener)appBuilder.Properties["System.Net.HttpListener"];
            httpListener.AuthenticationSchemes = AuthenticationSchemes.Ntlm;

            appBuilder.UseWebApi(configuration);
        }
    }
}
