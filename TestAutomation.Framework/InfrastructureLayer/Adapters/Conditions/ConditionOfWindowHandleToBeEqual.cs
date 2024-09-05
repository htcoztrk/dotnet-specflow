using System;
using OpenQA.Selenium.Support.UI;
using TestAutomation.Framework.DomainLayer.Contracts;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions {
    internal class ConditionOfWindowHandleToBeEqual : Condition {
        private readonly int handleCount;
        public ConditionOfWindowHandleToBeEqual(IAgent driver, int expectedHandleCount) {
            base.Driver = (ISeleniumWebDriver)driver.GetDriver();
            handleCount = expectedHandleCount;
        }

        public override void Wait() {
            Wait(GetTimeout());
        }

        public override void Wait(TimeSpan timeout) {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            wait.Until(wd => wd.WindowHandles.Count == handleCount);
        }
    }
}
