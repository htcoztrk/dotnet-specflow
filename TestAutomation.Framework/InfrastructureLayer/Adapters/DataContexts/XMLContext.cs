using TestAutomation.Framework.DomainLayer.Contracts;
using System.Xml;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.DataContexts
{
    public class XmlContext : IXmlContext
    {
        private readonly XmlDocument xmlData;
        private readonly string xmlSource;
        /// <summary>
        /// Dosya adı ve proje kaynağı parametrelerini dosya path'i oluşturan XmlContext methodu
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="projectSource"></param>
        public XmlContext(string fileName, ProjectSource projectSource)
        {
            xmlData = new XmlDocument();
            xmlData.Load(Path.Combine(Path.GetDirectoryName(typeof(XmlContext).Assembly.Location), fileName));
        }

        /// <summary>
        /// Doğrudan xmlSource (complete url) alan XmlContext methodu
        /// </summary>
        /// <param name="xmlSource"></param>
        public XmlContext(string xmlSource)
        {
            this.xmlSource = xmlSource;
            xmlData = new XmlDocument();
            xmlData.LoadXml(xmlSource);
        }
        /// <summary>
        /// Verilen xpath'e göre value çeken method.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetValueByXPath(string path)
        {
            return xmlData.SelectSingleNode(path).InnerText;
        }
        /// <summary>
        /// Verilen key'e göre value çeken method.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValueWithKey(string key)
        {
            var xDoc = XDocument.Parse(xmlSource);
            return xDoc.Root.Element(key).FirstNode.ToString();
        }
        /// <summary>
        /// Verilen obje tipine göre value çeken method.
        /// </summary>
        /// <returns></returns>
        public T Deserialize<T>()
        {
            var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("root"));
            using (var reader = new StringReader(xmlSource))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}

