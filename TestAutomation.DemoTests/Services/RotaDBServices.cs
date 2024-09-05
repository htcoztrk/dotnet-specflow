using System.Data.SqlClient;
using TestAutomation.Framework.Core.ServiceContracts;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Tests.Services
{
    public class RotaDBServices : IDBService
    {
        private const string SAFE_CODE_KEY = "Deneme";

        public string SafeCode
        {
            get
            {
                return Config.
                   GetInstance().
                   DatabaseSafeCode(SAFE_CODE_KEY);
            }
        }

        public SqlConnection GetDataBaseConnection()
        {
            return null;// SqlInterSafeService.Instance.GetDatabaseConnection(SafeCode);
        }
    }
}
