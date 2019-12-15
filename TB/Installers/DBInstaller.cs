using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TB.Data;
using TB.Services;

namespace TB.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
             services.AddDbContext<DataConrext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                //  .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<DataConrext>();

            services.AddSingleton<IPostService, PostService>();
        }
    }
}
