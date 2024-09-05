using TestAutomation.Framework.DomainLayer.Contracts;
using OpenQA.Selenium.Support.UI;
using System;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;
using ISeleniumWebElement = OpenQA.Selenium.IWebElement;


namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions {
    class ConditionOfElementToBeClickable : Condition {
        private readonly ISeleniumWebElement element = null;
        public ConditionOfElementToBeClickable(IAgent driver, IElement element) {
            base.Driver = (ISeleniumWebDriver)driver.GetDriver();
            this.element = (ISeleniumWebElement)((IElementInternal)element).BindedElement;
        }

        public override void Wait() {
            Wait(GetTimeout());
        }

        public override void Wait(TimeSpan timeout) {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }
    }
}
