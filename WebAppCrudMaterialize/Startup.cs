using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudTest.DataAccess;
using CrudTest.DataAccess.V1;
using CrudTest.Framework.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebAppCrudMaterialize
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
            #region Config DataBase
            string dbApiDatabase = Configuration.GetValue<string>("ConnectionStrings_DB:Database");
            string dbApiPassword = Configuration.GetValue<string>("ConnectionStrings_DB:Password");
            string dbApiServer = Configuration.GetValue<string>("ConnectionStrings_DB:Server");
            string dbApiUser = Configuration.GetValue<string>("ConnectionStrings_DB:User");
            int dbApiTimeout = Configuration.GetValue<int>("ConnectionStrings_DB:Timeout");
            //dbApiPassword = securityManager.EncryptDecrypt(false, dbApiPassword); ENCRIPTAR
            string connectionDbApi = Connection.DBApi(dbApiServer, dbApiDatabase, dbApiUser, dbApiPassword, "WebApiPrueba");
            #endregion

            #region Logs
            string pathLogFile = Configuration.GetValue<string>("Logs:Path_Log_File");
            string logLevel = Configuration.GetValue<string>("Logs:Level");

            services.AddSingleton<ILogger, Logger>(f => new Logger(pathLogFile, logLevel));
            #endregion

            #region DataAccess
            services.AddSingleton<IProduct, Product>(f => new Product(connectionDbApi, dbApiTimeout));
            #endregion
            #region Connectors
            //services.AddSingleton<IEmailManager, EmailManager>(f => new EmailManager(host, port, from, smtpUser, smtpPassword, flagEnableUserPassword, message));
            #endregion

            services.AddControllersWithViews();
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
