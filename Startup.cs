using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Recetas.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recetas.Models;
using AppWebSena.Models.Mail;

namespace Recetas
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<SmtpConfig>(Configuration.GetSection("SmtpConfig"));
            services.AddSingleton<IEmailSenderP, EmailSender>();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);

            });

            services.AddControllersWithViews();
            services.AddRazorPages();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapAreaControllerRoute(
                name: "Registro",
                areaName: "Registrarse",
                pattern: "Registrarse/{controller=Registro}/{action=Inicio}/{id?}");

                endpoints.MapAreaControllerRoute(
                name: "Recetas",
                areaName: "Recetas",
                pattern: "Recetas/{controller=Receta}/{action=Guardar}/{id?}");


                endpoints.MapAreaControllerRoute(
               name: "Ingrediente",
               areaName: "Ingrediente",
               pattern: "Ingrediente/{controller=Ingrediente}/{action=GuardarIngrediente}/{id?}");

                endpoints.MapAreaControllerRoute(
                name: "RecetaUsuario",
                areaName: "RecetaUsuario",
                pattern: "RecetaUsuario/{controller=RecetaUsuario}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                name: "Recetas",
                areaName: "Recetas",
                pattern: "Recetas/{controller=RecetaNombre}/{action=AgregarNombre}/{id?}");
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                
            });

        }
    }
}
