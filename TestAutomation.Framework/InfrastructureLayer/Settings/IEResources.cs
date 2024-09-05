using System;

namespace TestAutomation.Framework.InfrastructureLayer.Settings {
    public class IEResources : IResources {

        public void CreateDriversIntoFolder(string modifiedPath) {
            try {
                byte[] exeBytes = Properties.Resources.IEDriverServer;
                string exeToRun = modifiedPath + DriverResources.IeDriverName;
                Config.GetInstance().CreateResources(exeBytes, exeToRun);
            }
            catch (Exception ex) {
                string mlResult = null;// ServiceLocator.Create<IStringService>().GetString("TestAuto_IEDriverResource");
                //logger.Error(ex);
                throw new Exception(message: mlResult, innerException: ex);
            }
        }
    }
}
