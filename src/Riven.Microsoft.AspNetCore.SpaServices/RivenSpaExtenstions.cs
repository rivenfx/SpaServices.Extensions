using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    public static class RivenSpaExtenstions
    {
        public static IApplicationBuilder UseRivenSpa(this IApplicationBuilder app, Action<IRivenSpaBuilder> configuration)
        {

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var builder = new DefaultRivenSpaBuilder(app, new RivenSpaOptions());
            configuration.Invoke(builder);


            // RequestPath
            if (string.IsNullOrEmpty(builder.Options.RequestPath.Value))
            {
                throw new ArgumentException($"The value for {nameof(builder.Options.RequestPath)} cannot be null or empty.");
            }
            // DefaultPage
            if (string.IsNullOrEmpty(builder.Options.DefaultPage.Value))
            {
                throw new ArgumentException($"The value for {nameof(builder.Options.DefaultPage)} cannot be null or empty.");
            }


            // 使用静态文件目录
            if (builder.Options.PageStaticFileOptions != null)
            {
                // default page Middleware
                var requestPath1 = builder.Options.RequestPath.Value.TrimEnd('/');
                var requestPath2 = $"{requestPath1}/";

                var defaultPage = string.Format(
                    "{0}{1}",
                    requestPath2,
                    builder.Options.DefaultPage.Value.Trim('/')
                    );
                app.Use(async (context, next) =>
                {
                    var currentRequestPath = context.Request.Path.Value;

                    if (currentRequestPath == requestPath1
                        || currentRequestPath == requestPath2)
                    {
                        context.Request.Path = defaultPage;
                    }

                    await next();
                });

                // staticfiles 
                app.UseStaticFiles(builder.Options.PageStaticFileOptions);
            }


            return app;
        }
    }
}
