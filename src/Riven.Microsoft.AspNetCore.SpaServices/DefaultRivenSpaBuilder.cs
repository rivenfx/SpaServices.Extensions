using Microsoft.AspNetCore.Builder;

using System;

namespace Riven.Microsoft.AspNetCore.SpaServices
{
    public class DefaultRivenSpaBuilder : IRivenSpaBuilder
    {
        public IApplicationBuilder ApplicationBuilder { get; }

        public RivenSpaOptions Options { get; }

        public DefaultRivenSpaBuilder(IApplicationBuilder applicationBuilder, RivenSpaOptions options)
        {
            ApplicationBuilder = applicationBuilder
                ?? throw new ArgumentNullException(nameof(applicationBuilder));

            Options = options
                ?? throw new ArgumentNullException(nameof(options));
        }
    }
}
