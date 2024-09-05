using System;
using System.Diagnostics;
using OpenQA.Selenium.Appium.Windows;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Agents {
    class WinApp : DesktopDriver, IAgent {
        private readonly IDesktopDriverSettings settings;
        private readonly ProxyServer proxyServer;

        public WinApp(LocalDesktopDriverSettings settings, ProxyServer proxyServer) {
            this.settings = settings;
            this.proxyServer = proxyServer;

            try {
                Process.Start(Config.GetInstance().WinAppDriverPath);

                WinAppDriver = new WindowsDriver<WindowsElement>(this.settings.WinAppDriverHubUri,
                      this.settings.WinAppDriverOptions,
                      TimeSpan.FromSeconds(60));
            }
            catch (Exception) {
                //logger.Fatal(ex);
            }
        }

        public WinApp(RemoteDesktopDriverSettings settings, ProxyServer proxyServer) {
            this.settings = settings;
            this.proxyServer = proxyServer;

            try {
                WinAppDriver = new WindowsDriver<WindowsElement>(new Uri(ProxyServerWinappDriverUri),
                      this.settings.WinAppDriverOptions,
                      TimeSpan.FromSeconds(60));

                WinAppDriver = new WindowsDriver<WindowsElement>(new Uri(ProxyServerWinappDriverUri),
                    this.settings.WinAppDriverRootOptions,
                    TimeSpan.FromSeconds(60));
            }
            catch (Exception) {
                //logger.Fatal(ex);
            }
        }

        public WinApp(TestiniumDesktopDriverSettings settings, ProxyServer proxyServer) {
            this.settings = settings;
            this.proxyServer = proxyServer;

            try {
                WinAppDriver = new WindowsDriver<WindowsElement>(new Uri(ProxyServerWinappDriverUri),
                      this.settings.WinAppDriverOptions,
                      TimeSpan.FromSeconds(60));

                WinAppDriver = new WindowsDriver<WindowsElement>(new Uri(ProxyServerWinappDriverUri),
                    this.settings.WinAppDriverRootOptions,
                    TimeSpan.FromSeconds(60));
            }
            catch (Exception) {
                //logger.Fatal(ex);
            }
        }

        //remote makinada winappdriver için uri döner
        private string ProxyServerWinappDriverUri
        {
            get
            {
                return
                    "http://" +
                    proxyServer.ClientIP +
                    ":" +
                    Config.GetInstance().WinAppDriverPort +
                    "/wd/hub";
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
            return WinAppDriver;
        }

        public void Quit() {
            WinAppDriver.Quit();
        }
    }
}
