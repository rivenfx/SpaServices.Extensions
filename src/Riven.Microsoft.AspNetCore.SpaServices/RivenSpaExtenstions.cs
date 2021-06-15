using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Riven.Microsoft.AspNetCore.SpaServices;

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

            // dev环境使用代理
            var env = app.ApplicationServices.GetService<IHostEnvironment>();
            if (env != null
                && env.IsDevelopment()
                && builder.Options.DevServer != null)
            {
                builder.ProxyDevServer(builder.Options.DevServer);
            }

            // 非dev环境使用静态文件目录
            if (env != null
                && !env.IsDevelopment()
                && builder.Options.PageStaticFileOptions != null)
            {
                app.UseStaticFiles(builder.Options.PageStaticFileOptions);
            }


            return app;
        }
    }
}
