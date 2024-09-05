using System;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Models.Enums;

namespace TestAutomation.Framework.DomainLayer.POMBase
{
    public abstract class Model : IDisposable
    {

        private IWebDriver webApp;
        private IDesktopDriver desktopApp;

        protected IWebDriver WebApp
        {
            get
            {
                return webApp =
                    webApp ??
                    (IWebDriver)DriverManager.GetDriver(Test, TestEnvironment.WEBAPP);
            }
        }

        protected IDesktopDriver DesktopApp
        {
            get
            {
                return desktopApp =
                    desktopApp ??
                    (IDesktopDriver)DriverManager.GetDriver(Test, TestEnvironment.DESKTOPAPP);
            }
        }

        public Test Test
        {
            get; set;
        }
        public virtual void Init() { }
        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
