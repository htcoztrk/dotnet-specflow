using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestAutomation.Framework.DomainLayer.Contracts;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;
using ISeleniumWebElement = OpenQA.Selenium.IWebElement;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions {
    class ConditionOfFrameToBeSwitchableAndSwitchToIt : Condition {
        private readonly By element = null;
        public ConditionOfFrameToBeSwitchableAndSwitchToIt(IAgent driver, IElement element) {
            base.Driver = (ISeleniumWebDriver)driver.GetDriver();
            this.element = (By)((IElementInternal)element).By;
        }

        public override void Wait() {
            Wait(GetTimeout());
        }

        public override void Wait(TimeSpan timeout) {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            wait.Until(wd => {
                try {
                    ISeleniumWebElement frameElement = wd.FindElement(element);
                    return wd.SwitchTo().Frame(frameElement);
                }
                catch (NoSuchFrameException) {
                    return null;
                }
            });
        }
    }
}
