using System.Collections.Generic;
using System.Collections;
using Intertech.TestAutomation.Framework.DomainLayer.Contracts;
using Intertech.TestAutomation.Framework.DomainLayer.Models.Entities;
using Intertech.TestAutomation.Framework.DomainLayer.POMBase;

namespace Intertech.TestAutomation.Framework.DomainLayer.Mappers
{
    public class ProxyServerMapper : IMapper
    {
        private readonly Dictionary<Test, ProxyServer> proxyMap = new Dictionary<Test, ProxyServer>();
        
        public int Count
        {
            get
            {
                return proxyMap.Count;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (IDriver value in proxyMap.Values)
            {
                yield return value;
            }
        }

        public object this[object index]
        {
            get
            {
                return proxyMap[(Test)index];
            }
            set
            {
                proxyMap[(Test)index] = (ProxyServer)value;
            }
        }


        public object Get(object key)
        {
            return proxyMap[(Test)key];
        }

        public object GetAll()
        {
            return proxyMap;
        }

        public void Add(object key, object value)
        {
            proxyMap.Add((Test)key, (ProxyServer)value);
        }
        
        
        public void Clear()
        {
            proxyMap.Clear();
        }

        public bool ContainsValue(object value)
        {
            return proxyMap.ContainsValue((ProxyServer) value);

        }

        public bool ContainsKey(object key)
        {
            return proxyMap.ContainsKey((Test)key);

        }

        public void Remove(object key)
        {
            proxyMap.Remove((Test)key);
        }
    }
}
