using System.Collections.Generic;
using System.Collections;
using Intertech.TestAutomation.Framework.DomainLayer.Contracts;
using System;
using Intertech.TestAutomation.Framework.DomainLayer.POMBase;

namespace Intertech.TestAutomation.Framework.DomainLayer.Mappers
{
    public class PageMapper : IMapper
    {
        private readonly Dictionary<Type, Model> pageMap = new Dictionary<Type, Model>();
        
        public int Count
        {
            get
            {
                return pageMap.Count;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Model value in pageMap.Values)
            {
                yield return value;
            }
        }

        public object this[object index]
        {
            get
            {
                return pageMap[(Type)index];
            }
            set
            {
                pageMap[(Type)index] = (Model)value;
            }
        }


        public object Get(object key)
        {
            return pageMap[(Type)key];
        }

        public object GetAll()
        {
            return pageMap;
        }

        public void Add(object key, object value)
        {
            pageMap.Add((Type)key, (Model)value);
        }
        
        
        public void Clear()
        {
            pageMap.Clear();
        }

        public bool ContainsValue(object value)
        {
            return pageMap.ContainsValue((Model) value);

        }

        public bool ContainsKey(object key)
        {
            return pageMap.ContainsKey((Type)key);

        }

        public void Remove(object key)
        {
            pageMap.Remove((Type)key);
        }
    }
}
