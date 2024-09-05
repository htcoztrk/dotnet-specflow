using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Factories;
using TestAutomation.Framework.DomainLayer.Models.Attributes;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.Framework.DomainLayer.Models.Entities {
    public class Context {
        private readonly Test test;
        private IDictionary<string, IXmlContext> dataXMLContextDictionary;
        private IDictionary<string, ISqlContext> dataSQLContextDictionary;

        public Context(Test test) {
            this.test = test;
        }

        public IDictionary<string, IXmlContext> XmlData
        {
            get
            {
                if (dataXMLContextDictionary == null) {
                    Fetch();
                }

                return dataXMLContextDictionary;
            }
        }

        public string DefaultXmlDataKey
        {
            get
            {
                var key = this.XmlData.Keys;

                return key.FirstOrDefault();
            }
        }

        public IDictionary<string, ISqlContext> SqlData
        {
            get
            {
                if (dataSQLContextDictionary == null) {
                    Fetch();
                }

                return dataSQLContextDictionary;
            }
        }

        private List<Attribute> GetAttributeSource<T>() {
            List<Attribute> attributeList = new List<Attribute>();

            foreach (Attribute attribute in test.GetType().GetCustomAttributes()) {
                if (!attribute.GetType().Equals(typeof(T))) {
                    continue;
                }

                attributeList.Add(attribute);
            }

            return attributeList;
        }

        private static ProjectSource GetProjectSource(Context context) {
            string[] splittedNamespace = context.test.GetType().Namespace.Split('.');

            List<string> projectTestSourceFolder = new List<string>();

            int index = 0;

            for (int i = 0; i < splittedNamespace.Length; i++) {
                if (!splittedNamespace[i].Equals("Tests"))
                    continue;
                index = i;
                break;
            }

            for (int i = index + 1; i < splittedNamespace.Length; i++) {
                projectTestSourceFolder.Add(splittedNamespace[i]);
            }

            return new ProjectSource(string.Join(@"\", projectTestSourceFolder.ToArray()));
        }

        private void Fetch() {
            foreach (DataSource dataSource in GetAttributeSource<DataSource>().OfType<DataSource>()) {
                if (dataSource.Type.Equals(DataSourceType.XML)) {
                    FillXmlData(dataSource);
                }
                else if (dataSource.Type.Equals(DataSourceType.SQL)) {
                    FillSqlData(dataSource);
                }
                else {
                    throw new InvalidOperationException("Data source type is invalid");
                }
            }
        }

        private void FillXmlData(DataSource dataSource) {
            dataXMLContextDictionary =
                dataXMLContextDictionary ?? new Dictionary<string, IXmlContext>();

            IXmlContext xmlData =
                DataContextFactory.GetXmlContext(dataSource, GetProjectSource(this));

            if (xmlData == null) {
                return;
            }

            dataXMLContextDictionary.Add(dataSource.DataKey, xmlData);
        }

        private void FillSqlData(DataSource dataSource) {

            dataSQLContextDictionary =
                dataSQLContextDictionary ?? new Dictionary<string, ISqlContext>();

            ISqlContext sqlData =
                DataContextFactory.GetSqlContext(dataSource.FileName);

            if (sqlData == null) {
                return;
            }

            dataSQLContextDictionary.Add(dataSource.DataKey, sqlData);
        }
    }
}
