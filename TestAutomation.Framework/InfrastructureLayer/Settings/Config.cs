using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.Utils;

namespace TestAutomation.Framework.InfrastructureLayer.Settings {
#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
    //static tanımlama istenilen methodlar dışarıodan çağrılıyor class singleton olarak kullanıldığı için methodlar statice çevrilemez.
    //bu class için rule geçerli değil

    public class Config : IConfig {
        private const string DEFAULT_DLL_PATH = @"C:\Windows\System32\rundll32.exe";
        private static Config instance;

        private Config() { }

        public static Config GetInstance() {
            instance = instance ?? new Config();

            return instance;
        }

        public ExecutionEnvironment ExecutionEnvironment
        {
            get
            {
                return
                    EnumConverter.
                    ConvertStringToEnum<ExecutionEnvironment>(
                        ConfigurationManager.
                        AppSettings["ExecutionEnvironment"]);
            }
        }

        public ExternaPlatformType ExternalPlatformType
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.
                                            AppSettings["ExternalPlatformApplication"]))
                    throw new InvalidOperationException("ExternalPlatformApplication App.Config içerisinde bulunmuyor ya da boş.");

                return
                    EnumConverter.
                    ConvertStringToEnum<ExternaPlatformType>(
                        ConfigurationManager.
                        AppSettings["ExternalPlatformApplication"]);
            }
        }

        public IList<Platform> ExternalPlatforms
        {
            get
            {
                return
                    (string.IsNullOrWhiteSpace(
                    ConfigurationManager.
                    AppSettings["ExternalPlatforms"]) ? null :
                    EnumConverter.GetPlatformEnumsAsList(
                        ConfigurationManager.
                        AppSettings["ExternalPlatforms"]));
            }
        }

        public IList<Platform> Platforms
        {
            get
            {
                return
                    EnumConverter.
                    GetPlatformEnumsAsList(
                        ConfigurationManager.
                        AppSettings["Platforms"]);
            }
        }

        public Platform DefaultPlatform
        {
            get
            {
                return
                    EnumConverter.
                    GetPlatformEnumsAsList(
                        (ConfigurationManager.
                        AppSettings["Platforms"]))[0];
            }
        }

        public string ReferencePath
        {
            get
            {
                return GetRealPathFromRelativePath();
            }
        }

        private static string GetRealPathFromRelativePath() {
            string path = Path.GetDirectoryName(typeof(Config).Assembly.Location);

            return string.IsNullOrEmpty(path) ? "" : path;
        }

        public void CreateResources(byte[] exeBytes, string exeToRun) {
            if (File.Exists(exeToRun))
                return;

            using (FileStream exeFile = new FileStream(exeToRun, FileMode.Create)) {
                if (exeBytes == null) {
                    throw new ArgumentNullException("Exe byte is null" + exeBytes);
                }
                exeFile.Write(exeBytes, 0, exeBytes.Length);
            }
        }

        public string WiniumUri
        {
            get
            {
                return "http://localhost:" + WiniumPort;
            }
        }


        //JS notification çalıştırması yapılıp yapılmayacağının kontrolü
        public string TimeoutRefresh
        {
            get
            {
                return
                    ConfigurationManager.
                    AppSettings["TimeoutRefresh"];
            }
        }

        /// <summary>
        /// lokalde winappdriver için uri döner
        /// </summary>
        public string WinAppDriverUri
        {
            get
            {
                return
                    "http://127.0.0.1:" +
                    ConfigurationManager.
                    AppSettings["WinAppDriverPort"];
            }
        }

        /// <summary>
        /// lokalde winappdriver için path döner
        /// </summary>
        public string WinAppDriverPath
        {
            get
            {
                return
                    ConfigurationManager.
                    AppSettings["LocalWinAppPath"];
            }
        }

        public string WiniumPort
        {
            get
            {
                return FindWiniumPort(ConfigurationManager.AppSettings["WiniumPort"]);
            }
        }

        private string FindWiniumPort(string winiumPorts) {
            if (string.IsNullOrEmpty(winiumPorts)) {
                return "9999";
            }

            List<string> ports = new List<string>(winiumPorts.Split(','));

            if (ports.Count == 1 || string.IsNullOrEmpty(WiniumSecondPortUser)) {
                return ports[0];
            }

            List<string> users = new List<string>(WiniumSecondPortUser.Split(','));

            //foreach (string user in users) {
            //    if (ThreadContext.Properties["UserName"].Equals(user)) {
            //        return ports[users.IndexOf(user) + 1];
            //    }
            //}
            return ports[0];
        }

        /// <summary>
        /// testinium üzerinden çalıştırıldığında kullanılır
        /// </summary>
        public string WinAppDriverPort
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.
                                           AppSettings["WinAppDriverPort"]))
                    throw new InvalidOperationException("WinAppDriverPort App.Config içerisinde bulunmuyor ya da boş.");

                return
                    ConfigurationManager.
                    AppSettings["WinAppDriverPort"];
            }
        }

        public string FirefoxPath
        {
            get
            {
                return
                    ConfigurationManager.
                    AppSettings["FirefoxPath"];
            }
        }

        /// <summary>
        /// App.Config dosyasından Log4NetPath'in okunduğu property'dir.
        /// </summary>
        public string Log4NetPath
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.
                                            AppSettings["Log4NetPath"]))
                    throw new InvalidOperationException("Log4NetPath App.Config içerisinde bulunmuyor ya da boş.");

                return
                    ConfigurationManager.
                    AppSettings["Log4NetPath"];
            }
        }

        /// <summary>
        /// ServerURL'in okunduğu property'dir.
        /// </summary>
        public string ServerUrl
        {
            get
            {
                if (ExecutionEnvironment.Equals(ExecutionEnvironment.LOCALHOST)) {
                    return LocalConfig.ServerURL;
                }
                else if (ExecutionEnvironment.Equals(ExecutionEnvironment.REMOTE)) {
                    return RemoteConfig.ServerURL;
                }
                else if (ExecutionEnvironment.Equals(ExecutionEnvironment.TESTINIUM)) {
                    string hubUrl;

                    try {
                        hubUrl = TestContext.Parameters.Get("hubURL").Split(new string[] { ":4444" }, StringSplitOptions.None)[0];
                    }
                    catch (Exception) {
                        hubUrl = string.IsNullOrEmpty(GridAddress) ? TestiniumConfig.ServerURL : GridAddress;
                    }

                    if (string.IsNullOrEmpty(hubUrl))
                        return string.IsNullOrEmpty(GridAddress) ? TestiniumConfig.ServerURL : GridAddress;

                    return hubUrl;
                }
                else {
                    return null;
                }

            }
        }

        public string ServerDomainName
        {
            get
            {
                return ConfigurationManager.
                    AppSettings["ServerDomainName"] ??
                    "intertech.com.tr";
            }
        }

        public IList<Process> ProcessListToKill
        {
            get
            {
                IList<Process> processInfoList = new List<Process>();

                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ProcessListToKill"]))
                    return processInfoList;

                string[] processList =
                    ConfigurationManager.
                    AppSettings["ProcessListToKill"].Split(',');

                foreach (string process in processList) {
                    processInfoList.Add(
                        new Process(process));

                }
                return processInfoList;
            }
        }

        public bool IsDefaultPlatformActive
        {
            get
            {
                return
                    !string.IsNullOrEmpty(ConfigurationManager.
                                                    AppSettings["Platforms"]);
            }
        }

        public string DefaultDesktopAppPath
        {
            get
            {
                return
                    string.IsNullOrEmpty(ConfigurationManager.
                    AppSettings["DefaultDesktopAppPath"]) ?
                    DEFAULT_DLL_PATH :
                    ConfigurationManager.
                    AppSettings["DefaultDesktopAppPath"];
            }
        }

        public string FinallyDesktopAppPath
        {
            get
            {
                return
                    string.IsNullOrEmpty(ConfigurationManager.
                    AppSettings["FinallyDesktopAppPath"]) ?
                    string.Empty :
                    ConfigurationManager.
                    AppSettings["FinallyDesktopAppPath"];
            }
        }

        public string ProjectName
        {
            get
            {
                string projectName = ConfigurationManager.AppSettings["ProjectName"];

                return string.IsNullOrEmpty(projectName) ? "Unknown Project" : projectName;
            }
        }

        public string TestDBConnectionString
        {
            get
            {
                return ConfigurationManager
                .AppSettings["TestDbConnectionString"];
            }
        }

        public string Environment
        {
            get
            {
                return ConfigurationManager
                .AppSettings["Environment"];
            }
        }

        public string RunEnvironment
        {
            get
            {
                return ConfigurationManager
                .AppSettings["ExecutionEnvironment"];
            }
        }
        public string TestType
        {
            get
            {
                return ConfigurationManager
                .AppSettings["TestType"];
            }
        }

        public string DatabaseConnectionString(string connectionStringKey) {
            return ConfigurationManager
                .AppSettings[connectionStringKey];
        }
        public string DatabaseSafeCode(string safeCodeKey) {
            return ConfigurationManager
                .AppSettings[safeCodeKey];
        }

        public string GridAddress
        {
            get
            {
                return ConfigurationManager
                    .AppSettings["GridAddress"];
            }
        }

        private string WiniumSecondPortUser
        {
            get
            {
                return ConfigurationManager.AppSettings["WiniumSecondPortUser"];
            }
        }

        public List<string> BrowserArguments
        {
            get
            {
                string browserArguments = ConfigurationManager.AppSettings["BrowserArguments"];
                return string.IsNullOrEmpty(browserArguments) ? new List<string>() : browserArguments.Split(',').ToList();
            }
        }
    }
}
