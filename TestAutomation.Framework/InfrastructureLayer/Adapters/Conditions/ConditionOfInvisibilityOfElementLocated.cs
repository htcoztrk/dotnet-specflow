using TestAutomation.Framework.DomainLayer.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions {
    class ConditionOfInvisibilityOfElementLocated : Condition {
        private readonly By element = null;
        public ConditionOfInvisibilityOfElementLocated(IAgent driver, IElement element) {
            base.Driver = (ISeleniumWebDriver)driver.GetDriver();
            this.element = (By)((IElementInternal)element).By;
        }

        public override void Wait() {
            Wait(GetTimeout());
        }

        public override void Wait(TimeSpan timeout) {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(element));
        }

    }
}
