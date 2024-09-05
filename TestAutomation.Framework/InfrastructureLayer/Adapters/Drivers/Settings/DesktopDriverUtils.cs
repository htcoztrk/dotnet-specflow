using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers.Settings
{
    public static class DesktopDriverUtils
    {
        internal static string GetApplicationPath(DesktopAppType desktopAppType)
        {
            if (desktopAppType.Equals(DesktopAppType.Default))
            {
                return Config.GetInstance().DefaultDesktopAppPath;
            }
            else if (desktopAppType.Equals(DesktopAppType.Finally))
            {
                return Config.GetInstance().FinallyDesktopAppPath;
            }
            else
            {
                return Config.GetInstance().DefaultDesktopAppPath;
            }
        }
    }
}
