namespace BlazorServer;

public class Program
{
    private static async Task Main(string[] args)
    {
        var host = BuildWebHost(args, _ => { });
        await host.RunAsync();
    }

    public static IHost BuildWebHost(string[] args, Action<IServiceCollection> configureServices)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStaticWebAssets();
                builder.UseStartup<Startup>();
                builder.ConfigureServices(configureServices);
            })
            .Build();
}