using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace BabyCRM
{
    public static class DbHelper
    {
        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
