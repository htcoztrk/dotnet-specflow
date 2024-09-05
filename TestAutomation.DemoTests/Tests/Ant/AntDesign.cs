using TechTalk.SpecFlow;
using TestAutomation.DemoTests.PageModels.Ant;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.DemoTests.Tests.Ant {
    [Binding, Scope(Feature = "AntDesign")]
    public class AntDesign : Test {
        readonly AntModel antModel = null;

        [StepDefinition(@"'(.*)' URL açılır")]
        public void LaunchIntervision(string url) {
            antModel.LaunchUrl(url);
        }

        [StepDefinition(@"InputNumber Clear edilir")]
        public void Clear() {
            antModel.Clear();
        }

        [StepDefinition(@"InputNumber '(.*)' degeri ile Set edilir")]
        public void Set(string val) {
            antModel.SetText(val);
        }

        [StepDefinition(@"InputNumber '(.*)' olup olmadigi kontrol edilir")]
        public void Check(string val) {
            antModel.CheckText(val);
        }
    }
}
