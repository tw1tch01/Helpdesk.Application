using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Data.Common;
using Helpdesk.Application.Extensions;
using Helpdesk.Persistence.Actions;
using Helpdesk.Persistence.Common.Options;
using Helpdesk.Persistence.Contexts;
using Helpdesk.Persistence.Extensions;
using Helpdesk.Persistence.Options;
using Helpdesk.Services.Common.Contexts;
using Helpdesk.WebAPI.Common;
using Helpdesk.WebAPI.Docs;
using Helpdesk.WebAPI.Extensions;
using Helpdesk.WebAPI.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
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
            services.AddApplication();
            services.AddControllers();

            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader(ApiConfig.VersionHeader);
            });

            services.AddCors(options =>
            {
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

                options.SwaggerDoc($"v{ApiConfig.CurrentVersion}", new OpenApiInfo
                {
                    Version = $"v{ApiConfig.CurrentVersion}",
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

                options.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                options.DocInclusionPredicate((name, api) => true);
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

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                await ExceptionHandler.Handle(context);
            }));

            app.UseSwagger();

            app.UseReDoc(options =>
            {
                options.RoutePrefix = "docs";
                options.SpecUrl = $"/swagger/v{ApiConfig.CurrentVersion}/swagger.json";
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

        private static string GetUser(HttpContext httpContext)
        {
            return httpContext?.User?.Identity?.Name ?? "system";
        }

        private static string GetPath(HttpContext httpContext)
        {
            var request = httpContext?.Request;

            return request == null ? "/no-context" : $"{request.Path} [{request.Method}]";
        }

        private void AddDataService(IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                return new AddedStateAction
                {
                    CreatedBy = GetUser(httpContext),
                    CreatedProcess = GetPath(httpContext)
                };
            });

            services.AddScoped(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                return new ModifiedStateAction
                {
                    ModifiedBy = GetUser(httpContext),
                    ModifiedProcess = GetPath(httpContext)
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

            var contextOptions = new ContextOptions();
            Configuration.GetSection(ConfigurationSections.Contexts).Bind(contextOptions);

            services.ConfigureMySqlContext<ITicketContext, TicketContext>(contextOptions.TicketContext);
            services.ConfigureMySqlContext<IUserContext, UserContext>(contextOptions.UserContext);
        }
    }
}