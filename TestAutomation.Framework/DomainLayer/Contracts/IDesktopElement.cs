using TestAutomation.Framework.DomainLayer.Models.Attributes;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    [ElementType]
    public interface IDesktopElement : IElement {
        /// <summary>
        /// Elementin altında diğer elementi bulur ve döner.
        /// </summary>
        /// <returns></returns>
        IDesktopElement GetChild(IDesktopElement element);

        /// <summary>
        /// Elementin altında diğer elementleri bulur ve liste şeklinde döner.
        /// </summary>
        /// <returns></returns>
        IDesktopElementList GetChildList(IDesktopElement element);

        /// <summary>
        /// Elementi tekrar bağlanmaya zorlar
        /// </summary>
        void BindElementForcefully();

        /// <summary>
        /// Elementin resmini cekip, byte dizisi olarak dondurur
        /// </summary>
        byte[] GetElementScreenshot();
    }
}
