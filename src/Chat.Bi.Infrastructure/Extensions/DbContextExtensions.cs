namespace Chat.Bi.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static async Task RollbackIfExistsAsync(this DbContext context)
    {
        if (context.Database.CurrentTransaction is not null)
        {
            await context.Database.RollbackTransactionAsync();
        }
    }
}
