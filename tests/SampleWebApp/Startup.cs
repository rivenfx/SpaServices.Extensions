using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

using System;
using System.IO;

using System.Collections.Generic;
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
                var spaName = "app1";
                var requestPath = $"/{spaName}";

                spa.Options.RequestPath = requestPath;

                // 开发时使用
                if (env.IsDevelopment())
                {
                    spa.ProxyDevServer("http://localhost:8201");
                }
                else // 发布后使用
                {
                    spa.Options.PageStaticFileOptions = new StaticFileOptions()
                    {
                        RequestPath = spa.Options.RequestPath,
                        FileProvider = new PhysicalFileProvider(
                            Path.Join(env.WebRootPath, spaName)
                        )
                    };
                }
            });

            app.UseRivenSpa((spa) =>
            {
                var spaName = "app2";
                var requestPath = $"/{spaName}";

                spa.Options.RequestPath = requestPath;


                // 开发时使用
                if (env.IsDevelopment())
                {
                    spa.ProxyDevServer("http://localhost:8202");
                }
                else // 发布后使用
                {
                    spa.Options.PageStaticFileOptions = new StaticFileOptions()
                    {
                        RequestPath = spa.Options.RequestPath,
                        FileProvider = new PhysicalFileProvider(
                            Path.Join(env.WebRootPath, spaName)
                        )
                    };
                }
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
