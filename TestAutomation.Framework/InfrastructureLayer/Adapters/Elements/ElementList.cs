using TestAutomation.Framework.DomainLayer.Contracts;
using System.Collections.Generic;
using ISeleniumWebElement = OpenQA.Selenium.IWebElement;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.InfrastructureLayer.Utils;
using System;
using IWinAppElement = OpenQA.Selenium.Appium.Windows.WindowsElement;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Elements {
    internal abstract class ElementList : IElementListInternal {

        public IDriver Driver { get; set; }

        protected Locator OriginalLocator { get; set; } = null;

        private Locator locator = null;

        public Locator Locator
        {
            get { return locator; }
            set
            {
                OriginalLocator = OriginalLocator ?? value;

                locator = value;
            }
        }

        public TimeSpan Timeout { get; set; }

        public object By
        {
            get
            {
                return LocatorConverter.ToBy(Locator);
            }
        }

        public IElement ParentElement { get; set; }

        public bool IsPopulated { get; set; } = false;

        public abstract void SetBindedElementList(IList<ISeleniumWebElement> bindedList);

        public abstract void SetBindedElementList(IList<IWinAppElement> bindedList);

        public abstract void SetElementList<T>(IList<T> elementList);

        public Locator ReplaceXPathParameters(params string[] xpathParameters) {
            string replacedBy = OriginalLocator.By;

            for (int i = 0; i < xpathParameters.Length; i++) {
                replacedBy = replacedBy.Replace("{" + (i + 1) + "}", xpathParameters[i]);
            }

            return new Locator(replacedBy);
        }
    }
}
