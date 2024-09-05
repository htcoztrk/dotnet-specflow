using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    public interface IConfig {
        /// <summary>
        /// Çalışılan ortam bilgisini döndürür.
        /// </summary>
        ExecutionEnvironment ExecutionEnvironment { get; }

        /// <summary>
        /// Environment bilgisini döndürür.
        /// </summary>
        string Environment { get; }

        /// <summary>
        /// TestType bilgisini döndürür.
        /// </summary>
        string TestType { get; }

        /// <summary>
        /// Kütüphane referanslarının yolunu döndürür.
        /// </summary>
        string ReferencePath { get; }

        /// <summary>
        /// Winium URI bilgisini döndürür.
        /// </summary>
        string WiniumUri { get; }

        /// <summary>
        /// GeckoDriver yolunu döndürür.
        /// </summary>
        string FirefoxPath { get; }

        /// <summary>
        /// Log4Net Config dosyasının yolunu döndürür.
        /// </summary>
        string Log4NetPath { get; }

        /// <summary>
        /// Hub'ın URL bilgisini döndürür.
        /// </summary>
        string ServerUrl { get; }

        /// <summary>
        /// Server'ın hangi domainde bulunduğunu döndürür.
        /// </summary>
        string ServerDomainName { get; }

        /// <summary>
        /// Koşum sonunda öldürülmesi gereken işlemleri döndürür.
        /// </summary>
        IList<Process> ProcessListToKill { get; }

        /// <summary>
        /// Config dosyasından ek platform listesini döndürür.
        /// </summary>
        IList<Platform> ExternalPlatforms { get; }

        /// <summary>
        /// Config dosyasından platform listesini döndürür.
        /// </summary>
        IList<Platform> Platforms { get; }


        /// <summary>
        /// BDD için geçerli platformu döndürür.
        /// </summary>
        Platform DefaultPlatform { get; }

        /// <summary>
        /// Winium çalıştığında birlikte ayağa kalkacağı uygulama yolunu döndürür.
        /// </summary>
        string DefaultDesktopAppPath { get; }

        /// <summary>
        /// Test sonlandıgında bir uygulama calıstırılmak istenirse bu pathe bilgi girişi yapılır
        /// </summary>
        string FinallyDesktopAppPath { get; }

        /// <summary>
        /// Winium Port bilgisini döndürür.
        /// </summary>
        string WiniumPort { get; }

        /// <summary>
        /// Dll hangi projeden kullanılıyorsa o projenin proje adını döndürür.
        /// </summary>
        string ProjectName { get; }

        /// <summary>
        /// WinAppDriver Port bilgisini döndürür.
        /// </summary>
        string WinAppDriverPort { get; }

        /// <summary>
        /// WinAppDriver URI bilgisini döndürür.
        /// </summary>
        string WinAppDriverUri { get; }

        /// <summary>
        /// Windows form ve popup bileşenleri için kullanılacak uygulamayı belirtir. 
        /// </summary>
        ExternaPlatformType ExternalPlatformType { get; }

        /// <summary>
        /// Grid URI bilgisini döndürür.
        /// </summary>
        string GridAddress { get; }
    }
}