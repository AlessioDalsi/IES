using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Assistenza.BufDalsi.Web.Data;
using Assistenza.BufDalsi.Web.Models;
using Assistenza.BufDalsi.Web.Services;
using Microsoft.AspNetCore.Identity;
using Assistenza.BufDalsi.Data;
using Microsoft.AspNetCore.DataProtection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Assistenza.BufDalsi.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            var cultureInfo = new CultureInfo("it-IT");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();


        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
              .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
              .AddDataAnnotationsLocalization();

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddSingleton<ITicketData>(new TicketData(Configuration.GetConnectionString("Awstick")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var cultureInfo = new CultureInfo("it-IT");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/_Error");
                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "ImpiantoDetails",                                              // Route name
                    template: "{controller=Impianto}/{action=ImpiantoDetails}/{ipt_Id}/{clt_Id?}"  // URL with parameters
                    );  // Parameter default
            });

            await CreateRoles(app.ApplicationServices.GetRequiredService<RoleManager<ApplicationRole>>());
            await ConfigureSiteAdmin(app.ApplicationServices.GetRequiredService<RoleManager<ApplicationRole>>(), app.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>());
        }

        //Creazione ruoli predefiniti al primo avvio dell'applicazione. In caso siano già presenti del DB non ne vengono creati altri
        private async Task CreateRoles(RoleManager<ApplicationRole> roleManager)
        {
            var roles = new List<ApplicationRole>
            {
                // These are just the roles I made up. You can make your own!
                new ApplicationRole {Id="1",
                                Name = "Admin",
                                Description = "Amministratore dell'applicazione"},
                new ApplicationRole {Id="2",
                                Name = "User",
                                Description = "Cliente che possiede uno o più impianti"},
                new ApplicationRole {Id="3",
                                Name="Operator",
                                Description="Operatore che si occupa di fare le manutenzioni"}
            };

            foreach (var role in roles)
            {
                if (await roleManager.RoleExistsAsync(role.Name)) continue;
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded) continue;

                // If we get here, something went wrong.
                throw new Exception($"Could not create '{role.Name}' role.");
            }
        }


        //Creazione del Super user "Admin" con ruolo admin all'avvio dell'applicazione. Se l'utente esiste già non ne viene creato un altro
        private async Task ConfigureSiteAdmin(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {

            //TicketData m=new TicketData(Configuration.GetConnectionString("Awstick"));                in caso sia necessario recuperare i clienti in base a ciò che ci manderà elmas
            //m.GetClients();



            if (await userManager.FindByNameAsync("Admin") != null)
                return;
            if (!await roleManager.RoleExistsAsync("Admin"))
                throw new Exception($"The Admin role has not yet been created.");

            var user = new ApplicationUser
            {
                Name = "Admin",
                UserName = "Admin",
                Email = "admin@admin.admin",
                RoleId = "1"
            };

            await userManager.CreateAsync(user, "Password12345.");
            await userManager.AddToRoleAsync(user, "Admin");
        }

    }
}
