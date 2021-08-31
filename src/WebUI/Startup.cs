using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Brokers.Storages;
using Shop.Web.Services.Fondations.FileManager;
using Shop.Web.Services.Fondations.Products;

namespace Shop
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;


        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<Storage>();
            services.AddScoped<IStorage, Storage>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFileManager, FileManager>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
