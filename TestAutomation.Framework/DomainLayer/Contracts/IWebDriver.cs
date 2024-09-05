using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    public interface IWebDriver {
        /// <summary>
        /// Elementin tetiklenmesi için gerekli süreyi belirler
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Driver'ın odaklandığı browser penceresini kapatır
        /// </summary>
        void CloseWindow();

        /// <summary>
        /// WebElement in text metnini içerip içermediğini bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        bool ContainsText(IWebElement element, string text);

        /// <summary>
        /// Belirtilen timeout süresinde WebElement in text metnini içerip içermediğini bool tipinde olarak döner
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <param name="text">text</param>
        /// <param name="timeout">miliseconds</param>
        /// <returns></returns>
        bool ContainsText(IWebElement element, string text, TimeSpan timeout);

        /// <summary>
        /// Determines whether [contains window handles] [the specified expected handle count].
        /// </summary>
        /// <param name="expectedHandleCount">The expected handle count.</param>
        /// <returns>
        ///   <c>true</c> if [contains window handles] [the specified expected handle count]; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsWindowHandles(int expectedHandleCount);

        /// <summary>
        /// Determines whether [contains window handles] [the specified expected handle count].
        /// </summary>
        /// <param name="expectedHandleCount">The expected handle count.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        ///   <c>true</c> if [contains window handles] [the specified expected handle count]; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsWindowHandles(int expectedHandleCount, TimeSpan timeout);

        /// <summary>
        /// Belirtilen timeout süresi kadar bekler
        /// </summary>
        /// <param name="timeout">miliseconds</param>
        void ForceWait(TimeSpan timeout);

        /// <summary>
        /// Mevcut URL'i string tipinde döner
        /// </summary>
        /// <returns></returns>
        string GetCurrentUrl();

        /// <summary>
        /// Driver üzerindeki benzersiz şekilde tanımlanan pencereye opak bir tanıtıcı olan geçerli pencere tanıtıcısını alır.
        /// </summary>
        /// <returns></returns>
        string GetCurrentWindowHandle();

        /// <summary>
        /// Driver tarafından açılmış olan pencerelerin tamamını handle ederek collection tipinde döner
        /// </summary>
        /// <returns></returns>
        ReadOnlyCollection<string> GetWindowHandles();

        /// <summary>
        /// Sayfa kaynağını string tipinde döner
        /// </summary>
        /// <returns></returns>
        string GetPageSource();

        /// <summary>
        /// Mevcut browser title'ını string tipinde döner
        /// </summary>
        /// <returns></returns>
        string GetTitle();

        /// <summary>
        /// Verilen Webelement'in clickable olup olmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool IsElementClickable(IWebElement element);

        /// <summary>
        /// Belirtilen timeout süresi içinde parametre verilen elementin clickable olup olmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        bool IsElementClickable(IWebElement element, TimeSpan timeout);

        /// <summary>
        /// Verilen Webelement'in kaybolup kaybolmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool IsElementVanished(IWebElement element);
        /// <summary>
        /// Belirtilen timeout süresi boyunca Verilen Webelement'in kaybolup kaybolmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        bool IsElementVanished(IWebElement element, TimeSpan timeout);

        /// <summary>
        /// Verilen WebElement'in görünür olup olmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool IsElementVisible(IWebElement element);

        /// <summary>
        /// Belirtilen timeout süresinde Verilen Webelement'in ekranda görünür olup olmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        bool IsElementVisible(IWebElement element, TimeSpan timeout);

        /// <summary>
        /// Browserı maximize eder
        /// </summary>
        void Maximize();

        /// <summary>
        /// Browser üzerinde <paramref name="url"/>'i açılmasını tetikler
        /// </summary>
        /// <param name="url">asdasd</param>
        void Navigate(string url);

        /// <summary>
        /// Bir önceki browser sayfasına geçiş yapar
        /// </summary>
        void NavigateToBack();

        /// <summary>
        /// Bir sonraki browser sayfasına geçiş yapar
        /// </summary>
        void NavigateToForward();

        /// <summary>
        /// Browserı yeniden yükler.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Belirtilen  name="timeout" süresini sayfa yüklenme süresini setler.
        /// </summary>
        /// <param name="timeout"></param>
        void SetPageLoadTimeout(TimeSpan timeout);

        /// <summary>
        /// Belirtilen  name="timeout" süresince JavaScritp'in yüklenmesini bekler
        /// </summary>
        /// <param name="timeout"></param>
        void SetScriptTimeout(TimeSpan timeout);

        /// <summary>
        /// İlk pencereye geçiş yapılmasını sağlar.
        /// </summary>
        void SwictToFirstWindow();

        /// <summary>
        /// Aktif elemente geçiş yapılmasını sağlar
        /// </summary>
        void SwitchToActiveElement();

        /// <summary>
        /// Default frame'e geçiş yapılmasını sağlar
        /// </summary>
        void SwitchToDefaultContent();

        /// <summary>
        /// string olarak belirtilen frame'e geçiş yapılmasını sağlar
        /// </summary>
        /// <param name="frameName"></param>
        void SwitchToFrame(string frameName);

        /// <summary>
        /// Çoklu pencerelerde belirtilen index frameine geçiş yapılmasını sağlar
        /// </summary>
        /// <param name="index"></param>
        void SwitchToFrame(int index);

        /// <summary>
        /// WebElement'i verilen frame'e geçiş yapılmasını sağlar
        /// </summary>
        /// <param name="frameElement"></param>
        void SwitchToFrame(IWebElement frameElement);

        /// <summary>
        /// Son pencereye geçiş yapılmasını sağlar
        /// </summary>
        void SwitchToLastWindow();

        /// <summary>
        /// Aktif frame'in parentına geçiş yapılmasını sağlar
        /// </summary>
        void SwitchToParentFrame();

        /// <summary>
        /// Pencere adı verilmiş frame'e geçiş yapılmasını sağlar
        /// </summary>
        /// <param name="windowName"></param>
        void SwitchToWindow(string windowName);

        /// <summary>
        /// Javasccript Popup uyarısının bulunup bulunmadığını bool tipinde döner.
        /// </summary>
        /// <returns></returns>
        bool IsAlertPresent();

        /// <summary>
        /// Default timeout süresi içinde parametre verilen frame'e switch etmeyi dener. Edilebiliyor ise switch eder.
        /// </summary>
        /// <param name="frameElement"></param>
        /// <returns>Switch yapıldı ise true, timeout alındı ise false</returns>
        bool TrySwitchToFrameWhenAvailable(IWebElement frameElement);

        /// <summary>
        /// Belirtilen timeout süresi içinde parametre verilen frame'e switch etmeyi dener. Edilebiliyor ise switch eder.
        /// </summary>
        /// <param name="frameElement"></param>
        /// <param name="timeout"></param>
        /// <returns>Switch yapıldı ise true, timeout alındı ise false</returns>
        bool TrySwitchToFrameWhenAvailable(IWebElement frameElement, TimeSpan timeout);

        /// <summary>
        /// Javascript document.readyState complete olana kadar bekler
        /// </summary>
        /// <returns></returns>
        bool IsPageLoaded();

        /// <summary>
        /// Javasccript Popup uyarısını onaylar.
        /// </summary>
        /// <returns></returns>
        void AcceptAlert();

        /// <summary>
        /// Javasccript Popup uyarısını kapatır
        /// </summary>
        void DismissAlert();

        /// <summary>
        /// Javasccript Popup uyarısındaki text mesajını timeout süresince visible olduğunda string tipinde döner.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        string GetAlertText(TimeSpan timeout);

        /// <summary>
        /// Javasccript Popup uyarısındaki text mesajını string tipinde döner.
        /// </summary>
        /// <returns></returns>
        string GetAlertText();


        /// <summary>
        /// Javasccript Popup uyarısına text gönderilmesini sağlar
        /// </summary>
        /// <param name="keysToSend"></param>
        void SendKeysToAlert(string keysToSend);

        /// <summary>
        /// Kimlik bilgilerini isteyen bir uyarıda kullanıcı adı ve parolasını ayarlar.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        void SetAuthenticationCredentials(string userName, string password);

        /// <summary>
        /// String olarak verilen scripti execute eder.
        /// </summary>
        /// <param name="script"></param>
        object ExecuteJS(string script);

        /// <summary>
        /// String olarak verilen scripti parametrelerle birlikte execute eder.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        object ExecuteJS(string script, params object[] parameter);


        /// <summary>
        /// String olarak verilen scripti parametrelerle birlikte execute eder.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        object ExecuteJS(string script, DomainLayer.Contracts.IWebElement element);

        /// <summary>
        /// Belirtilen element'e kadar sayfa içerisinde "Scroll" aksiyonu alır.
        /// </summary>
        /// <param name="element"></param>
        void ScrollToElement(IWebElement element);

        /// <summary>
        /// Sayfanın en başına gidilir.
        /// </summary>
        void TopOfPage();

        /// <summary>
        /// Sayfa sonuna gidilir.
        /// </summary>
        void BottomOfPage();

        /// <summary>
        /// Elemente text gönderir.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        void SendKeys(IWebElement element, string text);

        /// <summary>
        /// Elemente çift tıklar.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        void DoubleClick(IWebElement element);

        /// <summary>
        /// Elemente  tıklar.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        void Click(IWebElement element);

        /// <summary>
        /// Elemente sağ tıklar.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        void RightClick(IWebElement element);

        /// <summary>
        /// Mouse elementin üstüne getirilir.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        void MoveElement(IWebElement element);

        /// <summary>
        /// Başlangıç olarak verilen elementin konumundan başlar basılı tutup hedef elementin olduğu yere mouse bırakılır.
        /// </summary>
        /// <param name="onElement"></param>
        /// <param name="toElement"></param>
        void DragAndDrop(IWebElement onElement, IWebElement toElement);

        /// <summary>
        /// Üst üste açılmış pencerelerede en üsste bulunan aktif pencerenin handle bilgisini string tipinde döner
        /// </summary>
        /// <returns></returns>
        string GetActiveWindowHandle();

        /// <summary>
        /// Browserın boyutunu ayarlamak için kullanılan method. Size bilgisi verilerek boyutu ayarlanır.
        /// </summary>
        /// <param name="size"></param>
        void ChangeWindowSize(Size size);

        /// <summary>
        /// Pencerenin pozisyonunu belirleyen method. Point bilgisi ile pencerenin yeri değiştirilir.
        /// </summary>
        /// <param name="point"></param>
        void ChangeWindowPosition(Point point);

        /// <summary>
        /// Mevcutta ayakta olan process kill edilir.
        /// </summary>
        /// <param name="processName"></param>
        void KillProcess(string processName);
    }
}