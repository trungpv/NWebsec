﻿// Copyright (c) André N. Klingsheim. See License.txt in the project root for license information.

using System.Configuration;
using NUnit.Framework;

namespace NWebsec.Tests.Functional
{
    [TestFixture]
    public class Mvc5Tests : MvcTestsBase
    {
        protected override string BaseUri
        {
            get { return ConfigurationManager.AppSettings["Mvc5BaseUri"]; }
        }
    }
}