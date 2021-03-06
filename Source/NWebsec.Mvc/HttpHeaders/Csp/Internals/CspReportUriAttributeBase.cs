﻿// Copyright (c) André N. Klingsheim. See License.txt in the project root for license information.

using System;
using System.Web.Mvc;
using NWebsec.Core.HttpHeaders.Configuration;
using NWebsec.Mvc.Helpers;
using NWebsec.Mvc.HttpHeaders.Internals;

namespace NWebsec.Mvc.HttpHeaders.Csp.Internals
{
    /// <summary>
    /// This class is abstract and cannot be used directly.
    /// </summary>
    public abstract class CspReportUriAttributeBase : HttpHeaderAttributeBase
    {
        private readonly CspReportUriDirectiveConfiguration _directive;
        private readonly CspConfigurationOverrideHelper _configurationOverrideHelper;
        private readonly HeaderOverrideHelper _headerOverrideHelper;

        protected CspReportUriAttributeBase()
        {
            _directive = new CspReportUriDirectiveConfiguration { Enabled = true };
            _configurationOverrideHelper = new CspConfigurationOverrideHelper();
            _headerOverrideHelper = new HeaderOverrideHelper();
        }

        internal sealed override string ContextKeyIdentifier
        {
            get { return ReportOnly ? "CspReportOnly" : "Csp"; }
        }

        /// <summary>
        /// Gets or sets whether the report-uri directive is enabled in the CSP header. The default is true.
        /// </summary>
        public bool Enabled { get { return _directive.Enabled; } set { _directive.Enabled = value; } }
        /// <summary>
        /// Gets or sets whether the URI for the built in CSP report handler should be included in the directive. The default is false.
        /// </summary>
        public bool EnableBuiltinHandler { get { return _directive.EnableBuiltinHandler; } set { _directive.EnableBuiltinHandler = value; } }
        /// <summary>
        /// Gets or sets custom report URIs for the directive. Report URIs are separated by exactly one whitespace, and they must be relative URIs.
        /// </summary>
        public string ReportUris
        {
            get { return String.Join(" ", _directive.ReportUris); }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("ReportUris cannot be set to null or an empty string.");
                if (value.StartsWith(" ") || value.EndsWith(" "))
                    throw new ArgumentException("ReportUris must not contain leading or trailing whitespace: " + value);
                if (value.Contains("  "))
                    throw new ArgumentException("ReportUris must be separated by exactly one whitespace: " + value);

                _directive.ReportUris = value.Split(' ');
            }
        }

        protected abstract bool ReportOnly { get; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_directive.Enabled && !_directive.EnableBuiltinHandler && _directive.ReportUris == null)
            {
                throw new ApplicationException("You need to either set EnableBuiltinHandler to true, or supply at least one Reporturi to enable the reporturi directive.");
            }

            _configurationOverrideHelper.SetCspReportUriOverride(filterContext.HttpContext, _directive, ReportOnly);
            base.OnActionExecuting(filterContext);
        }

        public sealed override void SetHttpHeadersOnActionExecuted(ActionExecutedContext filterContext)
        {
            _headerOverrideHelper.SetCspHeaders(filterContext.HttpContext, ReportOnly);
        }
    }
}
