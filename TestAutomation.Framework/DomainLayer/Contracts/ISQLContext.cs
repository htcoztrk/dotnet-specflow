using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace TestAutomation.Framework.DomainLayer.Contracts {
    public interface ISqlContext : IContext {
        void ExecuteNonQuery(StoredProcedure storedProcedure);
        IList<T> ExecuteReader<T>(StoredProcedure storedProcedure);
        IList<T> ExecuteReader<T>(StoredProcedure storedProcedure, DynamicParameters objParameter);
        IList<T> ExecuteQuery<T>(string query);
        int ExecuteCommand(string query);
        SqlCommand SmartSqlCommand(string sql);
        SqlCommand AddSpParameter(SqlCommand cmd, string query, string paramName, object paramValue);
    }
}
