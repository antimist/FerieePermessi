﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MyCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            //if (env.IsDevelopment())
            if(env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();

                //Aggiorniamo un file per notificare al BrowserSyngk che deve agiornare la pagina
//               lifetime.ApplicationStarted.Register(() =>
//               {
//                   string filePath = Path.Combine(env.ContentRootPath, "bin/reloaded.txt");
//                   File.WriteAllText(filePath, DateTime.Now.ToString());
//               });
            }
            app.UseStaticFiles();

            
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routeBuilder =>
            {
                // /Courser/Detail/5
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            /*
            app.Run(async (context) =>
            {
                string nome = context.Request.Query["nome"];
                await context.Response.WriteAsync($"<h1>Ciao {nome}!</h1>");
            });
            */
        }
    }
}
