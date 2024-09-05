namespace TestAutomation.Framework.DomainLayer.Contracts {
    public interface IXmlContext : IContext {
        string GetValueByXPath(string path);
        string GetValueWithKey(string key);
        T Deserialize<T>();
    }
}
