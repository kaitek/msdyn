using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Msdyn.Plugins.Core
{
    [Serializable, XmlType("setting")]
    public class ConfigSetting
    {
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public string value { get; set; }

        public ConfigSetting()
        {

        }
    }

    [Serializable, XmlRoot("config"), XmlType("config")]
    public class PluginConfiguration
    {
        private Dictionary<string, string> _settings;

        public PluginConfiguration()
        {
            _settings = new Dictionary<string, string>();
        }

        public PluginConfiguration(string configXml)
        {
            if (string.IsNullOrWhiteSpace(configXml))
                return;

            XmlSerializer serializer = new XmlSerializer(typeof(ConfigSetting[]),
                                 new XmlRootAttribute() { ElementName = "config" });
            StringReader reader = new StringReader(configXml);
            _settings = ((ConfigSetting[])serializer.Deserialize(reader))
               .ToDictionary(i => i.name, i => i.value);
        }

        public bool GetAsBool(string name)
        {
            try
            {
                if (_settings.ContainsKey(name))
                {
                    return Convert.ToBoolean(_settings[name]);
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        public string this[string name]
        {
            get
            {
                if (_settings.ContainsKey(name))
                {
                    return _settings[name];
                }
                else
                {
                    throw new InvalidOperationException($"Cannot find plugin configuration entry '{name}'");
                }
            }
        }
    }
}
