using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

// https://blogs.msdn.microsoft.com/cesardelatorre/2017/11/18/implementing-background-tasks-in-microservices-with-ihostedservice-and-the-backgroundservice-class-net-core-2-x/
namespace ConsoleHostingDemo
{
    public static class Logger
    {
        public static void Log(string msg)
        {
            Console.WriteLine($"{DateTime.Now.ToString()},{Thread.CurrentThread.ManagedThreadId},{msg}");
        }
    }
        

    public class MyService : IHostedService
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.Log("Entering StartAsync");
            // Store the task we're executing
            Task.Run(() => { ExecuteAsync(_stoppingCts.Token); });
            Logger.Log("Exiting StartAsync");
            return Task.CompletedTask;
        }

        private void ExecuteAsync(CancellationToken token)
        {
            Logger.Log("Entering ExecuteAsync");
            while (true & !token.IsCancellationRequested)
            {
                Thread.Sleep(1000);
                Logger.Log("Continueing work...");

            }
            Logger.Log("Completed work...");
            Logger.Log("Exiting ExecuteAsync");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.Log("Entering StopAsync");
            _stoppingCts.Cancel();
            Logger.Log("Exiting StopAsync");
            return Task.CompletedTask;
        }
    }
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                //.ConfigureLogging((hostContext, config) =>
                //{
                //    config.AddConsole();
                //    config.AddDebug();
                //})
                //.ConfigureAppConfiguration((hostContext, config) =>
                //{
                //    config.AddEnvironmentVariables();
                //    config.AddJsonFile("appsettings.json", optional: true);
                //    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                //    config.AddCommandLine(args);
                //})
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddLogging();
                    //services.AddSingleton<MonitorLoop>();

                    #region snippet1
                    services.AddHostedService<MyService>();
                    #endregion

                    
                });

            await hostBuilder.RunConsoleAsync();
            
        }
    }
}
