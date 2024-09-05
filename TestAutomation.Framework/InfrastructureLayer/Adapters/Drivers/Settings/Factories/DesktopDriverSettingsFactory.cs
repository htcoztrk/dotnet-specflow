using System;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Factories;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings.Factories {
    static class DesktopDriverSettingsFactory {
        private static ExecutionEnvironment executionEnv =
                               Config.GetInstance().ExecutionEnvironment;
        internal static IDriver SetDriverSettings(Platform platform, ProxyServer proxyServer) {
            IDriver driver = null;

            if (executionEnv.Equals(ExecutionEnvironment.LOCALHOST)) {
                driver = DriverFactory.LaunchDesktopDriver(new LocalDesktopDriverSettings(proxyServer), proxyServer);
            }
            else if (executionEnv.Equals(ExecutionEnvironment.REMOTE)) {
                driver = DriverFactory.LaunchDesktopDriver(new RemoteDesktopDriverSettings(proxyServer), proxyServer);
            }
            else if (executionEnv.Equals(ExecutionEnvironment.TESTINIUM)) {
                driver = DriverFactory.LaunchDesktopDriver(new TestiniumDesktopDriverSettings(proxyServer), proxyServer);
            }
            else {
                throw new ArgumentNullException(nameof(platform), "Girilen <Environment> değerine uygun bir Driver eşleşmesi yapılamadı.");
            }

            return driver;
        }
    }
}
