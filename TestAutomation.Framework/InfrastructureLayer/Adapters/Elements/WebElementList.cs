using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Events.Args;
using TestAutomation.Framework.DomainLayer.Events.Handlers;
using TestAutomation.Framework.DomainLayer.Events.Interfaces;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions;
using TestAutomation.Framework.InfrastructureLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Utils;
using By = OpenQA.Selenium.By;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;
using ISeleniumWebElement = OpenQA.Selenium.IWebElement;
using StaleElementReferenceException = OpenQA.Selenium.StaleElementReferenceException;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Elements {
    internal class WebElementList : ElementList, IEventRegisterable, IWebElementList {
        private bool isPopulated = false;
        private IList<ISeleniumWebElement> bindedList;
        private IList<WebElement> elementList;
        private By by;

        public override void SetBindedElementList(IList<ISeleniumWebElement> bindedList) {
            this.bindedList = bindedList;
        }
        public override void SetElementList<T>(IList<T> elementList) {
            this.elementList = (IList<WebElement>)elementList;
        }
        public void RegisterEvents() {
            EventService.AddListener(this, new EventOnStaleElement(), BindElementsIfStale);
        }
        private void BindElementsIfStale(object sender, IArgs args) {
            try {
                if (!IsStale()) {
                    return;
                }

                if (sender as WebElementList == this) {
                    PopulateList();
                }
            }
            catch (Exception) {
                throw;
            }
        }

        private IList<WebElement> PopulateList() {
            try {
                by = by ?? LocatorConverter.ToBy(Locator);

                ISeleniumWebDriver unwrappedWebDriver = ((IAgent)Driver).GetDriver() as ISeleniumWebDriver;

                if (ParentElement != null) {
                    ((Element)ParentElement).BindedElement =
                        ElementService.
                        GetBindedElement((By)(
                        ((IElementInternal)ParentElement).By), unwrappedWebDriver, Timeout);

                    bindedList = ElementService.
                        GetBindedChildElements(
                        unwrappedWebDriver,
                        (IElementInternal)ParentElement, elementList[0]);
                }
                else {
                    bindedList = ElementService.GetBindedElements(by, unwrappedWebDriver, Timeout);
                }

                elementList = ElementService.GetElementList<WebElement>(this, bindedList);

                isPopulated = true;

                return elementList;
            }
            catch (Exception) {
                throw;
            }
        }

        private bool IsStale() {
            WaitService.Wait(new ConditionOfPageLoaded((IAgent)Driver));
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

        private IList<WebElement> CheckStateAndBind() {
            try {
                elementList = elementList ?? PopulateList();

                if (IsStale()) {
                    EventService.Invoke(new EventOnStaleElement(),
                        new StaleElementArgs(
                            new Message("Stale Element Invoked!")),
                        this);
                }

                return elementList;
            }
            catch (Exception) {
                throw;
            }
        }

        public IWebElement this[int index]
        {
            get
            {
                try {
                    elementList = CheckStateAndBind();
                    return elementList[index];
                }
                catch (Exception ex) {
                    Assert.Fail("Element verilen index altında bulunamadı => " + ex);

                    return null;
                }
            }
        }

        public int Count
        {
            get
            {
                try {
                    elementList = CheckStateAndBind();
                    return elementList.Count;
                }
                catch (Exception ex) {
                    throw new Exception("Element verilen index altında bulunamadı => ", ex);
                }

            }
        }

        public IEnumerator GetEnumerator() {
            if (!isPopulated) {
                try {
                    PopulateList();
                }
                catch (Exception) {
                    //logger.Error(ex);
                    throw;
                }
            }

            for (int i = 0; i < Count; i++) {
                WebElement element = elementList[i];
                yield return element;
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

        public void SetXPathParameters(params string[] xpathParameters) {
            try {
                Locator = ReplaceXPathParameters(xpathParameters);
                elementList = null;
            }
            catch (Exception) {
                //logger.Error(ex);
            }
        }

        public override void SetBindedElementList(IList<WindowsElement> bindedList) {
            throw new NotImplementedException();
        }
    }
}
