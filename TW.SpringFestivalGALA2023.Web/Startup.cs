
using System.Net;
using Microsoft.OpenApi.Models;
using TW.Infrastructure.ApsNetCore.Middlewares;
using TW.Infrastructure.Core.Components.HttpClients;
using TW.Infrastructure.Core.Components.TransientFalutProcess;
using TW.Infrastructure.RestsharpHttpClient;
using TW.Infrastructure.TransientFaultProcess;
using TW.SpringFestivalGALA2023.Web.Models.Configures;
using TW.SpringFestivalGALA2023.Web.Services;
using TW.SpringFestivalGALA2023.Web.Services.VotingApi;

namespace TW.SpringFestivalGALA2023.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // DI
        ConfigureDependencyService(services);
        // swagger
        ConfigureSwagger(services);
        
        // Web
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseGlobalExceptionCatcher();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();

            // swagger
            endpoints.MapSwagger();
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("v1/swagger.json", "My API V1");
        });
    }

    private void ConfigureDependencyService(IServiceCollection services)
    {
        services.Configure<VotingApiConfiguration>(Configuration.GetSection("VotingAPI"));

        services.AddSingleton<IRetryProcessor, RetryProcessor>();
        services.AddSingleton<IHttpClientService, RestsharpHttpClientService>();
        
        services.AddScoped<IProgrammeService, ProgrammeService>();
        services.AddScoped<IVotingService, VotingService>();
        services.AddScoped<IVotingApiProxy, VotingApiProxy>();
    }
    
    private void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type => type.FullName);
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TW.SPRINGFESTIVALGALA2023 WEBAPI", Version = "v1" });
        });
    }
}