using System;
using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Events.Interfaces;

namespace TestAutomation.Framework.DomainLayer.Services {
    static class EventService {
        public static void Invoke(IEvent type, IArgs args, object sender) {
            Dictionary<object, Action<object, IArgs>> handler = type.GetEventHandler();

            if (handler == null) {
                return;
            }

            if (sender != null && handler.ContainsKey(sender)) {
                (handler[sender])(sender, args);
                return;
            }

            if (sender != null) {
                return;
            }

            foreach (object obj in handler.Keys) {
                (handler[obj])(obj, args);
            }
        }

        public static void AddListener(object sender, IEvent type, Action<object, IArgs> eventDelegate) {
            type.RegisterEvent(sender, eventDelegate);
        }
    }
}
