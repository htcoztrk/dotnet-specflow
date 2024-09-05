using System.Data.SqlClient;

namespace TestAutomation.Framework.Core.ServiceContracts {
    public interface IDBService {
        SqlConnection GetDataBaseConnection();
    }
}
