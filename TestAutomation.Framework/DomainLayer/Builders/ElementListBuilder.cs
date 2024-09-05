using System;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace TestAutomation.Framework.DomainLayer.Builders {
    internal class ElementListBuilder<T> where T : IElementListInternal, new() {
        private readonly IElementListInternal element;

        public ElementListBuilder() {
            element = new T();
        }

        public object Get() {
            return element;
        }

        public void InvokeRegisterEvents() {
            ((IEventRegisterable)element).RegisterEvents();
        }

        public void SetLocator(Locator locator) {
            element.Locator = locator;
        }

        public void SetTimeout(TimeSpan timeout) {
            element.Timeout = timeout;
        }

        public void SetDriver(IDriver driver) {
            element.Driver = driver;
        }
    }
}
