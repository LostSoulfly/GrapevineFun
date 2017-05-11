using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;

namespace WindowsFormsApplication3
{
    [RestResource]
    public class TestResource
    {
        
        //Use the keys list from Form1
        private List<ApiKeys> keys = Form1.keys;

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api")]
        public IHttpContext UseAPIKey(IHttpContext context)
        {
            var key = context.Request.QueryString["key"] ?? "no API key specified";
            /*
            var action = context.Request.QueryString["do"];
            switch (action) {
                case "1":

                    break;
                case "2":

                    break;
                default:
                    context.Response.SendResponse("no action specified");
                    break;
            }
            */
            //context.Response.SendResponse(key);

            var query = keys.FirstOrDefault(k => k.Key == key);
            if (query != null)
            {
                context.Response.SendResponse(query.IP);
            }

            if (keys.Select(k => k.Key).Contains(key))
            {
                //context.Response.SendResponse();
            } else
            {
                context.Response.SendResponse("No such key");
            }
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/getkey")]
        public IHttpContext GetAPIKey(IHttpContext context)
        {
            var exist = keys.FirstOrDefault(e => e.IP == context.Request.RemoteEndPoint.Address.ToString());

            if (exist != null)
            {
                context.Response.SendResponse(exist.Key);
            } else
            {
                string key, ip;
                key = ApiKeyGen.GetUniqueKey(64);
                ip = context.Request.RemoteEndPoint.Address.ToString();
                keys.Add(new ApiKeys() { Key = key, IP = ip });
                //_form.AddToList(ip, key);
                context.Response.SendResponse(key);
            }
            
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/")]
        public IHttpContext Index(IHttpContext context)
        {
            context.Response.SendResponse("API Server");
            return context;
        }

        [RestRoute]
        public IHttpContext CatchAll(IHttpContext context)
        {
            context.Response.SendResponse("Unknown");
            return context;
        }

    }
}
