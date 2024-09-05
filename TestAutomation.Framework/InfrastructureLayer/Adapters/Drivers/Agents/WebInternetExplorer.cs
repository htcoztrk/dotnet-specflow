using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Agents {
    class WebInternetExplorer : WebDriver, IAgent {
        private readonly IWebDriverSettings settings;


        public string SessionId
        {
            get
            {
                return ((RemoteWebDriver)Driver).SessionId.ToString();
            }
        }

        /// <summary>
        /// Create local IE driver
        /// </summary>
        /// <param name="settings"></param>
        public WebInternetExplorer(LocalWebDriverSettings settings) {
            try {
                this.settings = settings;

                Driver =
                    new InternetExplorerDriver(
                        Config.GetInstance().ReferencePath,
                    this.settings.InternetExplorerOptions);
            }
            catch (DriverServiceNotFoundException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartLocalError");
                //logger.Fatal( ex);
                throw new InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartLocalError");
                //logger.Fatal( ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        /// <summary>
        /// Create remote IE driver
        /// </summary>
        /// <param name="settings"></param>
        public WebInternetExplorer(RemoteWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver = new RemoteWebDriver(
                    this.settings.ServerUri,
                    this.settings.InternetExplorerOptions);
            }
            catch (WebDriverException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                //logger.Fatal( ex);
                throw new InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                //logger.Fatal( ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        /// <summary>
        /// Create testinium IE driver
        /// </summary>
        /// <param name="settings"></param>
        public WebInternetExplorer(TestiniumWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver = new RemoteWebDriver(
                    this.settings.ServerUri,
                    this.settings.InternetExplorerOptions);
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
            Driver.Close();
            Driver.Quit();
        }

        public object GetDriver() {
            return Driver;
        }
    }
}
