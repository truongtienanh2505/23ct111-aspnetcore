using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
namespace LearnAspNetCore.Services 
{
    public class MyHostedService : BackgroundService
    {
        public MyHostedService()
        {
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
