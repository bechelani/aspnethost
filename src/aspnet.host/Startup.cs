using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace aspnet.host
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {
            app.Run(async context =>
            {
                var request = context.Request;

                var reponse = new
                {
                    Environment = new
                    {
                        EnvironmentName = env.EnvironmentName,
                        ApplicationName = env.ApplicationName,
                        ContentRootFileProvider = env.ContentRootFileProvider,
                        ContenRootPath = env.ContentRootPath,
                        WebRootFileProvider = env.WebRootFileProvider,
                        WebRootPath = env.WebRootPath
                    },
                    Request = new 
                    {
                        Headers = request.Headers.ToList(),
                        ContentType = request.ContentType,
                        ContentLength = request.ContentLength,
                        Cookies = request.Cookies.ToList(),
                        Host = request.Host,
                        IsHttps = request.IsHttps,
                        Path = request.Path,
                        PathBase = request.PathBase,
                        Scheme = request.Scheme,
                        Method = request.Method,
                        Query = request.Query.ToList()
                    },
                    Configuration = config.AsEnumerable().OrderBy(c => c.Key).ToList()
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(reponse, Formatting.Indented));
            });
        }
    }
}
