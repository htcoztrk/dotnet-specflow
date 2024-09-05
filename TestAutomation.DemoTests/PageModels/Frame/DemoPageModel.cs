using System;
using NUnit.Framework;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Attributes;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.DemoTests.PageModels.Frame {
    public class DemoPageModel : Model {

        [FindsBy("id=myFrame3")]
        public IWebElement fraDemo3;

        [FindsBy("id=checkBox6")]
        public IWebElement checkbox6;

        public void LaunchUrl(string url) {
            WebApp.Navigate(url);
        }


        public void SwitchToFra3() {
            //Assert.True(WebApp.IsElementVisible(fraTester, TimeSpan.FromSeconds(15)));
            WebApp.SwitchToFrame(fraDemo3);
        }

        public void CheckTheBox() {
            checkbox6.Click();
        }

        public bool IsBoxChecked() {
            return checkbox6.GetAttribute("checked") == "true";
        }
    }
}