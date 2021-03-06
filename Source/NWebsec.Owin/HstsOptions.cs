﻿// Copyright (c) André N. Klingsheim. See License.txt in the project root for license information.

using System;

namespace NWebsec.Owin
{
    public class HstsOptions : HstsOptionsConfiguration, IFluentHstsOptions
    {

        // ReSharper disable once CSharpWarnings::CS0109
        public new IFluentHstsOptions MaxAge(int days = 0, int hours = 0, int minutes = 0, int seconds = 0)
        {
            if (days < 0) throw new ArgumentOutOfRangeException("days", "Value must be equal to or larger than 0.");
            if (hours < 0) throw new ArgumentOutOfRangeException("hours", "Value must be equal to or larger than 0.");
            if (minutes < 0) throw new ArgumentOutOfRangeException("minutes", "Value must be equal to or larger than 0.");
            if (seconds < 0) throw new ArgumentOutOfRangeException("seconds", "Value must be equal to or larger than 0.");

            base.MaxAge = new TimeSpan(days, hours, minutes, seconds);
            return this;
        }

        public new IFluentHstsOptions IncludeSubdomains()
        {
            base.IncludeSubdomains = true;
            return this;
        }

        public new IFluentHstsOptions Preload()
        {
            base.Preload = true;
            return this;
        }

        public new IFluentHstsOptions HttpsOnly()
        {
            base.HttpsOnly = true;
            return this;
        }
    }
}