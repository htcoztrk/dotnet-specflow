using System;
using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Factories;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions;
using TestAutomation.Framework.InfrastructureLayer.Services;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Elements {
    internal class WebElement : Element, IWebElement {


        private void BindedElementIsPresent() {
            BoundElement = CheckStateAndBind();

            WaitService.Wait(new ConditionOfElementIsVisible((IAgent)Driver, this));

            Assert.IsTrue(IsBinded(), "Element bulunamadı!");
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve text bilgisini döner.
        /// </summary>
        /// <returns></returns>
        public string GetText() {
            try {
                BindedElementIsPresent();

                return BoundElement.Text;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve attribute olarak istenen bilgiyi döner.
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public string GetAttribute(string attributeType) {
            try {
                BindedElementIsPresent();

                return BoundElement.GetAttribute(attributeType);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve click edilir.
        /// </summary>
        public void Click() {
            try {
                BindedElementIsPresent();

                BoundElement.Click();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve text bilgisini elemenete set eder.
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text) {
            try {
                BindedElementIsPresent();

                BoundElement.SendKeys(text);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve text bilgisini elemenete set ederken
        /// her bir char arasında bekleyerek işlemini gerçekleştirir.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="interval"></param>
        public void SetTextAtIntervals(string text, TimeSpan interval) {
            try {
                BoundElement = CheckStateAndBind();

                WaitService.Wait(
                    new ConditionOfPageLoaded((IAgent)Driver));

                Assert.IsTrue(IsBinded(), "Element sayfada bulunmuyor!");

                char[] textArr = text.ToCharArray();

                for (int i = 0; i < textArr.Length; i++) {
                    ((IWebDriver)Driver).ForceWait(interval);
                    BoundElement.SendKeys(textArr[i].ToString());
                }
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve combobox içerisine değeri
        /// value bilgisi üzerinden set eder.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="clickSelectedValue">Eğer eleman seçildikten sonra tıklanma ihtiyacı yoksa bu değer false gönderilmelidir.</param>
        public void SelectComboboxByValue(string value, bool clickSelectedValue) {
            BindedElementIsPresent();

            try {
                var selectElement = new SelectElement(BoundElement);
                selectElement.SelectByValue(value);
                if (clickSelectedValue)
                    selectElement.SelectedOption.Click();
            }
            catch (Exception) {
                //logger.Error(ex);

                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve combobox içerisine değeri
        /// value bilgisi üzerinden set eder.
        /// </summary>
        /// <param name="value"></param>
        public void SelectComboboxByValue(string value) {
            SelectComboboxByValue(value, true);
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve combobox içerisine değeri
        /// text bilgisi üzerinden set eder.
        /// </summary>
        /// <param name="text">Seçilecek elemanın text'i yazılır</param>
        /// <param name="clickSelectedText">Eğer eleman seçildikten sonra tıklanma ihtiyacı yoksa bu değer false gönderilmelidir.</param>
        public void SelectComboboxByText(string text, bool clickSelectedText) {
            BindedElementIsPresent();

            try {
                var selectElement = new SelectElement(BoundElement);
                selectElement.SelectByText(text);
                if (clickSelectedText)
                    selectElement.SelectedOption.Click();
            }
            catch (Exception) {
                //logger.Error(ex);

                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve combobox içerisine değeri
        /// text bilgisi üzerinden set eder.
        /// </summary>
        /// <param name="text"></param>
        public void SelectComboboxByText(string text) {
            SelectComboboxByText(text, true);
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve combobox içerisine set adilmiş text bilgisini döner.
        /// </summary>
        /// <returns></returns>
        public string GetSelectedValueTextFromCombobox() {
            try {
                BindedElementIsPresent();
                var selectElement = new SelectElement(BoundElement);
                return selectElement.SelectedOption.Text;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve combobox içerisine değeri
        /// index bilgisi üzerinden set eder.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="clickSelectedIndex">Eğer eleman seçildikten sonra tıklanma ihtiyacı yoksa bu değer false gönderilmelidir.</param>
        public void SelectComboboxByIndex(int index, bool clickSelectedIndex) {
            try {
                BindedElementIsPresent();
                SelectElement selectElement = new SelectElement(BoundElement);
                selectElement.SelectByIndex(index);
                if (clickSelectedIndex)
                    selectElement.SelectedOption.Click();
            }
            catch (Exception) {
                //logger.Error(ex);

                throw;
            }
        }

        public void SelectComboboxByIndex(int index) {
            SelectComboboxByIndex(index, true);
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve element submit edilir.
        /// </summary>
        public void Submit() {
            try {
                BindedElementIsPresent();

                BoundElement.Submit();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve elementin içerisindeki text temizlenir.
        /// </summary>
        public void Clear() {
            try {
                BindedElementIsPresent();

                BoundElement.Clear();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve location bilgisini döner.
        /// </summary>
        /// <returns></returns>
        public Point GetLocation() {
            try {
                BindedElementIsPresent();

                return BoundElement.Location;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve size bilgisini döner.
        /// </summary>
        /// <returns></returns>
        public Size GetSize() {
            try {
                BindedElementIsPresent();

                return BoundElement.Size;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve display özelliğinin value değerini döner.
        /// </summary>
        /// <returns></returns>
        public bool IsDisplayed() {
            try {
                BindedElementIsPresent();

                return BoundElement.Displayed;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }

        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve enable özelliğinin valu değerini döner.
        /// </summary>
        /// <returns></returns>
        public bool IsEnabled() {
            try {
                BindedElementIsPresent();

                return BoundElement.Enabled;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve seçili olup olmadığının
        /// özelliğini döner.
        /// </summary>
        /// <returns></returns>
        public bool IsSelected() {
            try {
                BindedElementIsPresent();

                return BoundElement.Selected;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve css value değerini 
        /// belirtilen propertyi ye göre döner.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetCssValue(string propertyName) {
            try {
                BindedElementIsPresent();

                return BoundElement.GetCssValue(propertyName);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve tag name bilgisini verir.
        /// </summary>
        /// <returns></returns>
        public string GetTagName() {
            try {
                BindedElementIsPresent();

                return BoundElement.TagName;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Elementin bulunup bulunmadığı bilgisine göre bool değer döner. 
        /// </summary>
        /// <returns></returns>
        public bool IsPresent() {
            try {
                BindedElementIsPresent();

                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Elementin bind edilip edilmedigine ait bool değer döner. 
        /// </summary>
        /// <returns></returns>
        public bool IsBinded() {
            try {
                return BoundElement != null;
            }
            catch (Exception) {
                //logger.Error(ex);

                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve elementin text bilgisi ile 
        /// gönderilen text karşılaştırılarak sonucu döner.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool TryMatchText(string text) {
            try {
                BoundElement = CheckStateAndBind();

                WaitService.Wait(
                    new ConditionOfTextToBePresentInElement(
                        (IAgent)Driver, this, text));

                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Element bind edilir visible olana kadar bekler ve tuş basışını elemente gönderir
        /// </summary>
        /// <param name="key">Elemente gönderilecek tuş</param>
        public void SendKey(KeyboardKey key) {
            try {
                BindedElementIsPresent();
                BoundElement.SendKeys(KeyboardFactory.Get(key));
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element visible olana kadar bekler ve Text türünde veya content-editable attribute'una sahip element'e yazıyı gönderir
        /// </summary>
        /// <param name="keys">Elemente gönderilecek yazı</param>
        public void SendKeys(string keys) {
            try {
                BindedElementIsPresent();
                BoundElement.SendKeys(keys);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir elementin hiyerarşik olarak altındaki child elementleri döner.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public IWebElement GetChild(IWebElement element) {
            try {
                BoundElement = CheckStateAndBind();
                IWebElement childElement = (IWebElement)ElementService.GetChild<WebElement>(Driver, this, element);
                return childElement;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir elementin hiyerarşik olarak altındaki child element listleri döner.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public IWebElementList GetChildList(IWebElement element) {
            try {
                BoundElement = CheckStateAndBind();
                WebElementList childList =
                    (WebElementList)ElementService.
                    GetChildList<WebElement, WebElementList>(this, element);

                return childList;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Element daha önceden bind edilmiş olabilir. Test aşamasında sorunla karşılaşmamak için
        /// element temizlenir ve tekrardan bind edilir.
        /// </summary>
        public void BindElementForcefully() {
            try {
                Init();
            }
            catch (Exception) {
                //logger.Error(ex);
            }
        }
    }
}
