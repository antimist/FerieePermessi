using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Application;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.Services.Application.Courses;
using Microsoft.Extensions.Hosting;

namespace MyCourse
{
    public class Startup
    {
        private  IConfiguration Configuration {get;}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddMvc(options =>
            {
                var homeProfile = new CacheProfile();
                //homeProfile.Duration= Configuration.GetValue<int>("ResponseCache:Home:Duration");
                //homeProfile.Location= Configuration.GetValue<ResponseCacheLocation>(("ResponseCache:Home:Location"));
                //homeProfile.VaryByQueryKeys= new string[] {"page"};
                Configuration.Bind("ResponseCache:Home", homeProfile);
                options.CacheProfiles.Add("Home", homeProfile);
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            #if DEBUG
            .AddRazorRuntimeCompilation()
            #endif
            ;
            
            //Usiamo ADO.NET o Entity Famework Core per l'accesso ai dati?
            //var persistence = Persistence.EfCore;
            //switch (persistence)
            //{
            //    case Persistence.AdoNet:
                    //services.AddTransient<ICourseService, AdoNetCourseService>();
                    services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
            //    break;
            //
            //    case Persistance.EfCore:
                    services.AddTransient<ICourseService, EfCoreCourseService>();
                    services.AddDbContextPool<MyCourseDbContext>(optionsBuilder => {
                        string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                        optionsBuilder.UseSqlite(connectionString);
                    });
            //    break;
            //}

            services.AddTransient<ICachedCourseService, MemoryCacheCourseService>(); // se vuoi disattivare la cache devi commentare questa riga

            #region Configurazione del sevizio di cache distribuita
            
            //Se vogiamo usare Redis, ecco le istruzioni per installarlo: https://docs.microsoft.com/it-it/aspnet/core/performance/caching/distributed?view=aspnetcore-2.2#distributed-redis-cache
            //Bisogna anche installare il pacchetto NuGet: Microsoft.Extension.Caching.StackExchangeRedis
            /*services.AddStackExchngeRedisCache(options => 
            {
                Configuration.Bind("DistributedCache:Redis", options);
            });
            */
            //Se vogliamo usare Sql Server, ecco le istruzioni per preparare la tabella usata per la cache https://docs.microsoft.com/it-it/aspnet/core/performance/caching/distributed?view=aspnetcore-2.2#distributed-sql-server-cache
            /* services.AddDistributedSqlServerCache(options =>
            {
                Configuration.Bind("DistributedCache:SqlServer", options);
            });
            */
            //Se vogliamo usare la memoria, mentre siamo in sviluppo
            //service.AddDistributedMemoryCache();

            #endregion

            //Options
            services.Configure<CoursesOptions>(Configuration.GetSection("Courses"));
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<MemoryCacheOptions>(Configuration.GetSection("MemoryCache"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)  
        {
            //if (env.IsDevelopment())
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                //Aggiorniamo un file per notificare al BrowserSyngk che deve agiornare la pagina
                lifetime.ApplicationStarted.Register(() =>
                {
                    string filePath = Path.Combine(env.ContentRootPath, "bin/reloaded.txt");
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                });
            } 
            else 
            {
                app.UseExceptionHandler("/Error");
            }
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseResponseCaching();

            //EndPointMiddleware
            app.UseEndpoints(routeBuilder => {
                routeBuilder.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseMvcWithDefaultRoute();
            /*
            app.UseMvc(routeBuilder =>
            {
                // /Courser/Detail/5
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            */
        }
    }
}
