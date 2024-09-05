using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Attributes;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.InfrastructureLayer.Adapters.DataContexts;

namespace TestAutomation.Framework.DomainLayer.Factories {
    public static class DataContextFactory {
        public static IXmlContext GetXmlContext(DataSource dataSource, ProjectSource projectSource) {
            return dataSource != null ? new XmlContext(dataSource.FileName, projectSource) : null;
        }
        public static IXmlContext GetXmlContext(string dataSource) {
            return new XmlContext(dataSource);
        }
        public static ISqlContext GetSqlContext(string connectionString) {
            return new DapperContext(connectionString);
        }
    }
}
