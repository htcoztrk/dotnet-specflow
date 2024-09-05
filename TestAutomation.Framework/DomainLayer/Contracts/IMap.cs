using System.Collections;

namespace Intertech.TestAutomation.Framework.DomainLayer.Contracts
{
    public interface IMapper : IEnumerable
    {
        object this[object index] { get; set; }
        int Count { get; }
        void Add(object key, object value);
        void Clear();
        bool ContainsKey(object key);
        bool ContainsValue(object value);
        object Get(object key);
        object GetAll();
        void Remove(object key);
    }
}