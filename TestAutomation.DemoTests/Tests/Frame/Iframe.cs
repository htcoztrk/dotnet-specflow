using System;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.DemoTests.PageModels.Frame;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.DemoTests.Tests.Frame {
    [Binding, Scope(Feature = "IFrame")]
    public class Iframe : Test {
        readonly DemoPageModel DemoPageModel;
        readonly W3SchoolsModel w3SchoolsModel;
        readonly FrameTesterModel frameTesterModel;


        [StepDefinition(@"'(.*)' URL açılır")]
        public void LaunchIntervision(string url) {
            DemoPageModel.LaunchUrl(url);
        }

        [StepDefinition(@"W3Schools iframe geçişi yapılır")]
        public void SwitchW3SchoolsFrame() {
            w3SchoolsModel.SwitchToFrameTag();
        }

        [StepDefinition(@"DemoPage iframe geçişi yapılır")]
        public void SwitchDemoPageFrame() {
            DemoPageModel.SwitchToFra3();
        }

        [StepDefinition(@"iframetester iframe geçişi yapılır")]
        public void SwitchTesterFrame() {
            frameTesterModel.SwitchFrame();
        }

        [StepDefinition(@"Wikipedia başlığı tıklanır")]
        public void ClickWikipediaHref() {
            frameTesterModel.ClickWikipediaHref();
        }

        [StepDefinition(@"Wikipedia logosu üzerindeki yazı kontrol edilir")]
        public void GetTableCaption() {
            string wikiText = frameTesterModel.GetTableCaption();
            Assert.AreEqual("Wikipedia", wikiText);
        }

        [StepDefinition(@"İlk bulunan div elementinin tagname'i okunur")]
        public void ClickDiv() {
            Console.WriteLine(w3SchoolsModel.GetTagNameOfFirstDiv());
        }

        [StepDefinition(@"Checkbox işaretlenir")]
        public void Check() {
            DemoPageModel.CheckTheBox();
        }

        [StepDefinition(@"Checkbox'ın işaretli olması kontrol edilir")]
        public void BlankStateVis() {
            bool isChecked = DemoPageModel.IsBoxChecked();
            Assert.True(isChecked);
            Console.WriteLine(isChecked);
        }
    }
}