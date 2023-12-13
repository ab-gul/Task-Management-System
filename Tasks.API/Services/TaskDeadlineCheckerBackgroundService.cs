
using Microsoft.EntityFrameworkCore;
using Tasks.API.Data;
using Tasks.API.Data.Abstract;
using Tasks.API.Domain;

namespace Tasks.API.Services
{
    public class TaskDeadlineCheckerBackgroundService : BackgroundService
    {
        private readonly AppDbContext _dbContext;

        public TaskDeadlineCheckerBackgroundService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Perform daily checks and update task statuses
                await UpdateTaskStatusses(DateTime.Now);

                // Delay for 24 hours before the next check
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private async Task UpdateTaskStatusses(DateTime desiredDate)
        {
            await _dbContext.Tasks
               .Where(t => t.DueDate < desiredDate && t.Status == Status.Pending)
               .ForEachAsync(t => t.Edit(null, null, Status.InProgress));

            await _dbContext.Tasks
                .Where(t => t.DueDate < desiredDate && t.Status == Status.InProgress)
                .ForEachAsync(t => t.Edit(null, null, Status.OverDue));

            await _dbContext.SaveChangesAsync();
        }
    }
}
