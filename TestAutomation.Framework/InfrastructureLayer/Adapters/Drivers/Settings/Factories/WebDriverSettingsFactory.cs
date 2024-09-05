using System;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Factories;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings.Factories {
    static class WebDriverSettingsFactory {
        private static ExecutionEnvironment executionEnv =
                            Config.GetInstance().ExecutionEnvironment;
        internal static IDriver SetDriverSettings(Platform platform) {
            IDriver driver;

            if (executionEnv.Equals(ExecutionEnvironment.LOCALHOST)) {
                driver = DriverFactory.LaunchWebDriver(platform, new LocalWebDriverSettings());
            }
            else if (executionEnv.Equals(ExecutionEnvironment.REMOTE)) {
                driver = DriverFactory.LaunchWebDriver(platform, new RemoteWebDriverSettings());
            }
            else if (executionEnv.Equals(ExecutionEnvironment.TESTINIUM)) {
                driver = DriverFactory.LaunchWebDriver(platform, new TestiniumWebDriverSettings());
            }
            else {
                throw new Exception("ExecutionEnvironment bulunamadı. Config dosyasındaki tanımları kontrol edin.");
            }

            return driver;
        }
    }
}
