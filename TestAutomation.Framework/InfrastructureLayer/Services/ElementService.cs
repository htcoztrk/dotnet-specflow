using TestAutomation.Framework.DomainLayer.Builders;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.InfrastructureLayer.Utils;
using System.Collections.Generic;
using OpenQA.Selenium;
using System;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;
using ISeleniumWebElement = OpenQA.Selenium.IWebElement;
using IWinAppElement = OpenQA.Selenium.Appium.Windows.WindowsElement;
using IWinAppDriver = OpenQA.Selenium.Appium.Windows.WindowsDriver<OpenQA.Selenium.Appium.Windows.WindowsElement>;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Elements;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions;
using OpenQA.Selenium.Appium;

namespace TestAutomation.Framework.InfrastructureLayer.Services {
    internal static class ElementService {
        #region Selenium Get Binded Element
        public static ISeleniumWebElement GetBindedElement(By by, ISeleniumWebDriver driver) {
            ISeleniumWebElement bindedElement = null;
            bindedElement = driver.FindElement(by);

            return bindedElement;
        }
        public static ISeleniumWebElement GetBindedElement(By by, ISeleniumWebDriver driver, TimeSpan timeout) {
            ISeleniumWebElement bindedElement = null;
            WaitService.Wait(new ConditionOfElementIsVisible(driver, by), timeout);
            bindedElement = driver.FindElement(by);

            return bindedElement;
        }
        #endregion

        #region Selenium Get Binded Elements
        public static IList<ISeleniumWebElement> GetBindedElements(By by, ISeleniumWebDriver driver) {
            IList<ISeleniumWebElement> bindedElements = null;

            try {
                bindedElements = driver.FindElements(by);
            }
            catch {
                bindedElements = null;
            }

            return bindedElements;
        }
        public static IList<ISeleniumWebElement> GetBindedElements(By by, ISeleniumWebDriver driver, TimeSpan timeout) {
            IList<ISeleniumWebElement> bindedElements = null;

            try {
                WaitService.Wait(new ConditionOfElementIsVisible(driver, by), timeout);
                bindedElements = driver.FindElements(by);
            }
            catch {
                bindedElements = null;
            }

            return bindedElements;
        }
        #endregion

        #region WinApp Get Binded Element
        public static IWinAppElement GetBindedElement(By by, IWinAppDriver driver) {
            IWinAppElement bindedElement = null;
            bindedElement = driver.FindElement(by);

            return bindedElement;
        }
        public static IWinAppElement GetBindedElement(By by, IWinAppDriver driver, TimeSpan timeout) {
            IWinAppElement bindedElement = null;
            bindedElement = driver.FindElement(by);

            return bindedElement;
        }
        #endregion

        #region WinApp Get Binded Elements
        public static IList<IWinAppElement> GetBindedElements(By by, IWinAppDriver driver) {
            IList<IWinAppElement> bindedElements = null;

            try {
                bindedElements = driver.FindElements(by);
            }
            catch {
                bindedElements = null;
            }

            return bindedElements;
        }
        public static IList<IWinAppElement> GetBindedElements(By by, IWinAppDriver driver, TimeSpan timeout) {
            IList<IWinAppElement> bindedElements = null;

            try {
                bindedElements = driver.FindElements(by);
            }
            catch {
                bindedElements = null;
            }

            return bindedElements;
        }
        #endregion

        #region Selenium Get Binded Child Element
        public static ISeleniumWebElement GetBindedChildElement(ISeleniumWebDriver driver, IElementInternal parentElement, IElementInternal childElement) {
            ISeleniumWebElement bindedElement = null;

            By childElementLocator = LocatorConverter.ToBy(childElement.Locator);

            try {
                WaitParent(driver, (Element)parentElement);
                bindedElement =
                ((ISeleniumWebElement)parentElement.BindedElement).FindElement(childElementLocator);
                return bindedElement;
            }
            catch {
                throw new NoSuchElementException((nameof(childElement) + ": Child element bulunamadı."));
            }
        }
        #endregion

        #region WinApp Get Binded Child Element
        public static IWinAppElement GetBindedChildElement(IWinAppDriver driver, IElementInternal parentElement, IElementInternal childElement) {
            AppiumWebElement bindedElement = null;

            By childElementLocator = LocatorConverter.ToBy(childElement.Locator);

            try {
                WaitParent(driver, (Element)parentElement);
                bindedElement =
                ((IWinAppElement)parentElement.BindedElement).FindElement(childElementLocator);
            }
            catch { }

            return (IWinAppElement)bindedElement;
        }
        #endregion

