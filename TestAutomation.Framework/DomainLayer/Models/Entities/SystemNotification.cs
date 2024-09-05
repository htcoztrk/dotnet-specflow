using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.POMBase;
using System.Timers;
using IWebDriver = TestAutomation.Framework.DomainLayer.Contracts.IWebDriver;

namespace TestAutomation.Framework.DomainLayer.Models.Entities
{
    public class SystemNotification
    {
        /// <summary>
        /// Timer'ın private set'li property'si
        /// </summary>
        public Timer Timer { get; private set; }
        private readonly IWebDriver driver;

        /// <summary>
        /// SystemNotification constructor methodu.
        /// </summary>
        /// <param name="driver"></param>
        public SystemNotification(IWebDriver driver)
        {
            this.driver = driver;
        }
        /// <summary>
        /// Aldığı message object'i ile birlikte JS Execute eder.
        /// </summary>
        /// <param name="message">Execute edilecek message object'i alır.</param>
        /// <returns></returns>
        public bool TryNotifyForAction(Message message)
        {
            try
            {
                if(message != null)
                {
                    driver.ExecuteJS("'" + message.Text + "'");
                }              
                return true;
            }
            catch
            {
                return false;

                //TODO Javascript Executor browser dialog penceresi
                // açıldığında hata alıyor ve catch'e düşüyor.
                // Problem Selenium tarafında olduğundan
                // burada execute edilen mesajın raporlarda izlenmesi
                // ve buraya gerekli loglamanın yapılması gerekiyor
            }
        }
        /// <summary>
        /// Verilen interval değeri ile birlikte timerı başlatır.
        /// </summary>
        /// <param name="interval">Interval object'i alır. Interval object 
        /// oluşturulurken Interval length verilmiş olmalıdır.</param>
        /// <returns></returns>
        public void StartPeriodicNotification(Interval interval)
        {
            if(interval == null)
            {
                return;
            }
            Timer = new Timer
            {
                Interval = interval.Length
            };
            Timer.Elapsed += SendWakeUpCall;
            Timer.Enabled = true;
        }
        /// <summary>
        /// StartPeriodicNotification ile başlatılan timer'ı durdurur.
        /// </summary>
        public void StopPeriodicNotification()
        {
            Timer.Enabled = false;
            Timer.Stop();
        }
        private void SendWakeUpCall(object source, ElapsedEventArgs e)
        {
            const string message = "'Wake-up call as a reminder the test is still up and running.'";
            driver.ExecuteJS(message);
        }
    }
}
