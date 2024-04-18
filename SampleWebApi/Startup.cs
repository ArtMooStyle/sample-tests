using Microsoft.OpenApi.Models;

namespace SampleWebApiApp;

public class Startup(IConfiguration configuration, IWebHostEnvironment environment)
{
    private IConfiguration Configuration { get; } = configuration;
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHealthChecks();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "LastMile.Mobile.BFF", Version = "v1" });
        });
    }
    public void Configure(IApplicationBuilder app)
    {
        if (environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        
        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "LastMile.Mobile.BFF v1"); });
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
