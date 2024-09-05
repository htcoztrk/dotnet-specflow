using TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions;
using System;

namespace TestAutomation.Framework.InfrastructureLayer.Services {
    static class WaitService {
        public static void Wait(Condition condition) {
            condition.Wait();
        }

        public static void Wait(Condition condition, TimeSpan timeout) {
            condition.Wait(timeout);
        }
    }
}
