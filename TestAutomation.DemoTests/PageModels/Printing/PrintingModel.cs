using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Attributes;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.DemoTests.PageModels.Printing {
    public class PrintingModel : Model {

        [FindsBy("id=PrintBlackAndWhite")]
        public IWebElement BlackAndWhite;

        public void LaunchUrl(string url) {
            WebApp.Navigate(url);
        }

        public void ClickBlackAndWhite() {
            BlackAndWhite.Click();
        }
    }
}
