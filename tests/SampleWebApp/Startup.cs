using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp
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
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseRivenSpa((spa) =>
            {
                var spaName = "ui1";
                var requestPath = $"/{spaName}";

                spa.Options.RequestPath = requestPath;
                spa.Options.PageStaticFileOptions = new StaticFileOptions()
                {
                    RequestPath = spa.Options.RequestPath,
                    FileProvider = new PhysicalFileProvider(
                       Path.Join(env.WebRootPath, spaName)
                    )
                };
                spa.Options.DevServer = new Uri("http://localhost:4200");
            });

            app.UseRivenSpa((spa) =>
            {
                var spaName = "ui2";
                var requestPath = $"/{spaName}";

                spa.Options.RequestPath = requestPath;
                spa.Options.PageStaticFileOptions = new StaticFileOptions()
                {
                    RequestPath = spa.Options.RequestPath,
                    FileProvider = new PhysicalFileProvider(
                       Path.Join(env.WebRootPath, spaName)
                    )
                };
                spa.Options.DevServer = new Uri("http://localhost:8200");
            });




            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
