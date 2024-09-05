using NUnit.Framework;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Attributes;
using TestAutomation.Framework.DomainLayer.POMBase;
using Keys = OpenQA.Selenium.Keys;

namespace TestAutomation.DemoTests.PageModels.Ant {
    public class AntModel : Model {
        [FindsBy("css=#input-number-demo-basic>section.code-box-demo>div>div.ant-input-number-input-wrap>input")]
        public IWebElement input;

        public void LaunchUrl(string url) {
            WebApp.Navigate(url);
        }

        public void Clear() {
            input.Click();
            input.SendKeys(Keys.Control + "A");
            input.SendKeys(Keys.Delete);
        }

        public void SetText(string val) {
            input.SetText(val);
        }

        public void CheckText(string val) {
            Assert.AreEqual(val, input.GetAttribute("value"));
        }
    }
}
