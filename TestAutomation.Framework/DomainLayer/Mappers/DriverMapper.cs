using System.Collections.Generic;
using System.Collections;
using Intertech.TestAutomation.Framework.DomainLayer.Contracts;
using Intertech.TestAutomation.Framework.DomainLayer.Utils.Enums;

namespace Intertech.TestAutomation.Framework.DomainLayer.Mappers
{
    public class DriverMapper : IMapper
    {
        private readonly Dictionary<DataSourceType, IDriver> driverMap = new Dictionary<DataSourceType, IDriver>();
        
        public int Count
        {
            get
            {
                return driverMap.Count;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (IDriver value in driverMap.Values)
            {
                yield return value;
            }
        }

        public object this[object index]
        {
            get
            {
                return driverMap[(DataSourceType)index];
            }
            set
            {
                driverMap[(DataSourceType)index] = (IDriver)value;
            }
        }


        public object Get(object key)
        {
            return driverMap[(DataSourceType)key];
        }

        public object GetAll()
        {
            return driverMap;
        }

        public void Add(object key, object value)
        {
            driverMap.Add((DataSourceType)key, (IDriver)value);
        }
        
        
        public void Clear()
        {
            driverMap.Clear();
        }

        public bool ContainsValue(object value)
        {
            return driverMap.ContainsValue((IDriver) value);

        }

        public bool ContainsKey(object key)
        {
            return driverMap.ContainsKey((DataSourceType)key);

        }

        public void Remove(object key)
        {
            driverMap.Remove((DataSourceType)key);
        }
    }
}
