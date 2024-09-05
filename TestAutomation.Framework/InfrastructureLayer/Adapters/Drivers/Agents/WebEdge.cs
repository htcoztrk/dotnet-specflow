using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Agents {
    class WebEdge : WebDriver, IAgent {
        private readonly IWebDriverSettings settings;


        public string SessionId
        {
            get
            {
                return ((RemoteWebDriver)Driver).SessionId.ToString();
            }
        }

        /// <summary>
        /// Create local edge driver
        /// </summary>
        /// <param name="settings"></param>
        public WebEdge(LocalWebDriverSettings settings) {
            try {
                this.settings = settings;
                EdgeDriverService service = EdgeDriverService.CreateDefaultService(Config.GetInstance().ReferencePath);
                service.HideCommandPromptWindow = false;
                service.UseSpecCompliantProtocol = true;
                Driver = new EdgeDriver(service, this.settings.EdgeOptions, TimeSpan.FromSeconds(60));
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
        /// Create remote edge driver
        /// </summary>
        /// <param name="settings"></param>
        public WebEdge(RemoteWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver =
                    new RemoteWebDriver(
                        this.settings.ServerUri,
                        this.settings.EdgeOptions);
            }
            catch (WebDriverException ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                ////logger.Fatal(ex);
                throw new
                    InvalidOperationException(mlResult, ex);
            }
            catch (Exception ex) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_SeleniumDriverStartRemoteError");
                ////logger.Fatal(ex);
                throw new InvalidOperationException(mlResult, ex);
            }
        }

        /// <summary>
        /// Create testinium edge driver
        /// </summary>
        /// <param name="settings"></param>
        public WebEdge(TestiniumWebDriverSettings settings) {
            try {
                this.settings = settings;
                Driver =
                    new RemoteWebDriver(
                        this.settings.ServerUri,
                        this.settings.EdgeOptions);
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
