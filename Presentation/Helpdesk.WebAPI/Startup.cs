using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Data.Common;
using Helpdesk.Persistence.Common.Actions;
using Helpdesk.Persistence.MySql.Extensions;
using Helpdesk.Persistence.MySql.Options;
using Helpdesk.WebAPI.Common;
using Helpdesk.WebAPI.Docs;
using Helpdesk.WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Helpdesk.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddDataService(services);

            services.AddControllers();

            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(new DateTime(2020, 5, 17), 1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader(ApiConfig.VersionHeader);
            });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy(ApiConfig.CorsPolicy, policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Context accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Swagger

            services.AddSwaggerGen(options =>
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                var description = GetType().Assembly.ReadEmbeddedResource($"{assemblyName}.Redoc.Description.md");

                options.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Helpdesk Application API",
                    Description = description,
                    Contact = new OpenApiContact
                    {
                        Name = "Robert Breedt",
                        Email = "robbiebreedt@yahoo.com"
                    }
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Properties/{assemblyName}.xml"));
                options.DocumentFilter<TagDescriptionDocumentFilter>();
            });

            #endregion Swagger
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseReDoc(options =>
            {
                options.RoutePrefix = "docs";
                options.SpecUrl = "/swagger/v1.0/swagger.json";
                options.DocumentTitle = "Helpdesk Application API";
                options.ExpandResponses(string.Empty);
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(ApiConfig.CorsPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddDataService(IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                return new AddedStateAction
                {
                    //CreatedBy = GetUser(httpContext),
                    //CreatedProcess = GetPath(httpContext)
                };
            });

            services.AddScoped(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                return new ModifiedStateAction
                {
                    //ModifiedBy = GetUser(httpContext),
                    //ModifiedProcess = GetPath(httpContext)
                };
            });

            services.AddScoped(provider =>
            {
                var addedContext = provider.GetService<AddedStateAction>();
                var modifiedContext = provider.GetService<ModifiedStateAction>();
                var contextScope = new ContextScope();
                contextScope.StateActions[EntityState.Added] = addedContext.SetCreatedAuditFields;
                contextScope.StateActions[EntityState.Modified] = modifiedContext.SetModifiedAuditFields;

                return contextScope;
            });

            var mySqlOptions = new MySqlOptions();
            Configuration.GetSection(MySqlConfig.SectionName).Bind(mySqlOptions);

            services.AddMySqlPersistence(options =>
            {
                options.Server = mySqlOptions.Server;
                options.Database = mySqlOptions.Database;
                options.Username = mySqlOptions.Username;
                options.Password = mySqlOptions.Password;
                options.Version = mySqlOptions.Version;
            });
        }
    }
}