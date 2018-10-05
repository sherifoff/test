using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Models;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace RealEstate
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                Configuration["Data:ConnectionString"]));

            
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IEstateItemRepository, EstateItemRepository>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IHouseTypeRepository, HouseTypeRepository>();            

            services.AddMvc();
            services.AddNodeServices();
            
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();


            app.UseMvc(routes =>
            {
                
                routes.MapRoute(
                    name: "pagination",
                    template: "Home/Page{estateItemPage}",
                    defaults: new { Controller = "Home", action = "List" });

                routes.MapRoute(
                    name: "paging",
                    template: "{controller=Home}/{action=List}/{id?}");


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{estateItemPage:int}",
                    defaults: new { controller = "Home", action = "List" }
                    );
                routes.MapRoute(
                    name: null,
                    template: "Page{estateItemPage:int}",
                    defaults: new
                    {
                        controller = "Home",
                        action = "List",
                        productPage = 1
                    }
                     );
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new
                    {
                        controller = "Home",
                        action = "List",
                        productPage = 1
                    }
                    );
                routes.MapRoute(
                name: null,
                template: "",
                defaults: new
                {
                    controller = "Home",
                    action = "List",
                    productPage = 1
                });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");

            });
        }
    }
}
