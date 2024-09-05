using System;
using TestAutomation.Framework.DomainLayer.Models.Enums;

namespace TestAutomation.Framework.DomainLayer.Models.Attributes {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DataSource : Attribute {
        public string DataKey { get; set; }
        public DataSourceType Type { get; set; }
        public string FileName { get; set; }
        public string MethodName { get; set; }
    }
}

