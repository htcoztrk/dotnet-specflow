using System;
using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Events.Interfaces;

namespace TestAutomation.Framework.DomainLayer.Events.Handlers {
    class EventOnSystemNotifier : IEvent {
        private static readonly Dictionary<object, Action<object, IArgs>> caughtSystemNotifierRequests = new Dictionary<object, Action<object, IArgs>>();

        public void RegisterEvent(object sender, Action<object, IArgs> eventDelegate) {
            if (!caughtSystemNotifierRequests.ContainsKey(sender)) {
                caughtSystemNotifierRequests.Add(sender, eventDelegate);
            }

        }

        public Dictionary<object, Action<object, IArgs>> GetEventHandler() {
            return caughtSystemNotifierRequests;
        }
    }
}
