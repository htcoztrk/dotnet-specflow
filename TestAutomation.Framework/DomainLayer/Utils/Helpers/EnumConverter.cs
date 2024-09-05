using Intertech.TestAutomation.Framework.DomainLayer.Utils.Enums;
using System.Collections.Generic;

namespace Intertech.TestAutomation.Framework.DomainLayer.Utils.Helpers
{
    public class EnumConverter
    {
        public static T ConvertStringToEnum<T>(string enumValue)
        {
            object convertedEnum = null;

            if (typeof(T).Equals(typeof(ExecutionEnvironment)))
            {
                if (enumValue.Equals("LOCALHOST"))
                {
                    convertedEnum = ExecutionEnvironment.LOCALHOST;
                }
                else if (enumValue.Equals("REMOTE"))
                {
                    convertedEnum = ExecutionEnvironment.REMOTE;
                }
                else if (enumValue.Equals("TESTINIUM"))
                {
                    convertedEnum = ExecutionEnvironment.TESTINIUM;
                }
            }
            else if(typeof(T).Equals(typeof(Platform)))
            {
                if (enumValue.Equals("WEBCHROME"))
                {
                    convertedEnum = Platform.WEB_CHROME;
                }
                else if (enumValue.Equals("WEBINTERNETEXPLORER"))
                {
                    convertedEnum = Platform.WEB_INTERNET_EXPLORER;
                }
                else if (enumValue.Equals("WEBFIREFOX"))
                {
                    convertedEnum = Platform.WEB_FIREFOX;
                }
                else if (enumValue.Equals("WEBSAFARI"))
                {
                    convertedEnum = Platform.WEB_SAFARI;
                }
                else if (enumValue.Equals("WINDOWSDESKTOPAPP"))
                {
                    convertedEnum = Platform.WINDOWS_DESKTOP_APP;
                }
                else if (enumValue.Equals("WEBCHROMEHEADLESS"))
                {
                    convertedEnum = Platform.WEB_CHROME_HEADLESS;
                }
            }


            return (T)convertedEnum;
        }

      


        public static IList<T> ConvertListToEnum<T>(string[] enumValues)
        {
            IList<T> convertedEnumList = new List<T>();

            foreach (string val in enumValues)
            {
                T brow = ConvertStringToEnum<T>(val);
                convertedEnumList.Add(brow);
            }

            return convertedEnumList;

        }

        public static IList<Platform> GetPlatformEnumsAsList(string platforms)
        {
            string[] platformArr = platforms.Split(',');
            IList<Platform> platformTypeList = EnumConverter.ConvertListToEnum<Platform>(platformArr);
            return platformTypeList;
        }

        public static TestEnvironment GetPlatformTestEnvironment(Platform platform)
        {
            if (platform.Equals(Platform.WEB_CHROME) ||
                platform.Equals(Platform.WEB_CHROME_HEADLESS) ||
                platform.Equals(Platform.WEB_FIREFOX) ||
                platform.Equals(Platform.WEB_INTERNET_EXPLORER) ||
                platform.Equals(Platform.WEB_SAFARI))
            {
                return TestEnvironment.WEBAPP;
            }
            else if (platform.Equals(Platform.WINDOWS_DESKTOP_APP))
            {
                return TestEnvironment.DESKTOPAPP;
            }
            else if (platform.Equals(Platform.ANDROID_APP) ||
                platform.Equals(Platform.ANDROID_CHROME) ||
                platform.Equals(Platform.ANDROID_FIREFOX) ||
                platform.Equals(Platform.ANDROID_NATIVE_GOOGLE) ||
                platform.Equals(Platform.IOS_APP) ||
                platform.Equals(Platform.IOS_SAFARI))
            {
                return TestEnvironment.MOBILEAPP;
            }
            else
            {
                return TestEnvironment.NONE;
            }

        }
    }



}
