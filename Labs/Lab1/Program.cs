using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace newapp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .Configure(app=> {
                app.Run(context=> context.Response.WriteAsync("Hi dude"));
            })
            .UseUrls("http://localhost:5050")
            .Build();
            //Console.WriteLine("Hello World!");
            host.Run();
        }
    }
}
