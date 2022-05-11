using Lightboat.Infrastructure.AspNetCore;
using Lightboat.Infrastructure.Domain.Components;
using Lightboat.Infrastructure.Domain.WebWorkContext;
using Lightboat.Metaverse.Domain.Attachment;
using Lightboat.Metaverse.Domain.Attachment.Services;
using Lightboat.Metaverse.Domain.Authentication;
using Lightboat.Metaverse.Domain.Authorization;
using Lightboat.Metaverse.Domain.Writings;
using Lightboat.Metaverse.Infrastructure;
using Lightboat.Metaverse.Infrastructure.AutoMapping;
using Lightboat.Metaverse.Web.Models.ApplicationConfigure;
using Lightboat.Metaverse.Web.Models.Writings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Linq;

namespace TW.Training.Voting.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure
            services.AddScoped(provider => Configuration.GetSection("Metaverse:Info").Get<MetaverseInfoOptions>());
            services.AddScoped(provider => Configuration.GetSection("Metaverse:Database").Get<MetaverseDatabaseOptions>());
            services.AddScoped(provider => Configuration.GetSection("Metaverse:Cache").Get<MetaverseCacheOptions>());
            services.AddScoped<IAttachmentConfig, MetaverseInfoOptions>(provider => Configuration.GetSection("Metaverse:Info").Get<MetaverseInfoOptions>());
            services.AddScoped<IAuthorizationConfig, MetaverseInfoOptions>(provider => Configuration.GetSection("Metaverse:Info").Get<MetaverseInfoOptions>());
            services.AddScoped<IAuthenticationConfig, MetaverseInfoOptions>(provider => Configuration.GetSection("Metaverse:Info").Get<MetaverseInfoOptions>());
            //services.Configure<MetaverseInfoOptions>(Configuration.GetSection("Metaverse:Info"));
            //services.Configure<MetaverseDatabaseOptions>(Configuration.GetSection("Metaverse:Database"));
            //services.Configure<MetaverseCacheOptions>(Configuration.GetSection("Metaverse:Cache"));
            //services.AddSingleton<IMetaverseInfoOptions, MetaverseInfoOptions>();
            //services.AddSingleton<IMetaverseDatabaseOptions, MetaverseDatabaseOptions>();
            //services.AddSingleton<IMetaverseCacheOptions, MetaverseCacheOptions>();

            // Common
            services.AddScoped<IWebWorkContext, WebWorkContext>();

            // AutoMapper
            ConfigureObjectMapper(services);

            // Repositories
            var connectionString = "server=localhost;user=root;password=1qazXSW@;database=metaverse";
            //var connectionString = "server=rm-bp1zh17dv8xbz1s6nbo.mysql.rds.aliyuncs.com;user=metaverse;password=metaverse1qazXSW@;database=lightboat_metaverse";
            services.ConfigureInfrastructure(
                () => new MetaverseInfrastructureConfigureSetting
                {
                    Database = new MetaverseInfrastructureConfigureSetting.DatabaseSetting { ConnectionString = connectionString }
                }
            );

            // Domain
            // services.ConfigureDomain();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IWritingService, WritingService>();

            // Web
            services.AddControllers();

            // swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lightboat", Version = "v1" });
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

            services.AddAutoMapper(config =>
            {
                config.DisableConstructorMapping();

                config.CreateMap<Category, GetCategoryOutput>();
                config.CreateMap<Category, GetCategoryListOutput.Item>();

                config.CreateMap<Tag, GetTagListOutput.Item>();

                config.CreateMap<AccessibleType, int>()
                    .ConstructUsing(source => source.Value)
                    .ReverseMap()
                    .ConstructUsing(source => new AccessibleType(source));

                config.CreateMap<Writing, GetWritingListOutput.Item>();
                config.CreateMap<Writing, GetWritingOutput>()
                      .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(d => d.Id.Value).ToList()))
                      .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.Tags.Select(d => d.Id.Value).ToList()));
            });
        }
    }
}
