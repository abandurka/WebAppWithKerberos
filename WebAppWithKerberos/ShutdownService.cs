using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAppWithKerberos
{
    public class ShutdownService: IHostedService
    {
        private readonly ILogger _logger;

        public ShutdownService(ILogger<ShutdownService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(ShutdownService)} started");
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(ShutdownService)} is stopping");

            for (var i = 0; i < 5; i++)
            {
                _logger.LogInformation($"{nameof(ShutdownService)} is stopping { i } sec");
                await Task.Delay(1000);
            }
        }
    }

    public class KerberosService : IHostedService
    {
        private readonly ILogger _logger;

        public KerberosService(ILogger<KerberosService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(KerberosService)} started");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                _logger.LogInformation($"It's linux!");
                BuildLinuxKerberosSchedule();
            }

            return Task.CompletedTask;
        }

        private void BuildLinuxKerberosSchedule()
        {
            // setup cron schedule

            // run kinit.sh

            //var strCmdText = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
            //System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}