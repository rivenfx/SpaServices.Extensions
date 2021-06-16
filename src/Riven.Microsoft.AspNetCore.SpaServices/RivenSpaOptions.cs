using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using System;

namespace Microsoft.AspNetCore.SpaServices
{
    public class RivenSpaOptions
    {
        private PathString _defaultPage = "/index.html";

        private PathString _requestPath = "/";

        /// <summary>
        /// 请求的地址,默认为 "/"
        /// </summary>
        public virtual PathString RequestPath
        {
            get => _requestPath;
            set
            {
                if (string.IsNullOrEmpty(value.Value))
                {
                    throw new ArgumentException($"The value for {nameof(RequestPath)} cannot be null or empty.");
                }

                _requestPath = value;
            }
        }

        /// <summary>
        /// 默认页面,默认为 "index.html"
        /// </summary>
        public virtual PathString DefaultPage
        {
            get => _defaultPage;
            set
            {
                if (string.IsNullOrEmpty(value.Value))
                {
                    throw new ArgumentException($"The value for {nameof(DefaultPage)} cannot be null or empty.");
                }

                _defaultPage = value;
            }
        }

        /// <summary>
        /// 静态文件路径配置
        /// </summary>
        public virtual StaticFileOptions PageStaticFileOptions { get; set; }
    }
}
