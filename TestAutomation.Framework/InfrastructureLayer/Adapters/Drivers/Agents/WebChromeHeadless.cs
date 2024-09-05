using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Agents {
    class WebChromeHeadless : WebDriver, IAgent {
        private readonly IWebDriverSettings settings;


        public string SessionId
        {
            get
            {
                return ((RemoteWebDriver)Driver).SessionId.ToString();
            }
        }

        /// <summary>
        /// Create local chrome headless driver
        /// </summary>
        /// <param name="settings"></param>
        public WebChromeHeadless(LocalWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver = new ChromeDriver(Config.GetInstance().ReferencePath, this.settings.ChromeHeadlessOptions);
            }
            catch (DriverServiceNotFoundException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartLocalError");
                //logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartLocalError");
                //logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        /// <summary>
        /// Create remote chrome headless driver
        /// </summary>
        /// <param name="settings"></param>
        public WebChromeHeadless(RemoteWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver =
                    new RemoteWebDriver(
                        this.settings.ServerUri,
                        this.settings.ChromeHeadlessOptions);
            }
            catch (WebDriverException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                //logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                //logger.Fatal( ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        /// <summary>
        /// Create testinium chrome headless driver
        /// </summary>
        /// <param name="settings"></param>
        public WebChromeHeadless(TestiniumWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver = new RemoteWebDriver(
                    this.settings.ServerUri,
                    this.settings.ChromeHeadlessOptions);
            }
            catch (InvalidOperationException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartTestiniumError");
                //logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
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
