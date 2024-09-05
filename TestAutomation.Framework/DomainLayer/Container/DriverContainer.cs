using System;
using System.Collections;
using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Enums;

namespace TestAutomation.Framework.DomainLayer.Container {
    public class DriverContainer : IContainer {
        private readonly Dictionary<TestEnvironment, IDriver> driverMap = new Dictionary<TestEnvironment, IDriver>();
        public int Count
        {
            get
            {
                return driverMap.Count;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            foreach (IDriver value in driverMap.Values) {
                yield return value;
            }
        }

        public object this[object key]
        {
            get
            {
                return driverMap[(TestEnvironment)key];
            }
            set
            {
                driverMap[(TestEnvironment)key] = (IDriver)value;
            }
        }


        public object Get(object key) {
            return driverMap[(TestEnvironment)key];
        }

        public object GetAll() {
            return driverMap;
        }

        public void Add(object key, object value) {
            try {
                driverMap.Add((TestEnvironment)key, (IDriver)value);
            }
            catch (ArgumentException) {
                throw new ArgumentException("Driver Container'da aynı key tekrar eklenemez.");
            }

        }

        public void Clear() {
            driverMap.Clear();
        }

        public bool ContainsValue(object value) {
            return driverMap.ContainsValue((IDriver)value);
        }

        public bool ContainsKey(object key) {
            return driverMap.ContainsKey((TestEnvironment)key);
        }

        public void Remove(object key) {
            if (!ContainsKey(key)) {
                throw new KeyNotFoundException(string.Format("Böyle bir key bulunamadı.{0}", key));
            }

            driverMap.Remove((TestEnvironment)key);
        }
    }
}
