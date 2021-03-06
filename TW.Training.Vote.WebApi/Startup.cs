
using Microsoft.OpenApi.Models;
using TW.Infrastructure.ApsNetCore.Middlewares;
using TW.Infrastructure.Core.Components;
using TW.Infrastructure.Domain.WebWorkContext;
using TW.Training.Vote.Domain.Programmes;
using TW.Training.Vote.Domain.Votings;
using TW.Training.Vote.Infrastructure;
using TW.Training.Vote.Infrastructure.AutoMapping;
using TW.Training.Vote.WebApi.Models.Programmes;

namespace TW.Training.Vote.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Infrastructure
        ConfigureInfrastructure(services);
        // Domain
        ConfigureDependencyService(services);
        // AutoMapper
        ConfigureObjectMapper(services);
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

    private void ConfigureInfrastructure(IServiceCollection services)
    {
        services.ConfigureInfrastructure(
            () => new VoteInfrastructureConfigure
            {
                Database = new VoteInfrastructureConfigure.DatabaseSetting { ConnectionString = Configuration.GetSection("Voting:Database:ConnectionString").Value }
            }
        );
    }

    private void ConfigureDependencyService(IServiceCollection services)
    {
        services.AddScoped<WebWorkContext>();

        services.AddScoped<IProgrammeService, ProgrammeService>();
        services.AddScoped<IVotingService, VotingService>();
        services.AddScoped<IStatisticService, StatisticService>();

    }

    private void ConfigureObjectMapper(IServiceCollection services)
    {
        services.AddSingleton<IObjectMapperComponent, AutoMapperObjectComponent>();
        
        services.AddAutoMapper(config =>
        {
            config.DisableConstructorMapping();

            config.CreateMap<Programme, GetProgrammeOutput>();
            config.CreateMap<ProgrammeItem, GetProgrammeOutput.Item>();

            config.CreateMap<CreateProgrammeRequest, CreateProgrammeInput>();
            config.CreateMap<CreateProgrammeRequest.Item, CreateProgrammeInput.Item>();
            
            config.CreateMap<UpdateProgrammeRequest, UpdateProgrammeInput>();
            config.CreateMap<UpdateProgrammeRequest.Item, UpdateProgrammeInput.Item>();
        });
    }

    private void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type => type.FullName);
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TW.TRAINING.VOTING WEBAPI", Version = "v1" });
        });
    }
}