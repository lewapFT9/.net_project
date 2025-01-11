namespace TicketReservationApplication.Entities
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class AttendanceUpdaterService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(15);

        public AttendanceUpdaterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var screeningService = scope.ServiceProvider.GetRequiredService<ScreeningService>();

                        await screeningService.UpdateAttendancesForEndedScreenings();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in AttendanceUpdaterService: {ex.Message}");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }

}
