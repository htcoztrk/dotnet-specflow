using System;
using NUnit.Framework;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Attributes;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.DemoTests.PageModels.Frame {
    public class W3SchoolsModel : Model {

        [FindsBy("tagName=iframe")]
        public IWebElement fra;

        [FindsBy("tagName=div")]
        public IWebElement div;

        public void SwitchToFrameTag() {
            WebApp.Maximize();
            Assert.True(WebApp.IsElementVisible(fra, TimeSpan.FromSeconds(15)));
            WebApp.SwitchToFrame(fra);
        }
        public string GetTagNameOfFirstDiv() {
            return div.GetTagName();
        }
    }
}