        #region Selenium Get Binded Child Elements
        public static IList<ISeleniumWebElement> GetBindedChildElements(ISeleniumWebDriver driver, IElementInternal parentElement, IElementInternal childElement) {
            IList<ISeleniumWebElement> bindedElements = null;

            By childElementLocator = LocatorConverter.ToBy(childElement.Locator);

            try {
                WaitParent(driver, (Element)parentElement);
                bindedElements =
                ((ISeleniumWebElement)parentElement.BindedElement).FindElements(childElementLocator);
            }
            catch { }

            return bindedElements;
        }
        #endregion

        #region WinApp Get Binded Child Elements
        public static IList<IWinAppElement> GetBindedChildElements(IWinAppDriver driver, IElementInternal parentElement, IElementInternal childElement) {
            IList<AppiumWebElement> bindedElements = null;

            By childElementLocator = LocatorConverter.ToBy(childElement.Locator);

            try {
                WaitParent(driver, (Element)parentElement);
                IWinAppElement el = (IWinAppElement)parentElement.BindedElement;
                bindedElements = el.FindElements(childElementLocator);

            }
            catch { }

            return (IList <IWinAppElement>)bindedElements;
        }
        #endregion

        /*
         * DesktopElement'te child elementleri beklemek bütün ekranın tamamında childi terar aramasıne sebep oluyordu.
         * Bu sebepten bu waite çok da ihtiyaç olmadığı düşünüldü. 
         * En tepedeki parentı bekleyerek bir derece bu beklemeyi sağlamak hedeflendi.
         * WebElement içinse çok uzun bir parent child ilişkisi genelde kurulmadığı için bu durumun bir zararı olmayıp 
         * büyük oranda doğru bir bekleme yapacağı düşünüldü.
         */
        private static void WaitParent(ISeleniumWebDriver driver, Element childElement) {
            if (childElement.ParentElement == null) {
                return;
            }
            WaitParent(driver, (Element)childElement.ParentElement);

        }
        private static void WaitParent(IWinAppDriver driver, Element childElement) {
            if (childElement.ParentElement == null) {
                return;
            }
            WaitParent(driver, (Element)childElement.ParentElement);

        }
        public static T GetElementProxy<T>(IDriver driver, Locator locator, TimeSpan timeout)
            where T : IElementInternal, new() {
            ElementBuilder<T> _elementBuilder = new ElementBuilder<T>();
            _elementBuilder.SetDriver(driver);
            _elementBuilder.SetLocator(locator);
            _elementBuilder.SetTimeout(timeout);
            _elementBuilder.InvokeRegisterEvents();

            return (T)_elementBuilder.Get();
        }
        public static T GetElementListProxy<T>(IDriver driver, Locator locator, TimeSpan timeout)
            where T : IElementListInternal, new() {
            ElementListBuilder<T> _elementListBuilder = new ElementListBuilder<T>();
            _elementListBuilder.SetDriver(driver);
            _elementListBuilder.SetLocator(locator);
            _elementListBuilder.SetTimeout(timeout);
            _elementListBuilder.InvokeRegisterEvents();

            return (T)_elementListBuilder.Get();
        }
        public static List<T> GetElementList<T>(IElementListInternal list, IList<ISeleniumWebElement> bindedList)
            where T : IElementInternal, new() {
            List<T> elementList = new List<T>();

            int index = 0;

            foreach (ISeleniumWebElement el in bindedList) {
                IElementInternal subElement = new T();

                subElement.Locator = list.Locator;
                subElement.Driver = list.Driver;
                subElement.BindedElement = el;
                subElement.ListOrder = index++;
                subElement.Timeout = list.Timeout;
                elementList.Add((T)subElement);
            }

            return elementList;
        }
        public static List<T> GetElementList<T>(IElementListInternal list, IList<IWinAppElement> bindedList)
            where T : IElementInternal, new() {
            List<T> elementList = new List<T>();

            int index = 0;

            foreach (IWinAppElement el in bindedList) {
                IElementInternal subElement = new T();

                subElement.Locator = list.Locator;
                subElement.Driver = list.Driver;
                subElement.BindedElement = el;
                subElement.ListOrder = index++;
                subElement.Timeout = list.Timeout;
                elementList.Add((T)subElement);
            }

            return elementList;
        }
        public static IElementInternal GetChild<T>(IDriver driver, IElement parentElement, IElement subElement) where T : IElementInternal, new() {
            ISeleniumWebDriver unwrappedWebDriver =
               (ISeleniumWebDriver)((IAgent)driver).GetDriver();

            IElementInternal childElement = GetElementProxy<T>(
                                            driver,
                                            ((IElementInternal)subElement).Locator, ((IElementInternal)subElement).Timeout);

            childElement.BindedElement = GetBindedChildElement(unwrappedWebDriver, (IElementInternal)parentElement, (IElementInternal)subElement);

            childElement.ParentElement = parentElement;

            return childElement;
        }
        public static IElementInternal GetChildWinApp<T>(IDriver driver, IElement parentElement, IElement subElement) where T : IElementInternal, new() {
            IWinAppDriver unwrappedWebDriver =
               (IWinAppDriver)((IAgent)driver).GetDriver();

            IElementInternal childElement = GetElementProxy<T>(
                                            driver,
                                            ((IElementInternal)subElement).Locator, ((IElementInternal)subElement).Timeout);

            childElement.BindedElement = GetBindedChildElement(unwrappedWebDriver, (IElementInternal)parentElement, (IElementInternal)subElement);

            childElement.ParentElement = parentElement;

            return childElement;
        }
        public static IElementListInternal GetChildList<T, M>(IElement parentElement, IElement subElement)
            where T : IElementInternal, new()
            where M : IElementListInternal, new() {
            IList<T> childElementList = new List<T>();

            IList<ISeleniumWebElement> elementList =
                        GetBindedChildElements(
                            (ISeleniumWebDriver)((IAgent)((IElementInternal)parentElement).Driver).GetDriver(),
                            (IElementInternal)parentElement,
                            (IElementInternal)subElement);

            for (int index = 0; index < elementList.Count; index++) {
                IElementInternal childElement =
                    GetElementProxy<T>(((IElementInternal)parentElement).
                    Driver, ((IElementInternal)subElement).Locator, ((IElementInternal)subElement).Timeout);

                childElement.BindedElement = elementList[index];
                childElement.ParentElement = parentElement;
                childElement.ListOrder = index;

                childElementList.Add((T)childElement);
            }

            IElementListInternal childList =
                       GetElementListProxy<M>(
                        ((IElementInternal)parentElement).Driver,
                        ((IElementInternal)subElement).Locator, ((IElementInternal)subElement).Timeout);

            ((ElementList)childList).SetBindedElementList(elementList);
            ((ElementList)childList).SetElementList(childElementList);
            ((ElementList)childList).ParentElement = parentElement;

            childList.IsPopulated = true;

            return childList;
        }
        public static IElementListInternal GetChildListWinApp<T, M>(IElement parentElement, IElement subElement)
            where T : IElementInternal, new()
            where M : IElementListInternal, new() {
            IList<T> childElementList = new List<T>();

            IList<IWinAppElement> elementList =
                        GetBindedChildElements(
                            (IWinAppDriver)((IAgent)((IElementInternal)parentElement).Driver).GetDriver(),
                            (IElementInternal)parentElement,
                            (IElementInternal)subElement);

            for (int index = 0; index < elementList.Count; index++) {
                IElementInternal childElement =
                    GetElementProxy<T>(((IElementInternal)parentElement).
                    Driver, ((IElementInternal)subElement).Locator, ((IElementInternal)subElement).Timeout);

                childElement.BindedElement = elementList[index];
                childElement.ParentElement = parentElement;
                childElement.ListOrder = index;

                childElementList.Add((T)childElement);
            }

            IElementListInternal childList =
                       GetElementListProxy<M>(
                        ((IElementInternal)parentElement).Driver,
                        ((IElementInternal)subElement).Locator, ((IElementInternal)subElement).Timeout);

            ((ElementList)childList).SetBindedElementList(elementList);
            ((ElementList)childList).SetElementList(childElementList);
            ((ElementList)childList).ParentElement = parentElement;

            childList.IsPopulated = true;

            return childList;
        }
    }
}
