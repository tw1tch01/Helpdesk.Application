using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Helpdesk.Application.Extensions;
using Helpdesk.Persistence.Actions;
using Helpdesk.Persistence.Extensions;
using Helpdesk.WebAPI.Configuration;
using Helpdesk.WebAPI.Docs;
using Helpdesk.WebAPI.Extensions;
using Helpdesk.WebAPI.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IConfigureOptions<AddedStateAction>, AddedStateConfiguration>();
            services.AddScoped<IConfigureOptions<ModifiedStateAction>, ModifiedStateConfiguration>();
            services.AddDataServices(Configuration);
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

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "helpdesk", "Demo API - full access" }
                            }
                        }
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

            var accessControl = new AccessControl();
            Configuration.GetSection(AccessControl.ConfigurationSectionName).Bind(accessControl);
            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.Authority = accessControl.Url;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ApiConfig.ScopePolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", accessControl.Scope);
                });
            });
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
                options.ExpandResponses("200,201");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(ApiConfig.CorsPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization(ApiConfig.ScopePolicy);
            });
        }
    }
}