using System;
using System.Reflection;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Models.Attributes;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.POMBase;

namespace TestAutomation.Framework.DomainLayer.Builders {
    public class PageBuilder {
        private Model Page { get; set; }
        private Test Test { get; set; }

        private readonly IDriver webDriver;

        private readonly IDriver desktopAppDriver;

        public PageBuilder(Test test, Model model) {
            Page = model;
            Test = test;
            webDriver = DriverManager.GetDriver(Test, TestEnvironment.WEBAPP);
            desktopAppDriver = DriverManager.GetDriver(Test, TestEnvironment.DESKTOPAPP);

        }

        public void SetProperties() {
            Page.Test = Test;
        }

        public void SetElements() {
            foreach (FieldInfo field in Page.GetType().GetFields()) {
                if (Attribute.GetCustomAttribute(field.FieldType, typeof(ElementType)) == null) {
                    continue;
                }

                FindsBy elementAttribute = (FindsBy)field.GetCustomAttribute(typeof(FindsBy));

                if (elementAttribute == null) {
                    continue;
                }

                Locator by = new Locator(elementAttribute.Locator);

                TimeSpan elementTimeout = TimeSpan.FromSeconds(elementAttribute.Timeout);

                if (field.FieldType.Equals(typeof(IWebElementList))) {
                    field.SetValue(
                        Page,
                        webDriver.GetElementList(field.FieldType, by, elementTimeout)
                        );
                }
                else if (field.FieldType.Equals(typeof(IDesktopElementList))) {
                    field.SetValue(
                        Page,
                        desktopAppDriver.GetElementList(field.FieldType, by, elementTimeout)
                        );
                }
                else if (field.FieldType.Equals(typeof(IWebElement))) {
                    field.SetValue(
                        Page,
                        webDriver.GetElement(field.FieldType, by, elementTimeout)
                        );
                }
                else if (field.FieldType.Equals(typeof(IDesktopElement))) {
                    field.SetValue(
                        Page,
                        desktopAppDriver.GetElement(field.FieldType, by, elementTimeout)
                        );
                }
                else {
                    throw new InvalidOperationException("Element tipi kontrol edilmeli.");
                }
            }
        }

        public void SetModelFields() {
            const BindingFlags flags = BindingFlags.Public |
                             BindingFlags.NonPublic |
                             BindingFlags.Static |
                             BindingFlags.Instance |
                             BindingFlags.DeclaredOnly;

            foreach (FieldInfo field in Page.GetType().GetFields(flags)) {
                if (!field.FieldType.IsSubclassOf(typeof(Model))) {
                    continue;
                }

                Model model = (Model)Activator.CreateInstance(field.FieldType);

                model = PageManager.BindModel(Test, model);

                field.SetValue(Page, model);
            }
        }


        public void InvokeSetup() {
            if (Page.GetType().IsSubclassOf(typeof(Model))) {
                Page.Init();
            }
        }


        public Model GetModel() {
            return Page;
        }

    }
}
