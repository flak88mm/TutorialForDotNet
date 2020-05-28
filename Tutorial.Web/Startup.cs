using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tutorial.Web.Data;
using Tutorial.Web.Model;
using Tutorial.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace Tutorial.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //var connectionString = _configuration["ConnectionStrings:DefalutConnection"];
            //Console.WriteLine(conString);
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => {
                //options.UseSqlServer(connectionString);
                options.UseMySQL(connectionString);
            });
            services.AddSingleton<IWelcomeService, WelcomeService>();
            services.AddScoped<IRepository<Student>, EFCoreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IWelcomeService welcomeService, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.Use(next => 
            // {
            //     logger.LogInformation("app.Use()......");
            //     return async HttpContext => 
            //     {
            //         logger.LogInformation("---async httpContext");
            //         if(HttpContext.Request.Path.StartsWithSegments("/first"))
            //         {
            //             logger.LogInformation("---First!");
            //             await HttpContext.Response.WriteAsync("First!!");
            //         }
            //         else
            //         {
            //             logger.LogInformation("---next(httpContext)");
            //             await next(HttpContext);
            //         }
            //     };
            // });

            // app.UseWelcomePage(new WelcomePageOptions{
            //     Path = "/welcome"
            // });

            app.UseStaticFiles();

            app.UseMvc(builder =>
            {
                // /Home/Index/3
                builder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

}
