using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Events.Args;
using TestAutomation.Framework.DomainLayer.Events.Handlers;
using TestAutomation.Framework.DomainLayer.Events.Interfaces;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Utils;
using IWiniumDesktopDriver = OpenQA.Selenium.IWebDriver;
using IWiniumDesktopElement = OpenQA.Selenium.IWebElement;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Elements {
    internal class DesktopElementList : ElementList, IDesktopElementList, IEventRegisterable {
        private IList<IWiniumDesktopElement> bindedList;
        private IList<DesktopElement> elementList;
        private By by;

        public override void SetBindedElementList(IList<IWiniumDesktopElement> bindedList) {
            this.bindedList = bindedList;
        }

        public override void SetElementList<T>(IList<T> elementList) {
            this.elementList = (IList<DesktopElement>)elementList;
        }

        public void RegisterEvents() {
            EventService.AddListener(this, new EventOnStaleElement(), BindElementsIfStale);
        }

        private void BindElementsIfStale(object sender, IArgs args) {
            try {
                if (!IsStale()) {
                    return;
                }

                if (sender as DesktopElementList == this) {
                    PopulateList();
                }
            }
            catch (Exception) {
                throw;
            }
        }

        private IList<DesktopElement> PopulateList() {
            try {
                by = by ?? LocatorConverter.ToBy(Locator);

                IWiniumDesktopDriver unwrappedWebDriver = ((IAgent)Driver).GetDriver() as IWiniumDesktopDriver;

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

                elementList = ElementService.GetElementList<DesktopElement>(this, bindedList);
                IsPopulated = true;

                return elementList;
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

        private IList<DesktopElement> CheckStateAndBind() {
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

        public IDesktopElement this[int index]
        {
            get
            {
                elementList = CheckStateAndBind();

                try {
                    return elementList[index];
                }
                catch (Exception ex) {
                    Assert.Fail("Element verilen index altında bulunamadı => " + ex);
                    return elementList[index];
                }
            }
        }

        public int Count
        {
            get
            {
                elementList = CheckStateAndBind();
                return elementList.Count;
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


        public void BindElementsForcefully() {
            try {
                PopulateList();
            }
            catch (Exception) {
            }
        }

        public void SetXPathParameters(params string[] xpathParameters) {
            Locator = ReplaceXPathParameters(xpathParameters);

            elementList = null;
        }

        public override void SetBindedElementList(IList<WindowsElement> bindedList) {
            throw new NotImplementedException();
        }
    }
}
