using System.Collections;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    public interface IContainer : IEnumerable {
        object this[object key] { get; set; }
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