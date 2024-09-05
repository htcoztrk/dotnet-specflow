using System;
using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Keyboard;

namespace TestAutomation.Framework.DomainLayer.Factories {
    static class KeyboardFactory {
        static readonly Dictionary<KeyboardKey, string> keys = new Dictionary<KeyboardKey, string>();

        static KeyboardFactory() {
            InitKeyValues();
        }
        static void InitKeyValues() {
            keys.Add(KeyboardKey.TAB, Keyboard.Tab);
            keys.Add(KeyboardKey.ESC, Keyboard.Escape);
            keys.Add(KeyboardKey.CTRL, Keyboard.Control);
            keys.Add(KeyboardKey.ALT, Keyboard.Alt);
            keys.Add(KeyboardKey.SHIFT, Keyboard.Shift);
            keys.Add(KeyboardKey.CLEAR, Keyboard.Clear);
            keys.Add(KeyboardKey.ENTER, Keyboard.Enter);
            keys.Add(KeyboardKey.HOME, Keyboard.Home);
            keys.Add(KeyboardKey.END, Keyboard.End);
            keys.Add(KeyboardKey.PAGEDOWN, Keyboard.PageDown);
            keys.Add(KeyboardKey.PAGEUP, Keyboard.PageUp);
            keys.Add(KeyboardKey.F2, Keyboard.F2);
            keys.Add(KeyboardKey.F8, Keyboard.F8);
            keys.Add(KeyboardKey.F9, Keyboard.F9);
            keys.Add(KeyboardKey.DOWN, Keyboard.Down);
            keys.Add(KeyboardKey.UP, Keyboard.Up);
            keys.Add(KeyboardKey.LEFT, Keyboard.Left);
            keys.Add(KeyboardKey.RIGHT, Keyboard.Right);
            keys.Add(KeyboardKey.SPACE, Keyboard.Space);
        }

        public static string Get(KeyboardKey key) {
            if (keys.ContainsKey(key))
                return keys[key];
            else throw new NotImplementedException("key not implemented " + key);
        }
    }
}
