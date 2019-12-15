﻿using Microsoft.AspNetCore.Identity;
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
             services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                //  .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IPostService, PostService>();
        }
    }
    //Add-Migration AddedPosts20191216 =>1
    //Update-Database => 2
}
