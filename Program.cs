using System.IO;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


    Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => {
        webBuilder
            .UseKestrel(options => {
                // HTTP 5000
                options.ListenLocalhost(5000);

                // HTTPS 5001
                options.ListenLocalhost(5001, builder =>
                {
                    builder.UseHttps();
                });
            })
            .Configure(app => app.Run(async context => {
                if (context.Request.Path == "/api/echo" && context.Request.Method == "POST") {
                
                    using var reader = new StreamReader(context.Request.Body);
                    var content = await reader.ReadToEndAsync();

            
                    context.Response.ContentType = MediaTypeNames.Text.Plain;
                    await context.Response.WriteAsync(content);
                }
                else if (context.Request.Path == "/api/echo" && context.Request.Method == "GET") {
                
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync($"Hello World!");
                }
            }));
    }).Build().Run();