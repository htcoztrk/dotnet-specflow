using System;
using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Container;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Factories;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.DomainLayer.Utils;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.DomainLayer.Managers {

    static class DriverManager {
        public static void Stop(Test test) {
            foreach (IDriver dr in ContainerService.Get<DriverContainer>(test)) {
                try {
                    ((IAgent)dr).Quit();
                }
                catch (Exception) {
                }
            }
        }

        public static void Start(Test test, Platform context) {
            IList<Platform> externalPlatformList = Config.GetInstance().ExternalPlatforms ?? new List<Platform>();

            LaunchDriver(test, context);

            foreach (Platform externalPlatform in externalPlatformList) {
                LaunchDriver(test, externalPlatform);
            }

            SetProxyClient(test);
        }

        /// <summary>
        /// Açık winium driver varsa önce process'i sonlandırır, ardından işleme başlar    
        /// </summary>
        /// <param name="test"></param>
        /// <param name="context"></param>    
        public static void CleanStart(Test test, Platform context) {
            if (Config.GetInstance().ExecutionEnvironment.ToString().Equals("LOCALHOST")) {
                ProcessManager.KillProcess(new Process("Winium.Desktop.Driver"), test);
            }

            Start(test, context);
        }

        private static void SetProxyClient(Test test) {
            ContainerService.
            Get<ProxyServerContainer>(test).
            Get(test);
        }

        public static Session GetSession(Test test) {
            IAgent driver = null;
            Session session = null;

            IContainer driverMap = ContainerService.Get<DriverContainer>(test);

            if (driverMap.Count == 0) {
                throw new ArgumentException(
                    "Driver'a ait session bilgisi alınamadı. " +
                    $"Config dosyasında {nameof(ExecutionEnvironment)} key'inin doğruluğunu kontrol ediniz."
                    );
            }

            TestEnvironment actualResult = EnumConverter.GetPlatformTestEnvironment(Config.GetInstance().DefaultPlatform);
            driver = (IAgent)driverMap.Get(actualResult);

            session = new Session(driver.SessionId);

            return session;
        }

        public static IDriver GetDriver(Test test, TestEnvironment testEnvironment) {
            IContainer driverMap = ContainerService.Get<DriverContainer>(test);

            if (driverMap.ContainsKey(testEnvironment)) {
                return (IDriver)driverMap.Get(testEnvironment);
            }

            return null;
        }

        private static void LaunchDriver(Test test, Platform platform) {
            TestEnvironment testEnvironment = EnumConverter.GetPlatformTestEnvironment(platform);

            string loggedUser = ProcessManager.GetActiveUserFromMachine(test);
            //ThreadContext.Properties["UserName"] = loggedUser;

            if (!CheckIfAlreadyLaunched(test, testEnvironment)) {
                LaunchDriverIfNotStartedAlready(test, platform);
            }
        }

        private static bool CheckIfAlreadyLaunched(Test test, TestEnvironment testEnvironment) {
            return ContainerService.Get<DriverContainer>(test).ContainsKey(testEnvironment);
        }

        private static void LaunchDriverIfNotStartedAlready(Test test, Platform platform) {
            ProxyServer proxyServer = (ProxyServer)ContainerService.Get<ProxyServerContainer>(test).Get(test);
            proxyServer.DesktopAppType = DesktopAppType.Default;

            IDriver driver = DriverFactory.GetLaunchedDriver(platform, proxyServer);

            ContainerService.Add<DriverContainer>(test, EnumConverter.GetPlatformTestEnvironment(platform),driver);
        }
    }
}
