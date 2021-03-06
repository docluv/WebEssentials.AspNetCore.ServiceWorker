﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace WebEssentials.AspNetCore.ServiceWorker
{
    internal class ServiceWorkerTagHelperComponent : TagHelperComponent
    {
        private const string _script = "\r\n\t<script>'serviceWorker'in navigator&&navigator.serviceWorker.register('/serviceworker.js')</script>";

        private IHostingEnvironment _env;
        private IHttpContextAccessor _accessor;
        private ServiceWorkerOptions _options;

        public ServiceWorkerTagHelperComponent(IHostingEnvironment env, IHttpContextAccessor accessor, ServiceWorkerOptions options)
        {
            _env = env;
            _accessor = accessor;
            _options = options;
        }

        /// <inheritdoc />
        public override int Order => 100;

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!_options.RegisterServiceWorker)
            {
                return;
            }

            if (string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase))
            {
                if (_accessor.HttpContext.Request.IsHttps || _env.IsDevelopment())
                {
                    output.PostContent.AppendHtml(_script);
                }
            }
        }
    }
}
