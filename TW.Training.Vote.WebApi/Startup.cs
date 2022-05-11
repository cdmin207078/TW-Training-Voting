
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Linq;
using TW.Infrastructure.ApsNetCore.Middlewares;
using TW.Infrastructure.Core.Components;
using TW.Infrastructure.Domain.WebWorkContext;
using TW.Training.Vote.Domain.Programmes;
using TW.Training.Vote.Domain.Votings;
using TW.Training.Vote.Infrastructure;
using TW.Training.Vote.Infrastructure.AutoMapping;

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
        // Common
        services.AddScoped<WebWorkContext>();

        // AutoMapper
        ConfigureObjectMapper(services);

        // Infrastructure
        services.ConfigureInfrastructure(
            () => new VoteInfrastructureConfigure
            {
                Database = new VoteInfrastructureConfigure.DatabaseSetting { ConnectionString = Configuration.GetSection("Voting:Database:ConnectionString").Value }
            }
        );

        // Domain
        services.AddScoped<IProgrammeService, ProgrammeService>();
        services.AddScoped<IVotingService, VotingService>();
        services.AddScoped<IStatisticService, StatisticService>();

        // Web
        services.AddControllers();

        // swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TW.TRAINING.VOTING WEBAPI", Version = "v1" });
        });
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

    public void ConfigureObjectMapper(IServiceCollection services)
    {
        services.AddSingleton<IObjectMapperComponent, AutoMapperObjectComponent>();
        
        //
        // services.AddAutoMapper(config =>
        // {
        //     config.DisableConstructorMapping();
        //
        //     config.CreateMap<Category, GetCategoryOutput>();
        //     config.CreateMap<Category, GetCategoryListOutput.Item>();
        //
        //     config.CreateMap<Tag, GetTagListOutput.Item>();
        //
        //     config.CreateMap<AccessibleType, int>()
        //         .ConstructUsing(source => source.Value)
        //         .ReverseMap()
        //         .ConstructUsing(source => new AccessibleType(source));
        //
        //     config.CreateMap<Writing, GetWritingListOutput.Item>();
        //     config.CreateMap<Writing, GetWritingOutput>()
        //         .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(d => d.Id.Value).ToList()))
        //         .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.Tags.Select(d => d.Id.Value).ToList()));
        // });
    }
}