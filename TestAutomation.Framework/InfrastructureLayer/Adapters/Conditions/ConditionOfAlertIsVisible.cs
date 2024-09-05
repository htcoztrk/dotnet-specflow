using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TestAutomation.Framework.DomainLayer.Contracts;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions {
    class ConditionOfAlertIsVisible : Condition {

        public ConditionOfAlertIsVisible(IAgent driver, TimeSpan timeout) {
            base.Driver = (ISeleniumWebDriver)driver.GetDriver();
            WaitUntilAlertIsPresent(timeout);
        }

        private void WaitUntilAlertIsPresent(TimeSpan timeout) {
            try {
                Wait(timeout);
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException();
            }
        }

        public override void Wait() {
            Wait(GetTimeout());
        }

        public override void Wait(TimeSpan timeout) {
            try {
                WebDriverWait wait = new WebDriverWait(Driver, timeout);
                wait.Until(ExpectedConditions.AlertIsPresent());
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException();
            }
        }

    }
}
