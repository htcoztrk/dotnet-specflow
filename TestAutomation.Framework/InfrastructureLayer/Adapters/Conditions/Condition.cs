using System;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions {
    abstract class Condition {

        protected ISeleniumWebDriver Driver;

        /// <summary>
        /// Wait with default timeout
        /// </summary>
        public abstract void Wait();

        /// <summary>
        /// Wait with specified timeout
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        public abstract void Wait(TimeSpan timeout);

        /// <summary>
        /// Gets the default timeout.
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetTimeout() {
            return TimeSpan.FromSeconds(10);
        }
    }
}
