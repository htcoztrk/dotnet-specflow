using System;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    internal interface IElementListInternal {
        object By { get; }
        IDriver Driver { get; set; }
        Locator Locator { get; set; }
        IElement ParentElement { get; set; }
        TimeSpan Timeout { get; set; }
        bool IsPopulated { get; set; }
    }

}