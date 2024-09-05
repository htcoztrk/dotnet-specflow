using TestAutomation.Framework.DomainLayer.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;


namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions {
    class ConditionOfPageLoaded : Condition {

        public ConditionOfPageLoaded(IAgent driver) {
            base.Driver = (ISeleniumWebDriver)driver.GetDriver();
        }

        public override void Wait() {
            Wait(GetTimeout());
        }

        public override void Wait(TimeSpan timeout) {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            IJavaScriptExecutor wd = (IJavaScriptExecutor)Driver;
            wait.Until(webdriver => wd.ExecuteScript("return document.readyState").Equals("complete"));

        }
    }
}
