using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings {
    class TestiniumWebDriverSettings : IWebDriverSettings {
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
                //ThreadContext.Properties["Platform"] = TestContext.Parameters.Get("browser");
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--test-type");
                options.AddArguments("--start-maximized");
                options.AddArgument("--ignore-certificate-errors");
                options.AddArgument("--disable-popup-blocking");
                options.AddArgument("no-sandbox");
                string key = TestContext.Parameters.Get("key");
                options.AddAdditionalCapability("key", key);
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
                //ThreadContext.Properties["Platform"] = TestContext.Parameters.Get("browser");
                InternetExplorerOptions ieOption = new InternetExplorerOptions {
                    IgnoreZoomLevel = true,
                    IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                    RequireWindowFocus = false,
                    BrowserCommandLineArguments = "",
                    ForceCreateProcessApi = false,
                    EnsureCleanSession = false,
                    UsePerProcessProxy = false
                };
                ieOption.RequireWindowFocus = false;
                ieOption.AddAdditionalCapability("ie.setProxyByServer", false);

                return ieOption;
            }
        }

        public EdgeOptions EdgeOptions
        {
            get
            {
                //ThreadContext.Properties["Platform"] = TestContext.Parameters.Get("browser");
                EdgeOptions options =
                    new EdgeOptions();
                options.AddAdditionalCapability("--test-type", true);
                options.AddAdditionalCapability("--start-maximized", true);
                options.AddAdditionalCapability("--disable-extensions", true);
                options.AddAdditionalCapability("--ignore-certificate-errors", true);
                options.AddAdditionalCapability("--disable-popup-blocking", true);
                options.AddAdditionalCapability("no-sandbox", true);
                string key = TestContext.Parameters.Get("key");
                options.AddAdditionalCapability("key", key);
                Dictionary<string, List<string>> map = new Dictionary<string, List<string>> {
                    { "args", BrowserArguments }
                };
                options.AddAdditionalCapability("ms:edgeOptions", map);
                return options;
            }
        }

        public Uri ServerUri
        {
            get
            {
                return new Uri(Config.GetInstance().ServerUrl + ":4444/wd/hub");
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
