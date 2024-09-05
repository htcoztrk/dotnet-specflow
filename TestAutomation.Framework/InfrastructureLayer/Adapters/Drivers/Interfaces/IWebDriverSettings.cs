using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces {
    public interface IWebDriverSettings {
        ChromeOptions ChromeHeadlessOptions { get; }

        ChromeOptions ChromeOptions { get; }

        FirefoxOptions FirefoxOptions { get; }

        InternetExplorerOptions InternetExplorerOptions { get; }

        EdgeOptions EdgeOptions { get; }

        Uri ServerUri { get; }
    }
}