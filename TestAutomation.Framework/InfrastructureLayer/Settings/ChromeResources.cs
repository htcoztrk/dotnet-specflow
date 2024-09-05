using System;

namespace TestAutomation.Framework.InfrastructureLayer.Settings {
    public class ChromeResources {
        /// <summary>
        /// extract chrome driver from resource to folder
        /// </summary>
        /// <param name="modifiedPath"></param>
        public void CreateDriversIntoFolder(string modifiedPath) {
            try {
                byte[] exeBytes = Properties.Resources.chromedriver;
                string exeToRun = modifiedPath + DriverResources.ChromeDriverName;
                Config.GetInstance().CreateResources(exeBytes, exeToRun);
            }
            catch (Exception) {
                //logger.Error(ex);
            }
        }
    }
}
