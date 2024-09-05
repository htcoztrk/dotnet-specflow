using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Agents {
    class WebChrome : WebDriver, IAgent {
        private readonly IWebDriverSettings settings;

        public string SessionId
        {
            get
            {
                return ((RemoteWebDriver)Driver).SessionId.ToString();
            }
        }

        /// <summary>
        /// Create local chrome driver
        /// </summary>
        /// <param name="settings"></param>
        public WebChrome(LocalWebDriverSettings settings) {
            try {
                this.settings = settings;
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(Config.GetInstance().ReferencePath);
                chromeDriverService.HideCommandPromptWindow = false;
                Driver = new ChromeDriver(chromeDriverService, this.settings.ChromeOptions, TimeSpan.FromSeconds(60));
            }
            catch (DriverServiceNotFoundException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartLocalError");
                ////logger.Fatal(ex);
                throw new
                    InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartLocalError");
                ////logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        /// <summary>
        /// Create remote chrome driver
        /// </summary>
        /// <param name="settings"></param>
        public WebChrome(RemoteWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver =
                    new RemoteWebDriver(
                        this.settings.ServerUri,
                        this.settings.ChromeOptions);
            }
            catch (WebDriverException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                ////logger.Fatal(ex);
                throw new
                    InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                /////logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        /// <summary>
        /// Create testinium chrome driver
        /// </summary>
        /// <param name="settings"></param>
        public WebChrome(TestiniumWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver =
                    new RemoteWebDriver(
                        this.settings.ServerUri,
                        this.settings.ChromeOptions);
            }
            catch (WebDriverException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartTestiniumError");
                ////logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
            catch (InvalidOperationException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartTestiniumError");
                ////logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartTestiniumError");
                ////logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        public void Quit() {
            Driver.Close();
            Driver.Quit();
        }

        public object GetDriver() {
            return Driver;
        }
    }
}
