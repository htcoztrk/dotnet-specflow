using Intertech.TestAutomation.Framework.DomainLayer.Utils.Enums;
using Intertech.TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Interfaces;
using Intertech.TestAutomation.Framework.InfrastructureLayer.System;

namespace Intertech.TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings
{
    class DesktopDriverSettings
    {
        private readonly ExecutionEnvironment _executionEnv;
        public DesktopDriverSettings()
        {
            _executionEnv = Config.GetInstance().ExecutionEnvironment;

        }

        public IDesktopDriverSettings GetEnvironment()
        {
            IDesktopDriverSettings settings = null;

            if (_executionEnv.Equals(ExecutionEnvironment.LOCALHOST))
            {
                settings = new LocalDesktopDriverSettings();
            }
            else if (_executionEnv.Equals(ExecutionEnvironment.REMOTE))
            {
                settings = new RemoteDesktopDriverSettings();
            }
            else if (_executionEnv.Equals(ExecutionEnvironment.TESTINIUM))
            {
               // settings = new TestiniumWebDriverSettings();
            }

            return settings;
        }


    }
}
