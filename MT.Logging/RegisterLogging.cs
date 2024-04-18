using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MT.Logging;

public static class RegisterLogging
{
    public static IHostBuilder ConfigureLogger(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog
        (
            (context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.WithElasticApmCorrelationInfo()
                    .Enrich.FromLogContext();

                if (!context.HostingEnvironment.IsDevelopment())
                    configuration.WriteTo.Console(new EcsTextFormatter());
                else
                    configuration.WriteTo.Console();
            }
        );
    }
}
public static class SerilogElasticApm
{
    public static void InitializeLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.WithElasticApmCorrelationInfo()
            .Enrich.FromLogContext()
            .WriteTo.Console(new EcsTextFormatter())
            .CreateBootstrapLogger();
    }
}