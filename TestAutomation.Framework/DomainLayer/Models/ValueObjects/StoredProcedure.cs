using System;

namespace TestAutomation.Framework.DomainLayer.Models.ValueObjects
{
    public class StoredProcedure
    {
        private string procedureName;

        /// <summary>
        /// StoredProcedure sınıfının prop'u. Null değer setlenmesi durumunda 
        /// ArgumentNullException hatası alır.
        /// </summary>
        public string ProcedureName
        {
            get
            {
                if (procedureName == null)
                    throw new ArgumentNullException(procedureName, 
                                        "StoredProcedure sınıfında ProcedureName property null geldi");

                return procedureName;
            }

            private set
            {
                procedureName = value;
            }
        }


        /// <summary>
        /// StoredProcedure sınıfının constructor methodu
        /// </summary>
        /// <param name="procedureName"></param>
        public StoredProcedure(string procedureName)
        {
            ProcedureName = procedureName;
        }
    }
}
