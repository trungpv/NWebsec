﻿// Copyright (c) André N. Klingsheim. See License.txt in the project root for license information.

using System;
using System.Web.Mvc;
using NWebsec.Core.HttpHeaders.Configuration;
using NWebsec.HttpHeaders;
using NWebsec.Mvc.Helpers;
using NWebsec.Mvc.HttpHeaders.Internals;

namespace NWebsec.Mvc.HttpHeaders
{
    /// <summary>
    /// Specifies whether the X-Xss-Protection security header should be set in the HTTP response.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class XXssProtectionAttribute : HttpHeaderAttributeBase
    {
        private readonly XXssProtectionConfiguration _config;
        private readonly HeaderConfigurationOverrideHelper _headerConfigurationOverrideHelper;
        private readonly HeaderOverrideHelper _headerOverrideHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="XXssProtectionAttribute"/> class
        /// </summary>
        public XXssProtectionAttribute()
        {
            _config = new XXssProtectionConfiguration { Policy = XXssProtectionPolicy.FilterEnabled, BlockMode = true };
            _headerConfigurationOverrideHelper = new HeaderConfigurationOverrideHelper();
            _headerOverrideHelper = new HeaderOverrideHelper();
        }

        /// <summary>
        /// Gets or sets whether the X-Xss-Protection security header should be set in the HTTP response.
        /// Possible values are: Disabled, FilterDisabled, FilterEnabled. The default is FilterEnabled.
        /// </summary>
        public XXssProtectionPolicy Policy
        {
            get { return _config.Policy; }
            set { _config.Policy = value; }
        }

        /// <summary>
        /// Gets or sets whether to enable the IE XSS filter block mode. This setting only takes effect when the Policy is set to FilterEnabled.
        /// The default is true.
        /// </summary>
        public bool BlockMode
        {
            get { return _config.BlockMode; }
            set { _config.BlockMode = value; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _headerConfigurationOverrideHelper.SetXXssProtectionOverride(filterContext.HttpContext, _config);
            base.OnActionExecuting(filterContext);
        }

        public override void SetHttpHeadersOnActionExecuted(ActionExecutedContext filterContext)
        {
            _headerOverrideHelper.SetXXssProtectionHeader(filterContext.HttpContext);
        }
    }
}
