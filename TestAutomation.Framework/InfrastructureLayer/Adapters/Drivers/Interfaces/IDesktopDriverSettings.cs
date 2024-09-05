using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces {
    public interface IDesktopDriverSettings {
        DesiredCapabilities WiniumCapabilities { get; }
        Uri WiniumHubUri { get; }
        string RemoteServerUrl { get; }
        AppiumOptions WinAppDriverOptions { get; }
        AppiumOptions WinAppDriverRootOptions { get; }
        Uri WinAppDriverHubUri { get; }
    }
}
