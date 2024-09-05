using System;
using System.Reflection;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.Framework.DomainLayer.Builders {
    public static class TestBuilder {
        public static void SetTestModelFields(Test test) {
            const BindingFlags flags = BindingFlags.Public |
                             BindingFlags.NonPublic |
                             BindingFlags.Static |
                             BindingFlags.Instance;

            if (test == null) {
                return;
            }
            foreach (FieldInfo field in test.GetType().GetFields(flags)) {
                if (!field.FieldType.IsSubclassOf(typeof(Model))) {
                    continue;
                }

                Model Page = (Model)Activator.CreateInstance(field.FieldType);


                Page = PageManager.BindModel(test, Page);

                field.SetValue(test, Page);
            }
        }
    }
}
