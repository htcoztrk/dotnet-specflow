using TestAutomation.Framework.DomainLayer.Models.Enums;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    public interface IElement {
        /// <summary>
        /// IWebElement değişkeninin textini string tipinde döner.
        /// </summary>
        /// <returns></returns>
        string GetText();

        /// <summary>
        /// Web Elementin tıklanmasını sağlar.
        /// </summary>
        void Click();

        /// <summary>
        /// Form öğesine onay komutu gönderme işlevini üstlenir ya da Element üzerinde Enter tuşuna basmayı simüle eder
        /// </summary>
        void Submit();

        /// <summary>
        /// Element içeriğinin silinmesini sağlar.
        /// </summary>
        void Clear();

        /// <summary>
        /// Elemente "text" metnini yazar.
        /// </summary>
        /// <param name="text"></param>
        void SetText(string text);

        /// <summary>
        /// Elementin aktif olup olmadığını bool tipinde döner.
        /// </summary>
        /// <returns></returns>
        bool IsEnabled();

        /// <summary>
        /// Elementin bind edilip edilmediğini bool tipinde döner.
        /// </summary>
        /// <returns></returns>
        bool IsBinded();

        /// <summary>
        /// Elementin bulunup bulunmadığını bool tipinde döner.
        /// </summary>
        /// <returns></returns>
        bool IsPresent();

        /// <summary>
        /// "attributeType" Hyperlink öğesinin özelliklerini string tipinde döner.
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        string GetAttribute(string attributeType);

        /// <summary>
        ///  Parametrik locator hazırlanmasını sağlar.
        /// </summary>
        /// <param name="locatorParameters">Locator tanımında {1}, {2} vb... şekilde belirtilmiş parametrelerin, sırasıyla değer karşılıklıklarını alır.</param>
        /// <returns></returns>
        void SetLocatorParameters(params string[] locatorParameters);

        /// <summary>
        /// Klavye komutlarını gönderir.
        /// </summary>
        /// <param name="key"></param>
        void SendKey(KeyboardKey key);
    }
}
