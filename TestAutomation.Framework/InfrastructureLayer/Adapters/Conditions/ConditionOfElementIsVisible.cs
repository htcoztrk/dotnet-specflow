using TestAutomation.Framework.DomainLayer.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;
using IWinAppDriver = OpenQA.Selenium.Appium.Windows.WindowsDriver<OpenQA.Selenium.Appium.Windows.WindowsElement>;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions {
    class ConditionOfElementIsVisible : Condition {
        private readonly By element = null;

        public ConditionOfElementIsVisible(IAgent driver, IElement element) {
            base.Driver = (ISeleniumWebDriver)driver.GetDriver();
            this.element = (By)((IElementInternal)element).By;
        }

        public ConditionOfElementIsVisible(ISeleniumWebDriver driver, By by) {
            base.Driver = driver;
            element = by;
        }

        public ConditionOfElementIsVisible(IWinAppDriver driver, By by) {
            base.Driver = driver;
            element = by;
        }

        public override void Wait() {
            Wait(GetTimeout());
        }

        public override void Wait(TimeSpan timeout) {
            try {
                WebDriverWait wait = new WebDriverWait(Driver, timeout);
                wait.Until(ExpectedConditions.ElementIsVisible(element));
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException();
            }
        }
    }
}
