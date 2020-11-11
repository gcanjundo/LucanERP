using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebUI
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
            services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }); ; 
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession(options => 
            {
                options.IdleTimeout = System.TimeSpan.FromSeconds(14400);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Seguranca/Login/";

                    });
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSingleton<KitandaConfig>();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting(); 
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints =>
            {

                /*
                endpoints.MapAreaControllerRoute(
                    name: "Seguranca",
                    areaName:"Seguranca", 
                    pattern: "Seguranca/{controller=Acesso}/{action=Login}/{id?}") ;
                
                endpoints.MapAreaControllerRoute(
                    name: "Geral",
                    areaName: "Geral",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "RecursosHumanos",
                    areaName: "RecursosHumanos",
                    pattern: "{controller=Home}/{action=Index}/{id?}");*/

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            }); 
        }
    }
}
