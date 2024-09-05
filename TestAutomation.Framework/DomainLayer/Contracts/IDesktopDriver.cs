using System;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    public interface IDesktopDriver {
        /// <summary>
        /// Elementin tetiklenmesi için gerekli süreyi belirler
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// DesktopElement in text metnini içerip içermediğini bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        bool ContainsText(IDesktopElement element, string text);

        /// <summary>
        /// Belirtilen timeout süresinde DesktopElement in text metnini içerip içermediğini bool tipinde olarak döner
        /// </summary>
        /// <param name="element">IDesktopElement</param>
        /// <param name="text">text</param>
        /// <param name="timeout">miliseconds</param>
        /// <returns></returns>
        bool ContainsText(IDesktopElement element, string text, TimeSpan timeout);

        /// <summary>
        /// Elemente çift tıklar.
        /// Actions kullanır.
        /// </summary>
        /// <param name="element"></param>
        void DoubleClick(IDesktopElement element);

        /// <summary>
        /// Belirtilen timeout süresi kadar bekler
        /// </summary>
        /// <param name="timeToWait">miliseconds</param>
        void ForceWait(TimeSpan timeToWait);

        /// <summary>
        /// check if element clickable
        /// </summary>
        /// <param name="element"></param>
        /// <returns>True if element becomes clickable in default timespan, false on timeout</returns>
        bool IsElementClickable(IDesktopElement element);

        /// <summary>
        /// check if element clickable
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns>True if element becomes clickable in the given timespan, false on timeout</returns>
        bool IsElementClickable(IDesktopElement element, TimeSpan timeout);

        /// <summary>
        /// Verilen DesktopElement'in görünür olup olmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool IsElementVisible(IDesktopElement element);

        /// <summary>
        /// Belirtilen timeout süresinde Verilen DesktopElement'in kaybolup kaybolmadığını bool tipinde döner
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        bool IsElementVisible(IDesktopElement element, TimeSpan timeout);
    }
}