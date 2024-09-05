using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestAutomation.Framework.DomainLayer.Container;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Conditions;
using TestAutomation.Framework.InfrastructureLayer.Adapters.Elements;
using TestAutomation.Framework.InfrastructureLayer.Services;
using ISeleniumWebDriver = OpenQA.Selenium.IWebDriver;
using ISeleniumWebElement = OpenQA.Selenium.IWebElement;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.Drivers {
    internal abstract class WebDriver : IDriver, DomainLayer.Contracts.IWebDriver {
        protected ISeleniumWebDriver Driver;



        private Session session;

        /// <summary>
        /// returns web driver element
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="locator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public IElement GetElement(Type elementType, Locator locator, TimeSpan timeout) {
            try {
                return ElementService.GetElementProxy<WebElement>(this, locator, timeout);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// returns web driver element list
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="locator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public IElementListInternal GetElementList(Type elementType, Locator locator, TimeSpan timeout) {
            try {
                return ElementService.GetElementListProxy<WebElementList>(this, locator, timeout);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Elementler için kullanılan default timeout süresini belirler
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        /// URL'e gidilir
        /// </summary>
        /// <param name="url"></param>
        public void Navigate(string url) {
            try {
                Driver.Navigate().GoToUrl(url);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// returns session info
        /// </summary>
        public Session Session
        {
            get
            {
                if (session == null) {
                    Test test = ContainerService.GetParentByElement<DriverContainer>(this);
                    session = DriverManager.GetSession(test);
                }

                return session;
            }
        }

        /// <summary>
        /// forward broser adress
        /// </summary>
        public void NavigateToForward() {
            try {
                Driver.Navigate().Forward();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// back browser adress
        /// </summary>
        public void NavigateToBack() {
            try {
                Driver.Navigate().Back();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// close browser
        /// </summary>
        public void CloseWindow() {
            try {
                Driver.Close();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// switch from frame to another frame
        /// </summary>
        /// <param name="frameElement"></param>
        public void SwitchToFrame(DomainLayer.Contracts.IWebElement frameElement) {
            try {
                Driver.
                    SwitchTo().
                    Frame(
                    (ISeleniumWebElement)((IElementInternal)frameElement).
                    BindedElement);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// switch from frame to another frame
        /// </summary>
        /// <param name="index"></param>
        public void SwitchToFrame(int index) {
            try {
                Driver.SwitchTo().Frame(index);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// switch from frame to another frame
        /// </summary>
        /// <param name="frameName"></param>
        public void SwitchToFrame(string frameName) {
            try {
                Driver.SwitchTo().Frame(frameName);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// switch from frame to parent frame
        /// </summary>
        public void SwitchToParentFrame() {
            try {
                Driver.SwitchTo().ParentFrame();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// switch to active element
        /// </summary>
        public void SwitchToActiveElement() {
            try {
                Driver.SwitchTo().ActiveElement();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// switch to default
        /// </summary>
        public void SwitchToDefaultContent() {
            try {
                Driver.SwitchTo().DefaultContent();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// switch to another window
        /// </summary>
        /// <param name="windowName"></param>
        public void SwitchToWindow(string windowName) {
            try {
                Driver.SwitchTo().Window(windowName);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// switch to first window
        /// </summary>
        public void SwictToFirstWindow() {
            try {
                Driver.SwitchTo().Window(Driver.WindowHandles.FirstOrDefault());
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// swicth to last window
        /// </summary>
        public void SwitchToLastWindow() {
            try {
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Switches to default content first, then switches to the frame
        /// </summary>
        /// <param name="frameElement">The frame element.</param>
        public void ChangeFrame(DomainLayer.Contracts.IWebElement frameElement) {
            try {
                SwitchToDefaultContent();
                SwitchToFrame(frameElement);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Switches to default content first, then switches to the frame
        /// </summary>
        /// <param name="index">The index.</param>
        public void ChangeFrame(int index) {
            try {
                SwitchToDefaultContent();
                SwitchToFrame(index);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Mevcut URL'i string tipinde döner
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUrl() {
            try {
                return Driver.Url;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Mevcut browser title'ını string tipinde döner
        /// </summary>
        /// <returns></returns>
        public string GetTitle() {
            try {
                return Driver.Title;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Sayfa kaynağını string tipinde döner
        /// </summary>
        /// <returns></returns>
        public string GetPageSource() {
            try {
                return Driver.PageSource;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Driver üzerindeki benzersiz şekilde tanımlanan pencereye opak bir tanıtıcı olan geçerli pencere tanıtıcısını alır.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentWindowHandle() {
            try {
                return Driver.CurrentWindowHandle;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Driver tarafından açılmış olan pencerelerin tamamını handle ederek collection tipinde döner
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<string> GetWindowHandles() {
            try {
                return Driver.WindowHandles;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Determines whether [contains window handles] [the specified expected handle count].
        /// </summary>
        /// <param name="expectedHandleCount">The expected handle count.</param>
        /// <returns>
        ///   <c>true</c> if [contains window handles] [the specified expected handle count]; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsWindowHandles(int expectedHandleCount) {
            try {
                return ContainsWindowHandles(expectedHandleCount, Timeout);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Determines whether [contains window handles] [the specified expected handle count].
        /// </summary>
        /// <param name="expectedHandleCount">The expected handle count.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        ///   <c>true</c> if [contains window handles] [the specified expected handle count]; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsWindowHandles(int expectedHandleCount, TimeSpan timeout) {
            try {
                WaitService.Wait(new ConditionOfWindowHandleToBeEqual((IAgent)this, expectedHandleCount), timeout);
                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Browserı maximize eder
        /// </summary>
        public void Maximize() {
            try {
                Driver.Manage().Window.Maximize();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Browserın boyutunu ayarlamak için kullanılan method. Size bilgisi verilerek boyutu ayarlanır.
        /// </summary>
        /// <param name="size"></param>
        public void ChangeWindowSize(Size size) {
            try {
                Driver.Manage().Window.Size = size;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Pencerenin pozisyonunu belirleyen method. Point bilgisi ile pencerenin yeri değiştirilir.
        /// </summary>
        /// <param name="point"></param>
        public void ChangeWindowPosition(Point point) {
            try {
                Driver.Manage().Window.Position = point;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Sayfayı yeniler
        /// </summary>
        public void Refresh() {
            try {
                Driver.Navigate().Refresh();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Belirtilen  name="timeout" süresini sayfa yüklenme süresini setler.
        /// </summary>
        /// <param name="timeout"></param>
        public void SetPageLoadTimeout(TimeSpan timeout) {
            try {
                Driver.Manage().Timeouts().PageLoad = timeout;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }


        /// <summary>
        /// Javascript asenkron timeout'u belirler
        /// </summary>
        /// <param name="timeout"></param>
        public void SetScriptTimeout(TimeSpan timeout) {
            try {
                Driver.Manage().Timeouts().AsynchronousJavaScript = timeout;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }


        /// <summary>
        /// Belirtilen timeout süresi kadar bekler
        /// </summary>
        public void ForceWait(TimeSpan timeout) {
            Thread.Sleep(timeout);
        }

        /// <summary>
        /// WebElement in text metnini içerip içermediğini bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool ContainsText(DomainLayer.Contracts.IWebElement element, String text) {
            try {
                return ContainsText(element, text, Timeout);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Belirtilen timeout süresinde WebElement in text metnini içerip içermediğini bool tipinde olarak döner
        /// </summary>
        /// <param name="element">IWebElement</param>
        /// <param name="text">text</param>
        /// <param name="timeout">miliseconds</param>
        /// <returns></returns>
        public bool ContainsText(DomainLayer.Contracts.IWebElement element, String text, TimeSpan timeout) {
            try {
                WaitService.Wait(new ConditionOfTextToBePresentInElement((IAgent)this, element, text), timeout);
                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Verilen Webelement'in kaybolup kaybolmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsElementVanished(DomainLayer.Contracts.IWebElement element) {
            try {
                return IsElementVanished(element, Timeout);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }


        /// <summary>
        /// Belirtilen timeout süresi boyunca Verilen Webelement'in kaybolup kaybolmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool IsElementVanished(DomainLayer.Contracts.IWebElement element, TimeSpan timeout) {
            try {
                WaitService.Wait(new ConditionOfInvisibilityOfElementLocated((IAgent)this, element), timeout);
                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Verilen Webelement'in clickable olup olmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsElementClickable(DomainLayer.Contracts.IWebElement element) {
            try {
                return IsElementClickable(element, Timeout);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Belirtilen timeout süresi içinde parametre verilen elementin clickable olup olmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool IsElementClickable(DomainLayer.Contracts.IWebElement element, TimeSpan timeout) {
            try {
                WaitService.Wait(new ConditionOfElementToBeClickable((IAgent)this, element), timeout);
                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Verilen WebElement'in görünür olup olmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsElementVisible(DomainLayer.Contracts.IWebElement element) {
            try {
                return IsElementVisible(element, Timeout);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Belirtilen timeout süresinde Verilen Webelement'in ekranda görünür olup olmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool IsElementVisible(DomainLayer.Contracts.IWebElement element, TimeSpan timeout) {
            try {
                WaitService.Wait(new ConditionOfElementIsVisible((IAgent)this, element), timeout);

                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Javasccript Popup uyarısının bulunup bulunmadığını bool tipinde döner.
        /// </summary>
        /// <returns></returns>
        public bool IsAlertPresent() {
            try {
                Driver.SwitchTo().Alert();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        /// <summary>
        /// Default timeout süresi içinde parametre verilen frame'e switch etmeyi dener. Edilebiliyor ise switch eder.
        /// </summary>
        /// <param name="frameElement"></param>
        /// <returns>Switch yapıldı ise true, timeout alındı ise false</returns>
        public bool TrySwitchToFrameWhenAvailable(DomainLayer.Contracts.IWebElement frameElement) {
            try {
                WaitService.Wait(new ConditionOfFrameToBeSwitchableAndSwitchToIt((IAgent)this, frameElement));
                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Belirtilen timeout süresi içinde parametre verilen frame'e switch etmeyi dener. Edilebiliyor ise switch eder.
        /// </summary>
        /// <param name="frameElement"></param>
        /// <param name="timeout"></param>
        /// <returns>Switch yapıldı ise true, timeout alındı ise false</returns>
        public bool TrySwitchToFrameWhenAvailable(DomainLayer.Contracts.IWebElement frameElement, TimeSpan timeout) {
            try {
                WaitService.Wait(new ConditionOfFrameToBeSwitchableAndSwitchToIt((IAgent)this, frameElement), timeout);
                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Javasccript Popup uyarısını onaylar.
        /// </summary>
        public void AcceptAlert() {
            try {
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Javasccript Popup uyarısını kapatır
        /// </summary>
        public void DismissAlert() {
            try {
                Driver.SwitchTo().Alert().Dismiss();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Javasccript Popup uyarısındaki text mesajını string tipinde döner.
        /// </summary>
        /// <returns></returns>
        public string GetAlertText() {
            try {
                return Driver.SwitchTo().Alert().Text;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Javasccript Popup uyarısındaki text mesajını timeout süresince visible olduğunda string tipinde döner.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string GetAlertText(TimeSpan timeout) {
            try {
                WaitService.Wait(new ConditionOfAlertIsVisible((IAgent)this, timeout));
                return Driver.SwitchTo().Alert().Text;
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Javasccript Popup uyarısına text gönderilmesini sağlar
        /// </summary>
        /// <param name="keysToSend"></param>
        public void SendKeysToAlert(string keysToSend) {
            try {
                Driver.SwitchTo().Alert().SendKeys(keysToSend);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Kimlik bilgilerini isteyen bir uyarıda kullanıcı adı ve parolasını ayarlar.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void SetAuthenticationCredentials(string userName, string password) {
            try {
                Driver.SwitchTo().Alert().SetAuthenticationCredentials(userName, password);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// String olarak verilen scripti execute eder.
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public object ExecuteJS(string script) {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            try {
                return js.ExecuteScript(script);
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// String olarak verilen scripti parametrelerle birlikte execute eder.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object ExecuteJS(string script, params object[] parameter) {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            try {
                return js.ExecuteScript(script, parameter);
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// String olarak verilen scripti parametrelerle birlikte execute eder.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public object ExecuteJS(string script, DomainLayer.Contracts.IWebElement element) {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            try {
                return js.ExecuteScript(script, element);
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Javascript document.readyState complete olana kadar bekler
        /// </summary>
        /// <returns></returns>
        public bool IsPageLoaded() {
            try {
                WaitService.Wait(new ConditionOfPageLoaded((IAgent)this));
                return true;
            }
            catch (Exception) {
                //logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Belirtilen element'e kadar sayfa içerisinde "Scroll" aksiyonu alır.
        /// </summary>
        /// <param name="element"></param>
        public void ScrollToElement(DomainLayer.Contracts.IWebElement element) {
            try {
                Actions action = new Actions(Driver);
                action.MoveToElement((ISeleniumWebElement)((IElementInternal)element).
                    BindedElement, element.GetLocation().X, element.GetLocation().Y).Perform();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Sayfanın en başına gidilir.
        /// </summary>
        public void TopOfPage() {
            try {
                ExecuteJS("window.scrollTo(document.body.scrollHeight,0)");
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Sayfa sonuna gidilir.
        /// </summary>
        public void BottomOfPage() {
            try {
                ExecuteJS("window.scrollTo(0, document.body.scrollHeight)");
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Elemente text gönderir.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public void SendKeys(DomainLayer.Contracts.IWebElement element, string text) {
            try {
                Actions action = new Actions(Driver);
                action.SendKeys(
                    (ISeleniumWebElement)((IElementInternal)element)
                    .BindedElement, text).
                    Build().
                    Perform();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Elemente çift tıklar.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        public void DoubleClick(DomainLayer.Contracts.IWebElement element) {
            try {
                Actions action = new Actions(Driver);
                action.DoubleClick(
                    (ISeleniumWebElement)((IElementInternal)element).
                    BindedElement).
                    Build().
                    Perform();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Elemente  tıklar.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        public void Click(DomainLayer.Contracts.IWebElement element) {
            try {
                Actions action = new Actions(Driver);
                action.Click(
                    (ISeleniumWebElement)((IElementInternal)element).
                    BindedElement).
                    Build().
                    Perform();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Elemente sağ tıklar.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        public void RightClick(DomainLayer.Contracts.IWebElement element) {
            try {
                Actions action = new Actions(Driver);
                action.ContextClick(
                    (ISeleniumWebElement)((IElementInternal)element).
                    BindedElement).
                    Build().
                    Perform();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Mouse elementin üstüne getirilir.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        public void MoveElement(DomainLayer.Contracts.IWebElement element) {
            try {
                Actions action = new Actions(Driver);
                action.MoveToElement(
                    (ISeleniumWebElement)((IElementInternal)element).
                    BindedElement).
                    Build().
                    Perform();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Başlangıç olarak verilen elementin konumundan başlar basılı tutup hedef elementin olduğu yere mouse bırakılır.
        /// </summary>
        /// <param name="onElement"></param>
        /// <param name="toElement"></param>
        public void DragAndDrop(
            DomainLayer.Contracts.IWebElement onElement,
            DomainLayer.Contracts.IWebElement toElement) {
            try {
                Actions action = new Actions(Driver);
                action.DragAndDrop(
                        (
                        ISeleniumWebElement)((IElementInternal)onElement).
                        BindedElement,
                        (ISeleniumWebElement)((IElementInternal)toElement).
                        BindedElement)
                      .Build()
                      .Perform();
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Üst üste açılmış pencerelerede en üsste bulunan aktif pencerenin handle bilgisini string tipinde döner
        /// </summary>
        /// <returns></returns>
        public string GetActiveWindowHandle() {
            string activeWindowHandle = null;
            try {
                string existingWindowHandle = GetCurrentWindowHandle();
                ReadOnlyCollection<string> windowHandles = GetWindowHandles();

                int counter = 0;
                if ((windowHandles.Count < 2) && (counter < 5)) {
                    ForceWait(TimeSpan.FromSeconds(2));
                    return GetActiveWindowHandle();
                }

                foreach (string handle in windowHandles) {
                    if (handle != existingWindowHandle) {
                        activeWindowHandle = handle;
                        break;
                    }
                }

            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }

            return activeWindowHandle;
        }

        /// <summary>
        /// Mevcutta ayakta olan process kill edilir.
        /// </summary>
        /// <param name="processName"></param>
        public void KillProcess(string processName) {
            try {
                ProcessManager.KillProcess(processName);
            }
            catch (Exception) {
                //logger.Error(ex);
                throw;
            }
        }
    }
}