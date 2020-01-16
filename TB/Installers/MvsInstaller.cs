using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TB.Options;

namespace TB.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
           // var moreOptions = new MoreOptions();
           // configuration.Bind(nameof(moreOptions), moreOptions);
            //configuration.GetSection("MoreOptions").Bind(moreOptions);

            //var builder = new ConfigurationBuilder()
             //   .AddJsonFile("person.json");
            //builder.Build().GetSection();

            //services.AddSingleton(moreOptions);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "TB API", Version = "v1" });
            });
        }
    }
}
