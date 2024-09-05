using System;
using System.Drawing;
using TestAutomation.Framework.DomainLayer.Models.Attributes;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    [ElementType]
    public interface IWebElement : IElement {

        /// <summary>
        /// WebElement text metni içerisinde text metninin olup olmadığını 
        /// true ya da false olarak geri döner.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        bool TryMatchText(string text);

        /// <summary>
        /// Verilen süre aralıklarında harfleri tek tek girer.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="interval"></param>
        void SetTextAtIntervals(string text, TimeSpan interval);

        /// <summary>
        /// Value değeri belirtilen comboxı seçer.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="clickSelectedValue">Eğer eleman seçildikten sonra tıklanma ihtiyacı yoksa bu değer false gönderilmelidir.</param>
        void SelectComboboxByValue(string value, bool clickSelectedValue);

        /// <summary>
        /// Value değeri belirtilen comboxı seçer.
        /// </summary>
        /// <param name="value"></param>
        void SelectComboboxByValue(string value);

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve combobox içerisine değeri
        /// text bilgisi üzerinden set eder.
        /// </summary>
        /// <param name="text">Seçilecek elemanın text'i yazılır</param>
        /// <param name="clickSelectedText">Eğer eleman seçildikten sonra tıklanma ihtiyacı yoksa bu değer false gönderilmelidir.</param>
        void SelectComboboxByText(string text, bool clickSelectedText);

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve combobox içerisine değeri
        /// text bilgisi üzerinden set eder.
        /// </summary>
        /// <param name="text">Seçilecek elemanın text'i yazılır</param>
        void SelectComboboxByText(string text);

        /// <summary>
        /// Indexe göre comboboztan seçim yapar.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="clickSelectedIndex">Eğer eleman seçildikten sonra tıklanma ihtiyacı yoksa bu değer false gönderilmelidir.</param>
        void SelectComboboxByIndex(int index, bool clickSelectedIndex);

        /// <summary>
        /// Indexe göre comboboztan seçim yapar.
        /// </summary>
        /// <param name="index"></param>
        void SelectComboboxByIndex(int index);

        string GetSelectedValueTextFromCombobox();

        /// <summary>
        /// elementin konumunu verir.
        /// </summary>
        /// <returns></returns>
        Point GetLocation();

        /// <summary>
        /// Elementin size bilgisini döner.
        /// </summary>
        /// <returns></returns>
        Size GetSize();

        /// <summary>
        /// Elementin CSS değerini string tipinde döner.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        string GetCssValue(string propertyName);

        /// <summary>
        /// Elementin görünür olup olmadığını bool tipinde döner.
        /// </summary>
        /// <returns></returns>
        bool IsDisplayed();

        /// <summary>
        /// Elementin seçili olup olmadığını bool tipinde döner.
        /// </summary>
        /// <returns></returns>
        bool IsSelected();

        /// <summary>
        /// Elementin TagName değerini string tipinde döner.
        /// </summary>
        /// <returns></returns>
        string GetTagName();

        /// <summary>
        /// Elementin altında diğer elementi bulur ve döner.
        /// </summary>
        /// <returns></returns>
        IWebElement GetChild(IWebElement element);

        /// <summary>
        /// Elementin altında diğer elementleri bulur ve liste şeklinde döner.
        /// </summary>
        /// <returns></returns>
        IWebElementList GetChildList(IWebElement element);

        /// <summary>
        /// Elementi tekrar bağlanmaya zorlar
        /// </summary>
        void BindElementForcefully();

        /// <summary>
        /// Text türünde veya content-editable attribute'una sahip elemente yazıyı gönderir
        /// </summary>
        /// <param name="keys">Elemente gönderilecek yazı</param>
        void SendKeys(string keys);
    }
}
