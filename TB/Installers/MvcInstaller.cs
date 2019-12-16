using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TB.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "TB API", Version = "v1" });
            });
        }
    }
}
