using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OA.DAL;
using OA.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.IO;
using System.Text;
using Serilog;
using Serilog.Sinks.RollingFile;
using Serilog.Events;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using Microsoft.Framework.DependencyInjection;

namespace OA.UI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            String fileLogPath = env.ContentRootPath + @"\Log\";

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.RollingFile(Path.Combine(fileLogPath, "log-{Date}.txt"), LogEventLevel.Debug)
                        .CreateLogger();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
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
        public void ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            // add MVC
            services.AddMvc();

            // add cashing.
            //services.AddCachaing();
            
            // add session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = "MyApplication";
            });

            //reCaptcha
            services.AddRecaptcha(new RecaptchaOptions {
                //SiteKey = Configuration[""],
                //SecretKey = Configuration[""]
                SiteKey = "6LfPsSQTAAAAAL9_nloxy9BT5d_74DvsgcVyZLXw",
                SecretKey = "6LfPsSQTAAAAABAlonl4Y0zIMQr_2snXa2UTe4iJ"
            });
            
            // add Dbcontext
            var connectiongString = @"Data Source=.;Initial Catalog=OA_DB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddDbContext<OA.Model.OAContext>(options => options.UseSqlServer(connectiongString, b => b.MigrationsAssembly("OA.UI")));

            // add configuration.
            services.AddOptions();
            services.Configure<ReflectUserInfo>(Configuration.GetSection("UserInfoDal"));

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // for log 
            loggerFactory.AddSerilog();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            //use session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
