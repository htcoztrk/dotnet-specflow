using System.Collections.Generic;
using System.Collections;
using Intertech.TestAutomation.Framework.DomainLayer.Contracts;
using Intertech.TestAutomation.Framework.DomainLayer.Utils.Enums;

namespace Intertech.TestAutomation.Framework.DomainLayer.Mappers
{
    public class RepositoryMapper : IMapper
    {
        private readonly Dictionary<DataSourceType, IRepository> repositoryMap = new Dictionary<DataSourceType, IRepository>();
        
        public int Count
        {
            get
            {
                return repositoryMap.Count;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (IRepository value in repositoryMap.Values)
            {
                yield return value;
            }
        }

        public object this[object index]
        {
            get
            {
                return repositoryMap[(DataSourceType)index];
            }
            set
            {
                repositoryMap[(DataSourceType)index] = (IRepository)value;
            }
        }


        public object Get(object key)
        {
            return repositoryMap[(DataSourceType)key];
        }

        public object GetAll()
        {
            return repositoryMap;
        }

        public void Add(object key, object value)
        {
            repositoryMap.Add((DataSourceType)key, (IRepository)value);
        }
        
        
        public void Clear()
        {
            repositoryMap.Clear();
        }

        public bool ContainsValue(object value)
        {
            return repositoryMap.ContainsValue((IRepository) value);

        }

        public bool ContainsKey(object key)
        {
            return repositoryMap.ContainsKey((DataSourceType)key);

        }

        public void Remove(object key)
        {
            repositoryMap.Remove((DataSourceType)key);
        }
    }
}
