using System;

namespace TestAutomation.Framework.DomainLayer.Models.Attributes {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class FindsBy : Attribute {
        private readonly string locator;

        public string Locator
        {
            get
            {
                return locator;
            }
        }

        /// <summary>
        /// Element'i Dom üzerinden ilişkilendirmek için zaman aşımı değeri (saniye cinsinden)
        /// </summary>
        public int Timeout { get; set; } = 10;

        public FindsBy(string locatorString) {
            locator = locatorString;
        }
    }
}
