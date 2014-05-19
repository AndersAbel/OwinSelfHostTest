using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Owin;

namespace OwinSelfHostExpanded
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start("http://localhost:4242", Startup))
            {
                Console.ReadLine();
            }
        }

        // The factory designer office that decides what the assembly line should look like
        // by ordering a set of assembly line constructor machines.
        private static void Startup(IAppBuilder app)
        {
            // Have to explicitly construct the Func<...> since the argument of
            // Use() is of type object.
            app.Use(new Func<AppFunc, AppFunc>(BasicAuthenticationMiddleware));

            app.Use(new Func<AppFunc, AppFunc>(StartPageMiddleware));
        }

        private static AppFunc BasicAuthenticationMiddleware(AppFunc next)
        {
            return new BasicAuthenticationHandler(next).InvokeAsync;
        }

        // Builds an item in an assembly line, e.g. the robot that paints items as they
        // are manufactured.
        private static AppFunc StartPageMiddleware(AppFunc next)
        {
            return new StartPageHandler(next).InvokeAsync;
        }
    }
}
