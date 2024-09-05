using System;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    internal interface IDriver {
        IElement GetElement(Type elementType, Locator locator, TimeSpan timeout);

        IElementListInternal GetElementList(Type elementType, Locator locator, TimeSpan timeout);

        Session Session { get; }
    }
}