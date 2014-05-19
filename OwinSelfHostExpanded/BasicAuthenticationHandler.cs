using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHostExpanded
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    class BasicAuthenticationHandler
    {
        AppFunc next;

        public BasicAuthenticationHandler(AppFunc next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(IDictionary<string, object> context)
        {
            if(((IDictionary<string, string[]>)context["owin.RequestHeaders"]).ContainsKey("Authorization"))
            {
                await next.Invoke(context);
            }
            else
            {
                ((IDictionary<string, string[]>)context["owin.ResponseHeaders"])["WWW-Authenticate"] =
                    new string[] { "Basic realm=\"http://localhost\"" };

                context["owin.ResponseStatusCode"] = 401;
            }
        }
    }
}
