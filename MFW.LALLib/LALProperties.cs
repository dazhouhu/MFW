using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFW.LALLib
{
    public class LALProperties
    {
        private static Dictionary<PropertyEnum, string> _properties = new Dictionary<PropertyEnum, string>();
        private static LALProperties _instance = null;
        private static ILog _log = LogManager.GetLogger("LAL::LALProperties");

        public static LALProperties GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LALProperties();
                foreach (var map in PropertyEnumMap.GetPropertiesMaps())
                {
                    _properties.Add(map.Key, string.Empty);
                }
            }
            return _instance;
        }

        /**
         *this method can be invoked by UI/Autostub (encapsulated this method in LAL) or invoked by LAL,
         *when invoked by UI/AutoStub, EX. properties updated on UI, set properties to LAL properties; 
         *when invoked by LAL, EX. LAL read properties from configuration file during initialize, LAL set properties to UI via sprite HashMap 
         * */
        public void SetProperty(PropertyEnum key, string value)
        {
            if(_properties.ContainsKey(key))
            {
                _properties[key] = value;
            }            
        }
        /**
         * get properties from Hash table properties, which is invoked by LAL
         * */
        public string GetProperty(PropertyEnum propertyEnum)
        {
            var val = _properties[propertyEnum];
            if (string.IsNullOrWhiteSpace(val))
            {
                return string.Empty;
            }
            return val;
        }


        public Dictionary<PropertyEnum, string> GetProperties()
        {
            return _properties;
        }
        
    }
}
