using System;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings {
    class RemoteWebDriverSettings : IWebDriverSettings {
        public Uri ServerUri
        {
            get
            {
                return new Uri(Config.GetInstance().ServerUrl + ":4444/wd/hub");
            }
        }

        public ChromeOptions ChromeHeadlessOptions
        {
            get
            {
                return new ChromeOptions();
            }
        }

        public ChromeOptions ChromeOptions
        {
            get
            {
                //ThreadContext.Properties["Platform"] = PlatformLogName.CHROME;
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--test-type");
                options.AddArguments("--start-maximized");
                options.AddArguments("--disable-extensions");
                options.AddArgument("--ignore-certificate-errors");
                options.AddArgument("--disable-popup-blocking");
                options.AddArgument("no-sandbox");
                options.AddArguments(BrowserArguments);

                return options;
            }
        }

        public FirefoxOptions FirefoxOptions
        {
            get
            {
                return new FirefoxOptions();
            }
        }

        public InternetExplorerOptions InternetExplorerOptions
        {
            get
            {
                //ThreadContext.Properties["Platform"] = PlatformLogName.IE;
                InternetExplorerOptions ieOption = new InternetExplorerOptions {
                    IgnoreZoomLevel = true,
                    IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                    RequireWindowFocus = false,
                    BrowserCommandLineArguments = "",
                    ForceCreateProcessApi = false,
                    EnsureCleanSession = false,
                    UsePerProcessProxy = false
                };
                ieOption.AddAdditionalCapability("ie.setProxyByServer", false);

                return ieOption;
            }
        }

        public EdgeOptions EdgeOptions
        {
            get
            {
                //ThreadContext.Properties["Platform"] = PlatformLogName.EDGE;
                EdgeOptions options = new EdgeOptions();
                options.AddAdditionalCapability("--test-type", true);
                options.AddAdditionalCapability("--start-maximized", true);
                options.AddAdditionalCapability("--disable-extensions", true);
                options.AddAdditionalCapability("--ignore-certificate-errors", true);
                options.AddAdditionalCapability("--disable-popup-blocking", true);
                options.AddAdditionalCapability("no-sandbox", true);
                Dictionary<string, List<string>> map = new Dictionary<string, List<string>> {
                    { "args", BrowserArguments }
                };
                options.AddAdditionalCapability("ms:edgeOptions", map);
                return options;
            }
        }

        private static List<string> BrowserArguments
        {
            get
            {
                return Config.GetInstance().BrowserArguments;
            }
        }
    }
}
