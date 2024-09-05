using System;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Agents;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings.Factories;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.DomainLayer.Factories {
    static class DriverFactory {
        public static IDriver GetLaunchedDriver(Platform platform, ProxyServer proxyServer) {
            IDriver driver;

            if (proxyServer.DesktopAppType.Equals(DesktopAppType.Finally)) {
                driver = DesktopDriverSettingsFactory.SetDriverSettings(platform, proxyServer);
            }
            else if (TryCompareChromium(platform) ||
                platform.Equals(Platform.WEB_FIREFOX) ||
                platform.Equals(Platform.WEB_INTERNET_EXPLORER) ||
                platform.Equals(Platform.WEB_EDGE)
                ) {
                driver = WebDriverSettingsFactory.SetDriverSettings(platform);
            }
            else if (platform.Equals(Platform.WINDOWS_DESKTOP_APP)) {
                driver = DesktopDriverSettingsFactory.SetDriverSettings(platform, proxyServer);
            }
            else {
                string mlResult = null;// ServiceLocator.Create<IStringService>().GetString("TestAuto_PlatformError");
                ////logger.Warn(mlResult);
                throw new ArgumentNullException(mlResult);
            }
            return driver;
        }

        private static bool TryCompareChromium(Platform platform) {
            return (platform.Equals(Platform.WEB_CHROME) ||
                    platform.Equals(Platform.WEB_CHROME_HEADLESS) ||
                    platform.Equals(Platform.WEB_EDGE));
        }

        internal static IDriver LaunchWebDriver(
            Platform platform,
            LocalWebDriverSettings localWebDriverSettings) {
            IDriver driver;

            if (platform.Equals(Platform.WEB_CHROME)) {
                new ChromeResources().CreateDriversIntoFolder(Config.GetInstance().ReferencePath);
                driver = new WebChrome(localWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_CHROME_HEADLESS)) {
                driver = new WebChromeHeadless(localWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_FIREFOX)) {
                driver = new WebFirefox(localWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_INTERNET_EXPLORER)) {
                new IEResources().CreateDriversIntoFolder(
                    Config.GetInstance().ReferencePath);
                driver = new WebInternetExplorer(localWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_EDGE)) {
                driver = new WebEdge(localWebDriverSettings);
            }
            else {
                string mlResult = null;// ServiceLocator.Create<IStringService>().GetString("TestAuto_PlatformDriverNotFound");
                throw new ArgumentNullException(mlResult);
            }

            return driver;
        }

        internal static IDriver LaunchWebDriver(
            Platform platform,
            RemoteWebDriverSettings remoteWebDriverSettings) {
            IDriver driver;

            if (platform.Equals(Platform.WEB_CHROME)) {
                driver = new WebChrome(remoteWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_CHROME_HEADLESS)) {
                driver = new WebChromeHeadless(remoteWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_FIREFOX)) {
                driver = new WebFirefox(remoteWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_INTERNET_EXPLORER)) {
                driver = new WebInternetExplorer(remoteWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_EDGE)) {
                driver = new WebEdge(remoteWebDriverSettings);
            }
            else {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_PlatformDriverNotFound");
                throw new ArgumentNullException(mlResult);
            }

            return driver;
        }

        internal static IDriver LaunchWebDriver(
            Platform platform,
            TestiniumWebDriverSettings testiniumWebDriverSettings) {
            IDriver driver;

            if (platform.Equals(Platform.WEB_CHROME)) {
                driver = new WebChrome(testiniumWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_CHROME_HEADLESS)) {
                driver = new WebChromeHeadless(testiniumWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_FIREFOX)) {
                driver = new WebFirefox(testiniumWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_INTERNET_EXPLORER)) {
                driver = new WebInternetExplorer(testiniumWebDriverSettings);
            }
            else if (platform.Equals(Platform.WEB_EDGE)) {
                driver = new WebEdge(testiniumWebDriverSettings);
            }
            else {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_PlatformDriverNotFound");
                throw new ArgumentNullException(mlResult);
            }

            return driver;
        }

        internal static IDriver LaunchDesktopDriver(
            LocalDesktopDriverSettings localDesktopDriverSettings,
            ProxyServer proxyServer) {

            new WiniumResources().CreateDriversIntoFolder(Config.GetInstance().ReferencePath);

            if (Config.GetInstance().ExternalPlatformType.Equals(ExternaPlatformType.WINAPPDRIVER)) {
                return new WinApp(localDesktopDriverSettings, proxyServer);
            }
            return new Winium(localDesktopDriverSettings, proxyServer);
        }

        internal static IDriver LaunchDesktopDriver(
            RemoteDesktopDriverSettings remoteDesktopDriverSettings,
            ProxyServer proxyServer) {
            if (Config.GetInstance().ExternalPlatformType.Equals(ExternaPlatformType.WINAPPDRIVER)) {
                return new WinApp(remoteDesktopDriverSettings, proxyServer);
            }
            return new Winium(remoteDesktopDriverSettings, proxyServer);
        }

        internal static IDriver LaunchDesktopDriver(
            TestiniumDesktopDriverSettings testiniumDesktopDriverSettings,
            ProxyServer proxyServer) {
            if (Config.GetInstance().ExternalPlatformType.Equals(ExternaPlatformType.WINAPPDRIVER)) {
                return new WinApp(testiniumDesktopDriverSettings, proxyServer);
            }
            return new Winium(testiniumDesktopDriverSettings, proxyServer);
        }
    }
}
