using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings {
    class TestiniumDesktopDriverSettings : IDesktopDriverSettings {

        private readonly ProxyServer proxyServer;

        public TestiniumDesktopDriverSettings(ProxyServer proxyServer) {
            this.proxyServer = proxyServer;
        }

        public string RemoteServerUrl
        {
            get
            {
                return Config.GetInstance().ServerUrl;
            }
        }

        public AppiumOptions WinAppDriverOptions
        {
            get
            {
                AppiumOptions appOptions = new AppiumOptions();

                appOptions.PlatformName = "Windows";
                appOptions.AddAdditionalCapability("app", Config.GetInstance().DefaultDesktopAppPath);
                appOptions.AddAdditionalCapability("deviceName", "WindowsPC");

                return appOptions;
            }
        }

        public AppiumOptions WinAppDriverRootOptions
        {
            get
            {
                AppiumOptions appOptions = new AppiumOptions();

                appOptions.PlatformName = "Windows";
                appOptions.AddAdditionalCapability("app", "Root");
                appOptions.AddAdditionalCapability("deviceName", "WindowsPC");

                return appOptions;
            }
        }

        public Uri WinAppDriverHubUri
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DesiredCapabilities WiniumCapabilities
        {
            get
            {
                DesiredCapabilities capability = new DesiredCapabilities();
                string appPath = DesktopDriverUtils.GetApplicationPath(proxyServer.DesktopAppType);
                capability.SetCapability("app", appPath);
                return capability;
            }

        }

        public Uri WiniumHubUri
        {
            get
            {
                return new Uri(Config.GetInstance().WiniumUri);
            }
        }
    }
}
