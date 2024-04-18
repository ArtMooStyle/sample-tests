using MT.Logging;
using SampleWebApiApp;
using Serilog;

SerilogElasticApm.InitializeLogger();
try
{
    Log.Information("Starting up");
    ThreadPool.SetMaxThreads(4, 8);
    ThreadPool.SetMinThreads(2, 4);
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .Build()
        .Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}