using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MFW.LALLib
{

    public class ConfigurationManager
    {
        private static readonly string ROOT = "MFJava";
        private static readonly string ROOTCOMMON = "MFWJavaCommon";
        private static XDocument doc = null;
        private static XDocument docCommon = null;
        private static XElement rootElement = null;
        private static XElement rootElementCommon = null;
        private static ConfigurationManager instance = null;
        private static ILog log = LogManager.GetLogger("LAL: ConfigurationManager");


        private ConfigurationManager()
        {
            
        }

        public static ConfigurationManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ConfigurationManager();
            }
            return instance;
        }

        public void LoadFromXML(string configFile, string commonFile)
        {
            try
            {
                doc = XDocument.Load(configFile);
                rootElement = doc.Root; // root
                if (rootElement.Name != ROOT)
                    throw new Exception("配置文档格式错误");
                docCommon = XDocument.Load(commonFile);
                rootElementCommon = docCommon.Root; // root
                if (rootElementCommon.Name != ROOTCOMMON)
                    throw new Exception("公共配置文档格式错误");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }
        public string GetPropertyFromConfigFile(PropertyEnum propertyEnum)
        {
            try
            {
                var eleName = PropertyEnumMap.GetPropertyEnumText(propertyEnum);
                if (null == rootElement)
                {
                    throw new Exception("getPropertyFromConfigFile: root Element is NULL");
                }
                if (null == rootElementCommon)
                {
                    throw new Exception("getPropertyFromCommonFile: root Element is NULL");
                }
                var pElement = rootElement.Element(eleName);
                if (null == pElement)
                {
                    pElement = rootElementCommon.Element(eleName);
                }
                if (null == pElement)
                {
                    return null;
                }
                return pElement.Value;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }
    }
}
