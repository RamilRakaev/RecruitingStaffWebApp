using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace RecruitingStaffWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var nLogger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                nLogger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                nLogger.Error(e, "Stopped program because of exception");
            }
            finally
            {
                
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(
                builder =>
                {
                    builder.AddJsonFile("appsettings.User.json", false);
                })
                .ConfigureWebHostDefaults(
                webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(log =>
                {
                    log.ClearProviders();
                    log.SetMinimumLevel(LogLevel.Trace);
                    log.AddConsole();
                }
                ).UseNLog();
    }
}
