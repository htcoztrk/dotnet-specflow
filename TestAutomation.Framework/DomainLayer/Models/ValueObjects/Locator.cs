using System;

namespace TestAutomation.Framework.DomainLayer.Models.ValueObjects
{
    public class Locator
    {
        private string by;

        public string By
        {
            get
            {
                if (by == null)
                {
                    throw new ArgumentException("Locator By değeri null olamaz.");
                }

                return by;
            }

            private set
            {
                by = value;
            }
        }

        public Locator(string by)
        {
            By = by;
        }
    }
}
