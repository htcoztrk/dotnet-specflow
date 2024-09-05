using System;
using NUnit.Framework;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Attributes;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.DemoTests.PageModels.Frame {
    public class FrameTesterModel : Model {
        [FindsBy("id=iframe-window")]
        public IWebElement iframeWindow;

        [FindsBy("css=#Welcome_to_Wikipedia > a")]
        public IWebElement wikipediaHref;

        [FindsBy("css=#mw-content-text > div.mw-content-ltr.mw-parser-output > table.infobox.vcard > caption")]
        public IWebElement tableCaption;

        public void LaunchUrl(string url) {
            WebApp.Navigate(url);
        }

        public void SwitchFrame() {
            Assert.True(WebApp.IsElementVisible(iframeWindow, TimeSpan.FromSeconds(15)));
            WebApp.SwitchToFrame(iframeWindow);
        }

        public void ClickWikipediaHref() {
            wikipediaHref.Click();
        }

        public string GetTableCaption() {
            return tableCaption.GetText();
        }
    }
}
