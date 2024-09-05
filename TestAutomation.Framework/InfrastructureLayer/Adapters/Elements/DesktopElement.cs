using System;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.InfrastructureLayer.Services;
using NUnit.Framework;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.DomainLayer.Container;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Factories;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Elements {
    /// <summary>
    /// Desktop uygulamaları için kullanılan elementlerin methodlarını bulundurur.
    /// </summary>
    internal class DesktopElement : Element, IDesktopElement {


        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve text bilgisini döner.
        /// </summary>
        /// <returns></returns>
        public string GetText() {
            try {
                SendSystemNotification("GetText Method is invoked for " + Locator.By + " element.");

                BindedElementIsPresent();

                return BoundElement.Text;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }

        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve click eder.
        /// </summary>
        public void Click() {
            try {
                SendSystemNotification("Click Method is invoked for " + Locator.By + " element.");

                BindedElementIsPresent();

                BoundElement.Click();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve submit edilir.
        /// </summary>
        public void Submit() {
            try {
                SendSystemNotification("Submit Method is invoked for " + Locator.By + " element.");

                BindedElementIsPresent();

                BoundElement.Submit();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve text bilgisini alana set eder.
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text) {
            try {
                SendSystemNotification("SetText Method is invoked for " + Locator.By + " element.");

                BindedElementIsPresent();

                BoundElement.SendKeys(text);
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
                SendSystemNotification("Clear Method is invoked for " + Locator.By + " element.");

                BindedElementIsPresent();

                BoundElement.Clear();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve enable bilgisini bool olarak döner.
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
        /// Element bind edilir clickable olana kadar bekler ve attribute olarak istenen bilgiyi döner.
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public string GetAttribute(string attributeType) {
            try {
                SendSystemNotification("GetAttribute Method is invoked for " + Locator.By + " element.");

                BindedElementIsPresent();

                return BoundElement.GetAttribute(attributeType);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        private void SendSystemNotification(string message) {
            try {
                Test parent = ContainerService.GetParentByElement<DriverContainer>(Driver);

                Message systemMessage = new Message(message);

                SystemNotification _notifier = new SystemNotification((IWebDriver)DriverManager.GetDriver(parent,
                                                                                DomainLayer.Models.Enums.TestEnvironment.WEBAPP));
                _notifier.TryNotifyForAction(systemMessage);
            }
            catch (Exception) {
                throw;
            }
        }

        private void BindedElementIsPresent() {
            try {
                BoundElement = CheckStateAndBind();

                Assert.IsTrue(IsBinded(), "Element bulunamadı!");
            }
            catch (Exception) {
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
                throw;
            }
        }

        /// <summary>
        /// Elementin bulunup bulunmadığı bilgisine göre bool değer döner. 
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
        /// Element bind edilir elementin hiyerarşik olarak altındaki child elementleri döner.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public IDesktopElement GetChild(IDesktopElement element) {
            try {
                SendSystemNotification("GetChild Method is invoked for " + Locator.By + " element.");

                BoundElement = CheckStateAndBind();

                DesktopElement childElement = (DesktopElement)ElementService.GetChild<DesktopElement>(Driver, this, element);

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
        public IDesktopElementList GetChildList(IDesktopElement element) {
            try {
                SendSystemNotification("GetChildList Method is invoked for " + Locator.By + " element.");

                BoundElement = CheckStateAndBind();

                DesktopElementList childList =
                    (DesktopElementList)ElementService.
                    GetChildList<DesktopElement, DesktopElementList>(this, element);

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
                SendSystemNotification("BindElementForcefully Method is invoked for " + Locator.By + " element.");

                Init();
            }
            catch (Exception) {
                //logger.Error(ex);
            }
        }


        public void SendKey(KeyboardKey key) {
            try {
                SendSystemNotification("SendKey Method is invoked for " + Locator.By + " element.");
                BindedElementIsPresent();
                BoundElement.SendKeys(KeyboardFactory.Get(key));
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        public byte[] GetElementScreenshot() {
            throw new NotSupportedException();
        }
    }
}