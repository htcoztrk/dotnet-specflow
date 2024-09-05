using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Events.Args;
using TestAutomation.Framework.DomainLayer.Events.Handlers;
using TestAutomation.Framework.DomainLayer.Events.Interfaces;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Services;
using ISeleniumWebElement = OpenQA.Selenium.IWebElement;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;
using System;
using OpenQA.Selenium;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.InfrastructureLayer.Utils;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Container;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Elements {
    internal abstract class Element : IElementInternal, IEventRegisterable {
        protected ISeleniumWebElement BoundElement;

        public IDriver Driver { get; set; }

        protected Locator OriginalLocator { get; set; } = null;

        private Locator locator = null;

        private Session session;

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

        public Session Session
        {
            get
            {
                if (session == null) {
                    Test test = ContainerService.GetParentByElement<DriverContainer>(this);
                    session = DriverManager.GetSession(test);
                }

                return session;
            }
        }

        public IElement ParentElement { get; set; }

        public int ListOrder { get; set; } = -1;

        public object BindedElement
        {
            get
            {
                return BoundElement ?? Init();
            }
            set
            {
                BoundElement = value as ISeleniumWebElement;
            }
        }

        protected ISeleniumWebElement Init() {
            try {
                ISeleniumWebDriver unwrappedWebDriver = (ISeleniumWebDriver)((IAgent)Driver).GetDriver();
                bool isListElement = ListOrder > -1;

                if (ParentElement != null) {
                    ((Element)ParentElement).BindedElement =
                        ElementService.GetBindedElement((By)(((IElementInternal)ParentElement).By), unwrappedWebDriver, Timeout);
                }

                if (isListElement && ParentElement != null) {
                    BoundElement =
                        ElementService.GetBindedChildElements(
                            unwrappedWebDriver,
                            (IElementInternal)ParentElement,
                            this)[ListOrder];

                }
                else if (!isListElement && ParentElement != null) {
                    BoundElement =
                        ElementService.GetBindedChildElement(
                            unwrappedWebDriver,
                            (IElementInternal)ParentElement,
                            this);
                }
                else if (isListElement) {
                    BoundElement = ElementService.GetBindedElements((By)By, unwrappedWebDriver, Timeout)[ListOrder];
                }
                else {
                    BoundElement = ElementService.GetBindedElement((By)By, unwrappedWebDriver, Timeout);
                }

                return BoundElement;
            }
            catch (WebDriverTimeoutException) {
                throw new NoSuchElementException($"Element bulunamadı.\nLocator: {Locator.By}");
            }
            catch (Exception) {
                throw;
            }
        }

        public virtual void RegisterEvents() {
            EventService.AddListener(this, new EventOnStaleElement(), BindElementIfStale);
        }

        protected void BindElementIfStale(object sender, IArgs args) {
            try {
                if (!IsStale()) {
                    return;
                }
                Init();
            }
            catch (Exception) {
                throw;
            }
        }

        protected bool IsStale() {
            try {
                bool isStale = BoundElement.Enabled;
            }
            catch (StaleElementReferenceException) {
                return true;
            }
            catch (InvalidOperationException) {
                return true;
            }

            return false;
        }

        protected ISeleniumWebElement CheckStateAndBind() {
            try {
                BoundElement = BoundElement ?? Init();

                if (BoundElement == null) {
                    return BoundElement;
                }
                else if (IsStale()) {
                    EventService.Invoke(
                        new EventOnStaleElement(),
                        new StaleElementArgs(
                            new Message("Stale Element Invoked!")),
                        this);
                }
                else {
                    return BoundElement;
                }

                return BoundElement;
            }
            catch (NoSuchElementException) {
                throw new NoSuchElementException($"Element bulunamadı.\nLocator: {Locator.By}");
            }
            catch (ArgumentNullException) {
                throw new NoSuchElementException($"Element bulunamadı.\nLocator: {Locator.By}");
            }
            catch (Exception) {
                throw;
            }
        }

        public void SetLocatorParameters(params string[] locatorParameters) {
            string replacedBy = OriginalLocator.By;

            for (int i = 0; i < locatorParameters.Length; i++) {
                replacedBy = replacedBy.Replace("{" + (i + 1) + "}", locatorParameters[i]);
            }

            Locator = new Locator(replacedBy);

            BoundElement = null;
        }
    }
}
