using System;
using System.Collections;
using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.Framework.DomainLayer.Container {
    public class PageContainer : IContainer {
        private readonly Dictionary<Type, Model> pageMap = new Dictionary<Type, Model>();

        public int Count
        {
            get
            {
                return pageMap.Count;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            foreach (Model value in pageMap.Values) {
                yield return value;
            }
        }

        public object this[object key]
        {
            get
            {
                return pageMap[(Type)key];
            }
            set
            {
                pageMap[(Type)key] = (Model)value;
            }
        }


        public object Get(object key) {
            return pageMap[(Type)key];
        }

        public object GetAll() {
            return pageMap;
        }

        public void Add(object key, object value) {
            try {
                pageMap.Add((Type)key, (Model)value);
            }
            catch (ArgumentException) {
                throw new ArgumentException("Page Container'da aynı key tekrar eklenemez.");
            }
        }

        public void Clear() {
            pageMap.Clear();
        }

        public bool ContainsValue(object value) {
            return pageMap.ContainsValue((Model)value);

        }

        public bool ContainsKey(object key) {
            return pageMap.ContainsKey((Type)key);
        }

        public void Remove(object key) {
            if (!ContainsKey(key)) {
                throw new KeyNotFoundException(string.Format("Böyle bir key bulunamadı.{0}", key));
            }
            pageMap.Remove((Type)key);
        }
    }
}
