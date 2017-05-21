using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions;

namespace Lab3
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json")
                          .Build();

            var logFile = Path.Combine(env.ContentRootPath, "logfile.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFile)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // loggerFactory.AddConsole(LogLevel.Trace);
            loggerFactory.AddConsole((category, level) => category == typeof(Startup).FullName);

            var startupLogger = loggerFactory.CreateLogger<Startup>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseStaticFiles();
            //app.UseFileServer(); //It show directly the index.html and skip the app.Run write Async
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"{Configuration["message"]}");
            });
            startupLogger.LogInformation("Application startup complete!");

            startupLogger.LogCritical("This is a critical message");
            startupLogger.LogDebug("This is a debug message");
            startupLogger.LogTrace("This is a trace message");
            startupLogger.LogWarning("This is a warning message");
            startupLogger.LogError("This is an error message");
        }
    }
}
