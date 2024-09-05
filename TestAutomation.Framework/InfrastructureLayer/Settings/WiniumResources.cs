using System;

namespace TestAutomation.Framework.InfrastructureLayer.Settings {
    public class WiniumResources : IResources {
        public void CreateDriversIntoFolder(string modifiedPath) {
            try {
                byte[] exeBytes = Properties.Resources.Winium_Desktop_Driver;
                string exeToRun = modifiedPath + DriverResources.WiniumDriverName;
                Config.GetInstance().CreateResources(exeBytes, exeToRun);
            }
            catch (Exception ex) {
                string mlResult = null;// ServiceLocator.Create<IStringService>().GetString("TestAuto_WiniumDriverResource");
                //logger.Error(ex);
                throw new Exception(message: mlResult, innerException: ex);
            }
        }
    }
}
