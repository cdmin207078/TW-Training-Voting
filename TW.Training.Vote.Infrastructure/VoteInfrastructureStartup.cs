using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using TW.Training.Vote.Domain.Programmes;
using TW.Training.Vote.Domain.Votings;
using TW.Training.Vote.Infrastructure.Programmes;
using TW.Training.Vote.Infrastructure.Votings;

namespace TW.Training.Vote.Infrastructure;

public static class VoteInfrastructureStartup
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, Func<VoteInfrastructureConfigure> Configure)
    {
        var configure = Configure();
        
        AutoMapperConfigure(services);
        EntityFrameworkCoreConfigure(services, configure);
        EasyCachingConfigure(services);
        RepositoriesConfigure(services);
        
        return services;
    }

    private static void AutoMapperConfigure(IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(VoteInfrastructureStartup));
        services.AddAutoMapper(config =>
        {
            config.DisableConstructorMapping();
        }, assembly);
    }

    private static void EntityFrameworkCoreConfigure(IServiceCollection services,VoteInfrastructureConfigure configure)
    {
        var version = ServerVersion.AutoDetect(configure.Database.ConnectionString);
        services.AddDbContext<VoteDbContext>(
            options => options
                .UseMySql(configure.Database.ConnectionString, version)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information));
    }

    private static void EasyCachingConfigure(IServiceCollection services)
    {
        services.AddEasyCaching(options =>
        {
            options.UseInMemory();
        });
    }

    private static void RepositoriesConfigure(IServiceCollection services)
    {
        services.AddScoped<IProgrammeRepository, ProgrammeRepository>();
        services.AddScoped<IVotingRepository, VotingRepository>();
        services.AddScoped<IStatisticRepository, StatisticRepository>();
    }
}