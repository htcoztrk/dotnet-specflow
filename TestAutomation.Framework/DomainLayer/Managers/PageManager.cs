using TestAutomation.Framework.DomainLayer.Builders;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Container;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.DomainLayer.Services;
using System;

namespace TestAutomation.Framework.DomainLayer.Managers {
    public static class PageManager {
        public static Model BindModel(Test test, Model model) {
            if (model == null)
                throw new ArgumentNullException(String.Format("{0} is null! "+model));
            if (GetMap(test).ContainsKey(model.GetType())) {
                return (Model)GetMap(test).Get(model.GetType());
            }

            PageBuilder pageBuilder = new PageBuilder(test, model);
            pageBuilder.SetProperties();
            pageBuilder.SetElements();
            pageBuilder.SetModelFields();
            pageBuilder.InvokeSetup();

            Model pageModel = pageBuilder.GetModel();

            ContainerService.Add<PageContainer>(
                test, model.GetType(), pageModel);

            return pageModel;
        }

        private static IContainer GetMap(Test test) {
            return ContainerService.Get<PageContainer>(test);
        }
    }
}
