using System;
using System.Collections;
using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.Framework.DomainLayer.Container {
    public class ProxyServerContainer : IContainer {
        private readonly Dictionary<Test, ProxyServer> proxyMap = new Dictionary<Test, ProxyServer>();

        public int Count
        {
            get
            {
                return proxyMap.Count;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            foreach (ProxyServer value in proxyMap.Values) {
                yield return value;
            }
        }

        public object this[object key]
        {
            get
            {
                return proxyMap[(Test)key];
            }
            set
            {
                proxyMap[(Test)key] = (ProxyServer)value;
            }
        }

        public object Get(object key) {
            return proxyMap[(Test)key];
        }

        public object GetAll() {
            return proxyMap;
        }

        public void Add(object key, object value) {
            try {
                proxyMap.Add((Test)key, (ProxyServer)value);
            }
            catch (ArgumentException) {
                throw new ArgumentException("Proxy Sever Container'da aynı key tekrar eklenemez.");
            }
        }

        public void Clear() {
            proxyMap.Clear();
        }

        public bool ContainsValue(object value) {
            return proxyMap.ContainsValue((ProxyServer)value);
        }

        public bool ContainsKey(object key) {
            return proxyMap.ContainsKey((Test)key);
        }

        public void Remove(object key) {
            if (!ContainsKey(key)) {
                throw new KeyNotFoundException(string.Format("Böyle bir key bulunamadı.{0}", key));
            }
            proxyMap.Remove((Test)key);
        }
    }
}
