using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Agents {
    class WebFirefox : WebDriver, IAgent {
        private readonly IWebDriverSettings settings;

        public string SessionId
        {
            get
            {
                return ((RemoteWebDriver)Driver).SessionId.ToString();
            }
        }

        /// <summary>
        /// Create local firefox driver
        /// </summary>
        /// <param name="settings"></param>
        public WebFirefox(LocalWebDriverSettings settings) {
            try {
                this.settings = settings;

                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Config.GetInstance().ReferencePath);
                FirefoxOptions options = new FirefoxOptions {
                    BrowserExecutableLocation = Config.GetInstance().FirefoxPath
                };
                Driver = new FirefoxDriver(service, options, TimeSpan.FromMinutes(1));
            }
            catch (DriverServiceNotFoundException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartLocalError");
                //logger.Fatal(ex);
                throw new
                    InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartLocalError");
                //logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        /// <summary>
        /// Create remote firefox driver
        /// </summary>
        /// <param name="settings"></param>
        public WebFirefox(RemoteWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver =
                    new RemoteWebDriver(
                        this.settings.ServerUri,
                        this.settings.FirefoxOptions);
            }
            catch (WebDriverException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                //logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                //logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        /// <summary>
        /// Create testinium firefox driver
        /// </summary>
        /// <param name="settings"></param>
        public WebFirefox(TestiniumWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver =
                    new RemoteWebDriver(
                        this.settings.ServerUri,
                        this.settings.FirefoxOptions);
            }
            catch (InvalidOperationException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartTestiniumError");
                //logger.Fatal(ex);
                throw new
                    InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartTestiniumError");
                //logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        public void Quit() {
            Driver.Quit();
        }

        public object GetDriver() {
            return Driver;
        }
    }
}
