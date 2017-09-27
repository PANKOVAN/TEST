using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MEDMCoreLibrary;
using System.IO;
using System;
using Models.MBuilderModel;
using Microsoft.Extensions.FileProviders;

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
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.CookieName = ".MyApp.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });
            services.AddMvc();

            //MEDMDefModel.MainDef.Load(Path.Combine(BaseDir, "Model", "CNTIModel.xml"));
            MBuilderModel.ConnectionPool.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            MBuilderModel.Store = new MEDMStore(Configuration.GetSection("Store").GetSection("Path").Value);

            EDMTrace.IsEnabled = true;
            MBuilderModel bm = new MBuilderModel();
            bm.BaseDir = BaseDir;
            bm.Init();

            services.AddScoped<MBuilderModel>();
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
                FileProvider = new PhysicalFileProvider(MBuilderModel.Store.DataPath),
                RequestPath = "/MStore"
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

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
