using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;


namespace WcfHost
{
    public class MyConfig : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            NameValueCollection configs;
            NameValueSectionHandler baseHandler = new NameValueSectionHandler();
            configs = (NameValueCollection)baseHandler.Create(parent, configContext, section);
            return configs;
        }
    }
}