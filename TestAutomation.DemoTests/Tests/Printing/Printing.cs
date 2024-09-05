using System.Threading;
using TechTalk.SpecFlow;
using TestAutomation.DemoTests.PageModels.Printing;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.DemoTests.Tests.Printing {
    [Binding, Scope(Feature = "Printing")]
    public class Printing : Test {
        readonly PrintingModel printingModel = null;

        [StepDefinition(@"'(.*)' URL açılır")]
        public void LaunchIntervision(string url) {
            printingModel.LaunchUrl(url);
        }

        [StepDefinition(@"PrintBlackAndWhite butonuna tıklanır")]
        public void ClickBlackAndWhite() {
            printingModel.ClickBlackAndWhite();
            Thread.Sleep(10_000);
        }
    }
}
