using System;
using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Events.Interfaces;

namespace TestAutomation.Framework.DomainLayer.Events.Handlers {
    class EventOnStaleElement : IEvent {
        private static readonly Dictionary<object, Action<object, IArgs>> caughtStaleElementEventException = new Dictionary<object, Action<object, IArgs>>();

        public void RegisterEvent(object sender, Action<object, IArgs> eventDelegate) {
            if (!caughtStaleElementEventException.ContainsKey(sender)) {
                caughtStaleElementEventException.Add(sender, eventDelegate);
            }

        }

        public Dictionary<object, Action<object, IArgs>> GetEventHandler() {
            return caughtStaleElementEventException;
        }
    }
}
