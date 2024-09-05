using TestAutomation.Framework.DomainLayer.Contracts;
using OpenQA.Selenium.Support.UI;
using System;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;
using ISeleniumWebElement = OpenQA.Selenium.IWebElement;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions {
    class ConditionOfTextToBePresentInElement : Condition {

        private readonly ISeleniumWebElement element = null;
        private readonly string text;
        public ConditionOfTextToBePresentInElement(IAgent driver, IElement element, string text) {
            base.Driver = (ISeleniumWebDriver)driver.GetDriver();
            this.element = (ISeleniumWebElement)((IElementInternal)element).BindedElement;

            this.text = text;
        }

        public override void Wait() {
            Wait(GetTimeout());
        }

        public override void Wait(TimeSpan timeout) {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, text));
        }
    }
}
