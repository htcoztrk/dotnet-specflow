
namespace TestAutomation.Framework.InfrastructureLayer.Settings {
    public static class DriverResources {
        public static string ChromeDriverName { get; private set; } = @"\chromedriver.exe";
        public static string IeDriverName { get; private set; } = @"\IEDriverServer.exe";
        public static string WiniumDriverName { get; private set; } = @"\Winium.Desktop.Driver.exe";
        public static string WinAppDriverName { get; private set; } = @"\WinAppDriver.exe";
    }
}
