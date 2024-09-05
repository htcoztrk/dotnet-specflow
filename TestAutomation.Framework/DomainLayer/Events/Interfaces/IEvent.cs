using System;
using System.Collections.Generic;

namespace TestAutomation.Framework.DomainLayer.Events.Interfaces {
    public interface IEvent {
        Dictionary<object, Action<object, IArgs>> GetEventHandler();

        void RegisterEvent(object sender, Action<object, IArgs> eventDelegate);
    }
}
