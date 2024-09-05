using Intertech.TestAutomation.Framework.DomainLayer.Contracts;
using Intertech.TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace Intertech.TestAutomation.Framework.DomainLayer.Factories
{
    public class ElementFactory
    {      
        private readonly Locator locator;
        private readonly IDriver driver;

        public ElementFactory(IDriver driver, Locator locator)
        {
            this.driver = driver; 
            this.locator = locator;
        }

        public IElement GetElement<T>() where T : IElement, new()
        {
            IElement element = new T();

            element = (IElement)UpdateStateAndGet((IDriverElement)element);

            ((IEventRegisterable)element).RegisterEvents();

            return element;
        }


        public IElementList GetElementList<T>() where T : IElementList, new()
        {
            IElementList element = new T();

            element = (IElementList)UpdateStateAndGet((IDriverElement)element);

            ((IEventRegisterable)element).RegisterEvents();

            return element;
        }

        private IDriverElement UpdateStateAndGet(IDriverElement element)
        {
            element.Locator = locator;
            element.Driver = driver;

            return element;
        }
    }
}
