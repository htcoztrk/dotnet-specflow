using System;
using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Models.Enums;

namespace TestAutomation.Framework.DomainLayer.Utils {
    /// <summary>
    /// Enum değerlerin string değerler ile karşılaştırılarak convert edilmiş 
    /// object döndüren methodların bulunduğu sınıf.
    /// </summary>
    public static class EnumConverter {

        /// <summary>
        /// String bir enum değeri alır (Çoğunlukla .config'lerde bulunan değerler) 
        /// alarak T tipinde convert edilmiş object döndürür
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static T ConvertStringToEnum<T>(string enumValue) where T : struct, IConvertible {
            object convertedEnum;

            if (string.IsNullOrEmpty(enumValue))
                return default;

            if (typeof(T).Equals(typeof(ExecutionEnvironment))) {
                convertedEnum = ArrangeTheEnvironment(enumValue);
            }
            else if (typeof(T).Equals(typeof(Platform))) {
                convertedEnum = ArrangeThePlantform(enumValue);
            }
            else if (typeof(T).Equals(typeof(ExternaPlatformType))) {
                convertedEnum = ArrangeExternaPlatformType(enumValue);
            }
            else {
                throw new Exception("Enum değeri eşlenemedi. Konfig değerlerinizi kontrol ediniz.");
            }

            return (T)convertedEnum;
        }
        private static object ArrangeTheEnvironment(string enumValue) {
            object convertedEnum;
            if (enumValue.Equals("LOCALHOST")) {
                convertedEnum = ExecutionEnvironment.LOCALHOST;
            }
            else if (enumValue.Equals("REMOTE")) {
                convertedEnum = ExecutionEnvironment.REMOTE;
            }
            else if (enumValue.Equals("TESTINIUM")) {
                convertedEnum = ExecutionEnvironment.TESTINIUM;
            }
            else {
                throw new ArgumentNullException(nameof(enumValue),
                    "Appconfig içerisinde girilen ExecutionEnvironment değerine "
                    + "uygun bir eşleşme yapılamadı. Config dosyasını kontrol ediniz.");
            }
            return convertedEnum;
        }

        private static object ArrangeThePlantform(string enumValue) {
            object convertedEnum;
            if (enumValue.Equals("WEBCHROME")) {
                convertedEnum = Platform.WEB_CHROME;
            }
            else if (enumValue.Equals("WEBINTERNETEXPLORER")) {
                convertedEnum = Platform.WEB_INTERNET_EXPLORER;
            }
            else if (enumValue.Equals("WEBFIREFOX")) {
                convertedEnum = Platform.WEB_FIREFOX;
            }
            else if (enumValue.Equals("WEBSAFARI")) {
                convertedEnum = Platform.WEB_SAFARI;
            }
            else if (enumValue.Equals("WINDOWSDESKTOPAPP")) {
                convertedEnum = Platform.WINDOWS_DESKTOP_APP;
            }
            else if (enumValue.Equals("WEBCHROMEHEADLESS")) {
                convertedEnum = Platform.WEB_CHROME_HEADLESS;
            }
            else if (enumValue.Equals("WEBEDGE")) {
                convertedEnum = Platform.WEB_EDGE;
            }
            else {
                throw new ArgumentNullException(nameof(enumValue),
                    "Appconfig içerisinde girilen Platforms değerine "
                    + "uygun bir eşleşme yapılamadı. Config dosyasını kontrol ediniz.");
            }
            return convertedEnum;
        }
        private static object ArrangeExternaPlatformType(string enumValue) {
            object convertedEnum;

            if (enumValue.Equals("") || enumValue.Equals("WINIUM")) {
                convertedEnum = ExternaPlatformType.WINIUM;
            }
            else if (enumValue.Equals("WINAPPDRIVER")) {
                convertedEnum = ExternaPlatformType.WINAPPDRIVER;
            }
            else {
                throw new ArgumentNullException(nameof(enumValue),
                    "Appconfig içerisinde girilen ExternaPlatformType değerine "
                    + "uygun bir eşleşme yapılamadı. Config dosyasını kontrol ediniz.");
            }

            return convertedEnum;
        }

        private static IList<T> ConvertListToEnum<T>(string[] enumValues) where T : struct, IConvertible {
            IList<T> convertedEnumList = new List<T>();
            foreach (string val in enumValues) {
                T brow = ConvertStringToEnum<T>(val);
                convertedEnumList.Add(brow);
            }
            return convertedEnumList;
        }

        public static IList<Platform> GetPlatformEnumsAsList(string platforms) {
            if (platforms == null)
                return null;

            string[] platformArr = platforms.Split(',');
            IList<Platform> platformTypeList = ConvertListToEnum<Platform>(platformArr);
            return platformTypeList;
        }

        public static TestEnvironment GetPlatformTestEnvironment(Platform platform) {
            object convertedEnum;

            if (TryCompareChromium(platform) ||
                platform.Equals(Platform.WEB_FIREFOX) ||
                platform.Equals(Platform.WEB_INTERNET_EXPLORER) ||
                platform.Equals(Platform.WEB_SAFARI)
                ) {
                convertedEnum = TestEnvironment.WEBAPP;
            }
            else if (platform.Equals(Platform.WINDOWS_DESKTOP_APP)) {
                convertedEnum = TestEnvironment.DESKTOPAPP;
            }
            else if (TryCompareAndroid(platform) || TryCompareIos(platform)) {
                convertedEnum = TestEnvironment.MOBILEAPP;
            }
            else {
                throw new
                    ArgumentNullException(nameof(platform),
                        "Girilen <string> değerine uygun bir enum eşleşmesi yapılamadı.");
            }

            return (TestEnvironment)convertedEnum;
        }

        private static bool TryCompareIos(Platform platform) {
            return platform.Equals(Platform.IOS_APP) ||
                platform.Equals(Platform.IOS_SAFARI);
        }

        private static bool TryCompareAndroid(Platform platform) {
            return platform.Equals(Platform.ANDROID_APP) ||
                platform.Equals(Platform.ANDROID_CHROME) ||
                platform.Equals(Platform.ANDROID_FIREFOX) ||
                platform.Equals(Platform.ANDROID_NATIVE_GOOGLE);
        }

        private static bool TryCompareChromium(Platform platform) {
            return (platform.Equals(Platform.WEB_CHROME) ||
                    platform.Equals(Platform.WEB_CHROME_HEADLESS) ||
                    platform.Equals(Platform.WEB_EDGE));
        }
    }
}
