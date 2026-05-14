using Microsoft.EntityFrameworkCore;
using MindEdge_1.Data;

public class ChatCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ChatCleanupService> _logger;

    public ChatCleanupService(IServiceProvider serviceProvider, ILogger<ChatCleanupService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Checking for old chat messages to delete...");

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var cutoffDate = DateTime.Now.AddDays(-7);

                    int deletedCount = await dbContext.ChatMessages
                        .Where(m => m.SentAt < cutoffDate)
                        .ExecuteDeleteAsync(stoppingToken);

                    if (deletedCount > 0)
                    {
                        _logger.LogInformation($"Successfully deleted {deletedCount} old messages.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while cleaning up old messages.");
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}