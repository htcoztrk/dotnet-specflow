using System;
using System.Diagnostics;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Agents {
    class Winium : DesktopDriver, IAgent {
        private readonly IDesktopDriverSettings settings;
        private readonly ProxyServer proxyServer;

        public Winium(LocalDesktopDriverSettings settings, ProxyServer proxyServer) {
            this.settings = settings;
            this.proxyServer = proxyServer;

            try {
                Process.Start(
                    @"" + Config.
                    GetInstance().
                    ReferencePath +
                    "\\Winium.Desktop.driver.exe");

                WiniumDriver = new RemoteWebDriver(
                    this.settings.WiniumHubUri,
                    this.settings.WiniumCapabilities,
                    TimeSpan.FromSeconds(60));
            }
            catch (Exception) {
                //logger.Fatal(ex);
            }
        }

        public Winium(RemoteDesktopDriverSettings settings, ProxyServer proxyServer) {
            this.settings = settings;
            this.proxyServer = proxyServer;

            try {
                WiniumDriver = new RemoteWebDriver(
                    new Uri(ProxyServerUri),
                    this.settings.WiniumCapabilities,
                    TimeSpan.FromSeconds(60));
            }
            catch (Exception) {
                //logger.Fatal(ex);
            }
        }

        public Winium(TestiniumDesktopDriverSettings settings, ProxyServer proxyServer) {
            this.settings = settings;
            this.proxyServer = proxyServer;

            try {
                WiniumDriver = new RemoteWebDriver(
                    new Uri(ProxyServerUri),
                    this.settings.WiniumCapabilities,
                    TimeSpan.FromSeconds(60));
            }
            catch (Exception) {
                //logger.Fatal(ex);
            }
        }

        private string ProxyServerUri
        {
            get
            {
                return
                    "http://" +
                    proxyServer.ClientIP +
                    ":" +
                    Config.GetInstance().WiniumPort;
            }
        }

        public string SessionId
        {
            get
            {
                return proxyServer.SessionId;
            }
        }

        public object GetDriver() {
            return WiniumDriver;
        }

        public void Quit() {
            WiniumDriver.Quit();
        }
    }
}
