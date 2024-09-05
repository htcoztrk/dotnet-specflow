using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TestAutomation.Framework.DomainLayer.Container;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Events.Args;
using TestAutomation.Framework.DomainLayer.Events.Handlers;
using TestAutomation.Framework.DomainLayer.Factories;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Services;
using IWinAppDriver = OpenQA.Selenium.Appium.Windows.WindowsDriver<OpenQA.Selenium.Appium.Windows.WindowsElement>;
using IWinAppElement = OpenQA.Selenium.Appium.Windows.WindowsElement;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Elements {
    internal class WinAppElement : Element, IDesktopElement, IElementInternal {
        protected IWinAppElement BindedWinAppElement;

        private Session session;

        public new Session Session
        {
            get
            {
                if (session == null) {
                    Test test = ContainerService.GetParentByElement<DriverContainer>(this);
                    session = DriverManager.GetSession(test);
                }

                return session;
            }
        }

        public new object BindedElement
        {
            get
            {
                return BindedWinAppElement ?? Init();
            }
            set
            {
                BindedWinAppElement = value as IWinAppElement;
            }
        }

        protected new IWinAppElement Init() {
            try {
                IWinAppDriver unwrappedWebDriver = (IWinAppDriver)((IAgent)Driver).GetDriver();

                bool isListElement = ListOrder > -1;

                if (ParentElement != null) {
                    ((Element)ParentElement).BindedElement =
                        ElementService.GetBindedElement((By)(((IElementInternal)ParentElement).By), (OpenQA.Selenium.IWebDriver)unwrappedWebDriver, Timeout);
                }

                else if (isListElement) {
                    BindedWinAppElement = ElementService.GetBindedElements((By)By, unwrappedWebDriver, Timeout)[ListOrder];
                }
                else {
                    BindedWinAppElement = ElementService.GetBindedElement((By)By, unwrappedWebDriver, Timeout);
                }

                return BindedWinAppElement;
            }
            catch (WebDriverTimeoutException) {
                throw new NoSuchElementException();
            }
            catch (Exception) {
                throw;
            }
        }
        public void BindElementForcefully() {
            try {
                BindedWinAppElement = null;
            }
            catch (Exception) {
                //logger.Error(ex);
            }
        }

        public string GetAttribute(string attributeType) {
            try {
                BindedElementIsPresent();
                return BindedWinAppElement.GetAttribute(attributeType);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        public IDesktopElement GetChild(IDesktopElement element) {
            try {
                BindedWinAppElement = CheckStateAndBind();
                WinAppElement childElement = (WinAppElement)ElementService.GetChildWinApp<WinAppElement>(Driver, this, element);
                return childElement;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        public IDesktopElementList GetChildList(IDesktopElement element) {
            try {
                SendSystemNotification("GetChildList Method is invoked for " + Locator.By + " element.");
                BindedWinAppElement = CheckStateAndBind();

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

        public string GetText() {
            try {
                BindedElementIsPresent();
                return BindedWinAppElement.Text;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        public bool IsEnabled() {
            try {
                BindedElementIsPresent();
                return BindedWinAppElement.Enabled;
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

        public bool IsBinded() {
            try {
                return BindedWinAppElement != null;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        public void SendKey(KeyboardKey key) {
            try {
                BindedElementIsPresent();
                BindedWinAppElement.SendKeys(KeyboardFactory.Get(key));
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        public new void SetLocatorParameters(params string[] locatorParameters) {
            Locator currentLocator = this.Locator;
            string replacedBy = OriginalLocator.By;

            for (int i = 0; i < locatorParameters.Length; i++) {
                replacedBy = replacedBy.Replace("{" + (i + 1) + "}", locatorParameters[i]);
            }
            Locator newLocator = new Locator(replacedBy);

            if (!currentLocator.By.Equals(newLocator.By)) {
                Locator = new Locator(replacedBy);
                BindedWinAppElement = null;
            }
        }

        /// <summary>
        /// Element bind edilir clickable olana kadar bekler ve text bilgisini alana set eder.
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text) {
            try {
                BindedElementIsPresent();
                BindedWinAppElement.SendKeys(text);
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
                BindedWinAppElement.Clear();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        public void Submit() {
            try {
                BindedElementIsPresent();
                BindedWinAppElement.Submit();
                SendSystemNotification("Submit Method is invoked for " + Locator.By + " element.");
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        private void BindedElementIsPresent() {
            try {
                var sessionId = Driver.Session.Id;
                BindedWinAppElement = CheckStateAndBind();
                Assert.IsTrue(IsBinded(), "Element bulunamadı!");
            }
            catch (Exception) {
                throw;
            }
        }

        protected new IWinAppElement CheckStateAndBind() {
            try {
                BindedWinAppElement = BindedWinAppElement ?? Init();

                if (BindedWinAppElement == null) {
                    return BindedWinAppElement;
                }
                else if (IsStale()) {
                    EventService.Invoke(
                        new EventOnStaleElement(),
                        new StaleElementArgs(
                            new Message("Stale Element Invoked!")),
                        this);
                }
                else {
                    return BindedWinAppElement;
                }

                return BindedWinAppElement;
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException($"Element bulunamadı.\nLocator: {Locator.By}");
            }
            catch (ArgumentNullException) {
                throw new NoSuchElementException($"Element bulunamadı.\nLocator: {Locator.By}");
            }
            catch (Exception) {
                throw;
            }
        }

        private new bool IsStale() {
            try {
                bool isStale = BindedWinAppElement.Enabled;
            }
            catch (StaleElementReferenceException) {
                return true;
            }
            catch (InvalidOperationException) {
                return true;
            }
            return false;
        }

        public void Click() {
            try {
                BindedElementIsPresent();
                BindedWinAppElement.Click();
                SendSystemNotification("Click Method is invoked for " + Locator.By + " element.");
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
                SystemNotification _notifier = new SystemNotification((DomainLayer.Contracts.IWebDriver)DriverManager.GetDriver(parent,
                                                                                DomainLayer.Models.Enums.TestEnvironment.WEBAPP));
                _notifier.TryNotifyForAction(systemMessage);
            }
            catch (Exception) {
                throw;
            }
        }

        public byte[] GetElementScreenshot() {
            try {
                BindedElementIsPresent();
                Screenshot ss = BindedWinAppElement.GetScreenshot();
                return ss.AsByteArray;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }
    }
}
