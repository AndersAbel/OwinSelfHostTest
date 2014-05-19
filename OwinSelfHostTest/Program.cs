using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHostTest
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start("http://localhost:4242", (app) =>
                {
                    app.Use(new Func<AppFunc, AppFunc>(next => (async context =>
                        {
                            using (var writer = new StreamWriter(context["owin.ResponseBody"] as Stream))
                            {
                                await writer.WriteAsync("Hello World!");
                            }
                        })));
                }))
            {
                Console.ReadKey();
            }
        }
    }
}
