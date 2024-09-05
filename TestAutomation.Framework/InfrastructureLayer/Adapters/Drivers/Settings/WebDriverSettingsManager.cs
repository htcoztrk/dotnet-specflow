using Intertech.TestAutomation.Framework.DomainLayer.Utils.Enums;
using Intertech.TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using Intertech.TestAutomation.Framework.InfrastructureLayer.System;

namespace Intertech.TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings
{
    class WebDriverSettingsManager
    {
        private readonly ExecutionEnvironment _executionEnv;
        public WebDriverSettingsManager()
        {
            _executionEnv = Config.GetInstance().ExecutionEnvironment;
            
        }

        public IWebDriverSettings GetEnvironment()
        {
            IWebDriverSettings settings = null;

            if (_executionEnv.Equals(ExecutionEnvironment.LOCALHOST))
            {
                settings = new LocalWebDriverSettings();
            }
            else if (_executionEnv.Equals(ExecutionEnvironment.REMOTE))
            {
                settings = new RemoteWebDriverSettings();
            }
            else if (_executionEnv.Equals(ExecutionEnvironment.TESTINIUM))
            {
                settings = new TestiniumWebDriverSettings();
            }

            return settings;
        }
    }
}
