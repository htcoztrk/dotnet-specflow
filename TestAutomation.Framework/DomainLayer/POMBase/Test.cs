using System;
using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Framework.DomainLayer.Builders;
using TestAutomation.Framework.DomainLayer.Container;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Events.Handlers;
using TestAutomation.Framework.DomainLayer.Events.Interfaces;
using TestAutomation.Framework.DomainLayer.Factories;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.DomainLayer.SpecSync;
using TestAutomation.Framework.InfrastructureLayer.Settings;
using IWebDriver = TestAutomation.Framework.DomainLayer.Contracts.IWebDriver;

namespace TestAutomation.Framework.DomainLayer.POMBase {

    /// <summary>
    /// Base Test Class.
    /// All tests begin and end with this class
    /// </summary>
    [Parallelizable]
    public abstract class Test : IDisposable {
        private SystemNotification notifier;
        private Context context;

        //readonly ITestLogBase uiTestLog = new UiTestLog();
        Platform platform;

        /// <summary>
        /// Context.
        /// </summary>
        protected Context Context
        {
            get
            {
                return context = context ?? new Context(this);
            }
        }

        /// <summary>
        /// Test name
        /// </summary>
        public string Name { get; private set; }

        private void InternalInitialize(Platform context) {
            DriverManager.CleanStart(this, context);

            if (IsSeleniumTimeoutRefresh() && DriverManager.GetDriver(this, TestEnvironment.DESKTOPAPP) != null) {
                notifier = new SystemNotification((IWebDriver)DriverManager.GetDriver(this, TestEnvironment.WEBAPP));
                notifier.StartPeriodicNotification(new Interval(50000));

                EventService.AddListener(this, new EventOnSystemNotifier(), NotifyServerOnAction);
            }

            TestBuilder.SetTestModelFields(this);
        }

        private static bool IsSeleniumTimeoutRefresh() {
            if (Config.GetInstance().TimeoutRefresh != "True") {
                return false;
            }
            return true;
        }

        //When using winium to get screenshot
        private void NotifyServerOnAction(object sender, IArgs args) {
            notifier.TryNotifyForAction(args.Message);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext) {
            TagConverter.Convert(scenarioContext?.ScenarioInfo.ScenarioAndFeatureTags);
        }

        /// <summary>
        /// Setup method for test. Tests start with this method.
        /// </summary>
        [Before]
        [SetUp]
        public virtual void SetUp() {
            ContainerService.Add<ProxyServerContainer>(this, this, new ProxyServer(this));
            Initialize();
            //uiTestLog.DefineLogsBeforeTestStarted(this);

            if (Config.GetInstance().IsDefaultPlatformActive) {
                platform = Platform.NONE;

                try {
                    platform = Config.GetInstance().DefaultPlatform;
                }
                catch (Exception) {
                    string mlResult = null;// ServiceLocator.Create<IStringService>().GetString("TestAuto_PlatformNotFound");
                    string message = string.Format(mlResult, platform.ToString());
                    ////logger.Error(ex);
                    throw new ArgumentNullException(message);
                }

                InternalInitialize(platform);

                //ThreadContext.Properties["ClientIP"] = ProcessManager.GetClientUri(this) ?? "";
                //ThreadContext.Properties["ClientMachineName"] = ProcessManager.GetClientMachineName(this) ?? "";

                InitAfterDriver();
            }
        }

        /// <summary>
        /// Override initialize
        /// </summary>
        protected virtual void Initialize() { }

        /// <summary>
        /// Override initialize after drivers have been loaded
        /// </summary>
        protected virtual void InitAfterDriver() { }

        /// <summary>
        /// Run On Teardown
        /// </summary>
        protected virtual void AfterScenario() { }

        /// <summary>
        /// Tests finish with this method
        /// </summary>
        [After]
        [TearDown]
        public virtual void Teardown() {
            try {

                if (IsSeleniumTimeoutRefresh() && DriverManager.GetDriver(this, TestEnvironment.DESKTOPAPP) != null) {
                    notifier.StopPeriodicNotification();
                }

                DriverManager.Stop(this);

                if (!string.IsNullOrEmpty(Config.GetInstance().FinallyDesktopAppPath)) {
                    ProxyServer proxyServer = (ProxyServer)ContainerService.Get<ProxyServerContainer>(this).Get(this);
                    proxyServer.DesktopAppType = DesktopAppType.Finally;
                    var finallyDesktopDriver = ((IAgent)DriverFactory.GetLaunchedDriver(platform, proxyServer));
                }
            }
            catch (Exception) {
                ////logger.Error(ex);
            }
            finally {
                ProcessManager.KillAll(this);
                ContainerService.Clear<ProxyServerContainer>(this);
            }

            //uiTestLog.DefineLogsAfterTestFinished();
            AfterScenario();
        }

        /// <summary>
        /// For cross browser define. Returns list of platforms.
        /// </summary>
        public static IEnumerable<Platform> CrossBrowserDataSource() {
            IList<Platform> platformList = Config.GetInstance().Platforms;

            foreach (Platform crossPlatform in platformList) {
                yield return crossPlatform;
            }
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
        }
    }
}