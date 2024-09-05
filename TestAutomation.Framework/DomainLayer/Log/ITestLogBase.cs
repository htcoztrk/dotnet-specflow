using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.Framework.DomainLayer.Log {
    public interface ITestLogBase {

        void DefineLogsBeforeTestStarted(Test test);

        void DefineLogsAfterTestFinished();
    }
}
