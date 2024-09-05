using System;
using System.Collections;
using System.Collections.Generic;
using OpenQA.Selenium;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Events.Args;
using TestAutomation.Framework.DomainLayer.Events.Handlers;
using TestAutomation.Framework.DomainLayer.Events.Interfaces;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Utils;
using IWinAppDriver = OpenQA.Selenium.Appium.Windows.WindowsDriver<OpenQA.Selenium.Appium.Windows.WindowsElement>;
using IWinAppElement = OpenQA.Selenium.Appium.Windows.WindowsElement;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Elements {
    class WinAppElementList : ElementList, IDesktopElementList, IEventRegisterable {

        private IList<IWinAppElement> bindedList;
        private IList<WinAppElement> elementList;
        private By by;



        public IDesktopElement this[int index]
        {
            get
            {
                elementList = CheckStateAndBind();
                try {
                    return elementList[index];
                }
                catch (Exception) {
                    return elementList[index];
                }
            }
        }

        public override void SetBindedElementList(IList<IWinAppElement> bindedList) {
            this.bindedList = bindedList;
        }

        public override void SetElementList<T>(IList<T> elementList) {
            this.elementList = (IList<WinAppElement>)elementList;
        }

        public void RegisterEvents() {
            EventService.AddListener(this, new EventOnStaleElement(), BindElementsIfStale);
        }

        public int Count
        {
            get
            {
                try {
                    elementList = CheckStateAndBind();
                    return elementList.Count;
                }
                catch (Exception) {
                    //logger.Error(ex);
                    throw;
                }
            }
        }

        public void BindElementsForcefully() {
            try {
                PopulateList();
            }
            catch (Exception) {
                //logger.Error(ex);
            }
        }

        public IEnumerator GetEnumerator() {
            if (!IsPopulated) {
                PopulateList();
            }

            for (int i = 0; i < Count; i++) {
                yield return elementList[i];
            }
        }

        public override void SetBindedElementList(IList<OpenQA.Selenium.IWebElement> bindedList) {
            throw new NotImplementedException();
        }


        public void SetXPathParameters(params string[] xpathParameters) {
            try {
                Locator = ReplaceXPathParameters(xpathParameters);
                elementList = null;
            }
            catch (Exception) {
                //logger.Error(ex);
            }
        }

        private void BindElementsIfStale(object sender, IArgs args) {
            try {
                if (!IsStale()) {
                    return;
                }

                if (sender as WinAppElementList == this) {
                    PopulateList();
                }
            }
            catch (Exception) {
                throw;
            }
        }

        private static bool IsStale() {
            try {
                return false;
            }
            catch (StaleElementReferenceException) {
                return true;
            }
            catch (Exception) {
                throw;
            }
        }

        private IList<WinAppElement> PopulateList() {
            try {
                by = by ?? LocatorConverter.ToBy(Locator);

                IWinAppDriver unwrappedWebDriver = ((IAgent)Driver).GetDriver() as IWinAppDriver;

                if (ParentElement != null) {
                    ((Element)ParentElement).BindedElement =
                        ElementService.GetBindedElement((By)(((IElementInternal)ParentElement).By), unwrappedWebDriver);
                    bindedList = ElementService.GetBindedChildElements(
                        unwrappedWebDriver,
                     (IElementInternal)ParentElement,
                        elementList[0]);
                }
                else {
                    bindedList = ElementService.GetBindedElements(by, unwrappedWebDriver);
                }

                elementList = ElementService.GetElementList<WinAppElement>(this, bindedList);
                IsPopulated = true;

                return elementList;
            }
            catch (Exception) {
                throw;
            }
        }

        private IList<WinAppElement> CheckStateAndBind() {
            try {
                elementList = elementList ?? PopulateList();

                if (IsStale()) {
                    EventService.Invoke(new EventOnStaleElement(), new StaleElementArgs(new Message("Stale Element Invoked!")), this);
                }
                return elementList;
            }
            catch (Exception) {
                throw;
            }
        }
    }
}
