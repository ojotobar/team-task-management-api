using Serilog;

namespace TeamTaskManagerApi.Configurations
{
    public static class LogConfigurations
    {
        public static void ConfigureLogging()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var config = GetConfigurations(env);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.Console()
                .Enrich.WithProperty("Environment", env)
                .ReadFrom.Configuration(config)
                .CreateLogger();
        }

        public static IConfigurationRoot GetConfigurations(string environment)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            return config;
        }
    }
}
