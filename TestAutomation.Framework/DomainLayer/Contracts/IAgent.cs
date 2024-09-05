namespace TestAutomation.Framework.DomainLayer.Contracts {
    public interface IAgent {

        /// <summary>
        /// Driver öldürülür.
        /// </summary>
        void Quit();

        /// <summary>
        /// Driverı return eder.
        /// </summary>
        /// <returns></returns>
        object GetDriver();

        /// <summary>
        /// Remote makinada açılan drivera ait SessionId bilgisini geri döner, kullanılacağı zaman yeni değere set eder. 
        /// </summary>
        string SessionId { get; }
    }
}