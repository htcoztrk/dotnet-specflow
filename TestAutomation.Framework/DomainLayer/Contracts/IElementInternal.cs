using System;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    internal interface IElementInternal {
        object BindedElement { get; set; }
        IElement ParentElement { get; set; }
        int ListOrder { get; set; }
        object By { get; }
        IDriver Driver { get; set; }
        Locator Locator { get; set; }
        TimeSpan Timeout { get; set; }
    }
}