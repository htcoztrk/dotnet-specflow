using System.Collections;
using TestAutomation.Framework.DomainLayer.Models.Attributes;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    [ElementType]
    public interface IWebElementList : IEnumerable {
        /// <summary>
        /// Elementi verilen index altında bulunduğunda IWebElement tipinde dönüş yapar
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IWebElement this[int index] { get; }

        /// <summary>
        /// Elementlist'teki count bilgisini int tipinde döner
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Elementlist'teki elementleri tekrar bağlanmaya zorlar
        /// </summary>
        void BindElementsForcefully();

        /// <summary>
        ///  Parametrik XPath hazırlanmasını sağlar.
        /// </summary>
        /// <param name="xpathParameters">XPath tanımında {1}, {2} vb... şekilde belirtilmiş parametrelerin, sırasıyla değer karşılıklıklarını alır.</param>
        /// <returns></returns>
        void SetXPathParameters(params string[] xpathParameters);
    }
}