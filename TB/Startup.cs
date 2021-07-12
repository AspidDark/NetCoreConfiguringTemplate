using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TB.Options;
using TB.Installers;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TB.Contracts.HealthChecks;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //var context = AdapterSmevContext.Create(configuration);
            //
            //context.Database.Migrate();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssemblies(Configuration);
            // services.AddMvc(options => options.EnableEndpointRouting = false);
           // services.AddControllers(opt => opt.Filters.Add<ValidationFilter>()); Фильтр валидации
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                 app.UseHsts();
            }

            //health checks
            //  app.UseHealthChecks("/health");
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new HealthCheck
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description
                        }),
                        Duration = report.TotalDuration
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            });


            #region Swager#1
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;

            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });
            #endregion


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //JWT token
            app.UseAuthentication();
            //Its Ok
            app.UseMvc();
        }
    }



    public class ValidationFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
