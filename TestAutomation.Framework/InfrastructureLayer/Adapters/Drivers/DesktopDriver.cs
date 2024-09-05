using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.DomainLayer.Container;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Elements;
using TestAutomation.Framework.InfrastructureLayer.Services;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers {
    internal abstract class DesktopDriver : IDriver, IDesktopDriver {
        private Session session;

        protected RemoteWebDriver WiniumDriver;
        protected WindowsDriver<WindowsElement> WinAppDriver;


        /// <summary>
        /// returns session info
        /// </summary>
        public Session Session
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

        /// <summary>
        /// returns desktop driver element
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="locator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public IElement GetElement(Type elementType, Locator locator, TimeSpan timeout) {
            try {
                if (WinAppDriver != null)
                    return ElementService.GetElementProxy<WinAppElement>(this, locator, timeout);
                return ElementService.GetElementProxy<DesktopElement>(this, locator, timeout);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }

        }

        /// <summary>
        /// returns desktop driver element list
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="locator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public IElementListInternal GetElementList(Type elementType, Locator locator, TimeSpan timeout) {
            try {
                if (WinAppDriver != null)
                    return ElementService.GetElementListProxy<WinAppElementList>(this, locator, timeout);
                return ElementService.GetElementListProxy<DesktopElementList>(this, locator, timeout);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        /// wait with thread sleep
        /// </summary>
        /// <param name="timeToWait"></param>
        public void ForceWait(TimeSpan timeToWait) {
            Thread.Sleep(timeToWait);
        }

        /// <summary>
        /// check if element contains string, returns bool
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool ContainsText(
            IDesktopElement element,
            string text) {
            return ContainsText(element, text, Timeout);
        }

        /// <summary>
        /// check if element contains string, returns bool
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool ContainsText(
            IDesktopElement element,
            string text,
            TimeSpan timeout) {
            try {
                WaitService.Wait(
                    new ConditionOfElementIsVisible(
                        (IAgent)this,
                        element),
                        timeout);

                return element.GetAttribute("Name").Contains(text);
            }
            catch (Exception) {
                //logger.Error(ex);

                throw;
            }
        }

        /// <summary>
        /// check if element visible
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsElementVisible(IDesktopElement element) {
            return IsElementVisible(element, Timeout);
        }

        /// <summary>
        /// check if element visible
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool IsElementVisible(IDesktopElement element, TimeSpan timeout) {
            try {
                WaitService.Wait(
                    new ConditionOfElementIsVisible(
                        (IAgent)this,
                        element),
                    timeout);

                return true;
            }
            catch (WebDriverTimeoutException) {
                return false;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public bool IsElementClickable(IDesktopElement element) {
            return IsElementClickable(element, Timeout);
        }

        /// <inheritdoc/>
        public bool IsElementClickable(IDesktopElement element, TimeSpan timeout) {
            try {
                WaitService.Wait(
                    new ConditionOfElementToBeClickable(
                        (IAgent)this,
                        element),
                    timeout);

                return true;
            }
            catch (WebDriverTimeoutException) {
                return false;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public void DoubleClick(IDesktopElement element) {
            try {
                Actions action = new Actions(WinAppDriver);
                action.DoubleClick(
                    (WindowsElement)((IElementInternal)element).BindedElement).
                    Build().
                    Perform();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }
    }
}
