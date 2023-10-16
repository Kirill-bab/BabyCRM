namespace BabyCRM.Extensions
{
    public static class AppExtensions
    {
        public static void RunWithExceptionsHandling(this WebApplication? app, ILogger logger)
        {
            try
            {
                app?.Run();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}
