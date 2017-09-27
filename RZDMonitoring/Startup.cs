using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MEDMCoreLibrary;
using System.IO;
using System;
using Models.RZDMonitoringModel;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace WebixApp
{
    public class Startup
    {
        string BaseDir = null;
        public Startup(IHostingEnvironment env)
        {
            BaseDir = env.ContentRootPath;
            var builder = new ConfigurationBuilder()
                .SetBasePath(BaseDir)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddIdentity<MUser, IdentityRole>() .AddEntityFrameworkStores();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                        .AddCookie(o => o.LoginPath = new PathString("/Account/Login"));


            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.CookieName = ".MyApp.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });
            services.AddMvc();

            //MEDMDefModel.MainDef.Load(Path.Combine(BaseDir, "Model", "CNTIModel.xml"));
            RZDMonitoringModel.ConnectionPool.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            RZDMonitoringModel.Store = new MEDMStore(Configuration.GetSection("Store").GetSection("Path").Value);

            EDMTrace.IsEnabled = true;
            RZDMonitoringModel bm = new RZDMonitoringModel();
            bm.BaseDir = BaseDir;
            bm.Init();

            services.AddScoped<RZDMonitoringModel>();
            services.AddSingleton<IConfigurationRoot>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            app.UseExceptionHandler("//Error");

            app.UseSession();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(RZDMonitoringModel.Store.DataPath),
                RequestPath = "/MStore"
            });

            app.UseAuthentication();
            /*
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            */
        //https://go.microsoft.com/fwlink/?linkid=845470


            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}");
                routes.MapRoute(name: "app", template: "{controller=App}/{action=Main}/{command=Main}");
                routes.MapRoute(name: "data", template: "{controller=Data}/{action=Main}/{command=Main}");
                routes.MapRoute(name: "template", template: "{controller=Templates}/{action=Main}/{command=Main}");
            });
        }
    }
}
