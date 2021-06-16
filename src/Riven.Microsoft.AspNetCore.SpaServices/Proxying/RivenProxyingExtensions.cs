// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.Extensions.Proxy;
using Microsoft.Extensions.Hosting;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for proxying requests to a local SPA development server during
    /// development. Not for use in production applications.
    /// </summary>
    public static class RivenSpaProxyingExtensions
    {
        /// <summary>
        /// Configures the application to forward incoming requests to a local Single Page
        /// Application (SPA) development server. This is only intended to be used during
        /// development. Do not enable this middleware in production applications.
        /// </summary>
        /// <param name="spaBuilder">The <see cref="IRivenSpaBuilder"/>.</param>
        /// <param name="baseUri">The target base URI to which requests should be proxied.</param>
        public static void ProxyDevServer(
            this IRivenSpaBuilder spaBuilder,
            string baseUri)
        {
            ProxyDevServer(
                spaBuilder,
                new Uri(baseUri));
        }

        /// <summary>
        /// Configures the application to forward incoming requests to a local Single Page
        /// Application (SPA) development server. This is only intended to be used during
        /// development. Do not enable this middleware in production applications.
        /// </summary>
        /// <param name="spaBuilder">The <see cref="IRivenSpaBuilder"/>.</param>
        /// <param name="baseUri">The target base URI to which requests should be proxied.</param>
        public static void ProxyDevServer(
            this IRivenSpaBuilder spaBuilder,
            Uri baseUri)
        {
            ProxyDevServer(
                spaBuilder,
                () => Task.FromResult(baseUri));
        }

        /// <summary>
        /// Configures the application to forward incoming requests to a local Single Page
        /// Application (SPA) development server. This is only intended to be used during
        /// development. Do not enable this middleware in production applications.
        /// </summary>
        /// <param name="spaBuilder">The <see cref="IRivenSpaBuilder"/>.</param>
        /// <param name="baseUriTaskFactory">A callback that will be invoked on each request to supply a <see cref="Task"/> that resolves with the target base URI to which requests should be proxied.</param>
        public static IRivenSpaBuilder ProxyDevServer(
            this IRivenSpaBuilder spaBuilder,
            Func<Task<Uri>> baseUriTaskFactory)
        {
            var app = spaBuilder.ApplicationBuilder;
            var applicationStoppingToken = GetStoppingToken(app);

            app.UseWebSockets();

            var neverTimeOutHttpClient =
               RivenSpaProxy.CreateHttpClientForProxy(Timeout.InfiniteTimeSpan);

            var requestPath1 = spaBuilder.Options.RequestPath.Value.TrimEnd('/');
            var requestPath2 = $"{requestPath1}/";

            app.Use(async (context, next) =>
            {
                var currentRequestPath = context.Request.Path.Value;
                if (!currentRequestPath.StartsWith(requestPath1)
                    && !currentRequestPath.StartsWith(requestPath2))
                {
                    await next();
                    return;
                }

                var didProxyRequest = await RivenSpaProxy.PerformProxyRequest(
                   context,
                   neverTimeOutHttpClient,
                   baseUriTaskFactory.Invoke(),
                   applicationStoppingToken,
                   proxy404s: true
                   );
            });

            return spaBuilder;
        }

        private static CancellationToken GetStoppingToken(IApplicationBuilder appBuilder)
        {
            var applicationLifetime = appBuilder
                .ApplicationServices
                .GetService(typeof(IHostApplicationLifetime));
            return ((IHostApplicationLifetime)applicationLifetime).ApplicationStopping;
        }
    }
}
