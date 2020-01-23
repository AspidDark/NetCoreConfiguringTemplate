using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TB.Data;
using TB.HealthChecks;

namespace TB.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        //Microsoft.Extensions.Diagnostics.Healthchecks.EntityFrameworkCore <-nuget
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<DataContext>()
                .AddCheck<RedisHealthCheck>("Redis");
        }
    }
}
