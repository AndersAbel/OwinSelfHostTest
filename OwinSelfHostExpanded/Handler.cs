using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHostExpanded
{
    using System.IO;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    // Part of a factory assembly line, e.g. the station putting paint on the manufactured 
    // item. Has to be a class to keep the next state, which to know where to send the item
    // next.
    class Handler
    {
        AppFunc next;

        public Handler(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> context)
        {
            if (context["owin.RequestPath"] as string == "/")
            {
                using (var writer = new StreamWriter(context["owin.ResponseBody"] as Stream))
                {
                    await writer.WriteAsync("Hello World!");
                }
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
