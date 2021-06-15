using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices;

using System.Collections.Generic;
using System.Text;

namespace Riven.Microsoft.AspNetCore.SpaServices
{
    /// <summary>
    /// Riven 的 SPA 构建器
    /// </summary>
    public interface IRivenSpaBuilder
    {
        /// <summary>
        /// The <see cref="IApplicationBuilder"/> representing the middleware pipeline
        /// in which the SPA is being hosted.
        /// </summary>
        IApplicationBuilder ApplicationBuilder { get; }

        /// <summary>
        /// SPA配置项
        /// </summary>
        RivenSpaOptions Options { get; }
    }
}
