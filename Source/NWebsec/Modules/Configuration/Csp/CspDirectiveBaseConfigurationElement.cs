// Copyright (c) Andr� N. Klingsheim. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NWebsec.Core.HttpHeaders.Configuration;

namespace NWebsec.Modules.Configuration.Csp
{
    public class CspDirectiveBaseConfigurationElement : ConfigurationElement, ICspDirectiveConfiguration
    {
        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = true)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
            set
            {
                this["enabled"] = value;
            }
        }

        [ConfigurationProperty("none", IsRequired = false, DefaultValue = false)]
        public bool NoneSrc
        {
            get
            {
                return (bool)this["none"];
            }
            set
            {
                this["none"] = value;
            }
        }

        [ConfigurationProperty("self", IsRequired = false, DefaultValue = false)]
        public bool SelfSrc
        {
            get
            {
                return (bool)this["self"];
            }
            set
            {
                this["self"] = value;
            }
        }

        public IEnumerable<string> CustomSources
        {
            get
            {
                return Sources.Cast<CspSourceConfigurationElement>().Select(s => s.Source);
            }
            set { throw new NotImplementedException(); }
        }

        [ConfigurationProperty("", IsRequired = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(CspSourcesElementCollection), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
        public CspSourcesElementCollection Sources
        {
            get
            {
                return (CspSourcesElementCollection)base[""];
            }
            set
            {
                base[""] = value;
            }
        }

    }
}
