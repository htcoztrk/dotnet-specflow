using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using TestAutomation.Framework.Core.ServiceContracts;
using TestAutomation.Framework.DomainLayer.Contracts;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace TestAutomation.Framework.InfrastructureLayer.Adapters.DataContexts {
    public class DapperContext : ISqlContext, IDisposable {
        protected IDbConnection DbConnection;

        public DapperContext(IDBService dbService) {
            if (dbService == null) {
                throw new ArgumentException("Db service null olmamalı.");
            }
            DbConnection = dbService.GetDataBaseConnection();
        }
        public DapperContext(string connectionString) {
            if (string.IsNullOrEmpty(connectionString)) {
                throw new ArgumentException("Connection string null olmamalı.");
            }
            DbConnection = new SqlConnection(connectionString);
            OpenConnection();
        }

        private void OpenConnection() {
            if (DbConnection.State.Equals(ConnectionState.Closed)) {
                DbConnection.Open();
            }
        }
        public SqlCommand SmartSqlCommand(string sql) {
            SqlCommand cmd = new SqlCommand(sql) {
                Connection = (SqlConnection)DbConnection
            };
            return cmd;
        }
        public SqlCommand AddSpParameter(SqlCommand cmd, string query, string paramName, object paramValue) {
            if (query != null && cmd != null && query.Contains(paramName)) {
                SqlParameter param = new SqlParameter {
                    ParameterName = "@" + paramName,
                    Value = paramValue
                };
                cmd.Parameters.Add(param);
            }
            return cmd;
        }
        public void ExecuteNonQuery(StoredProcedure storedProcedure) {
            if (storedProcedure == null) {
                throw new ArgumentException("StoredProcedure null olmamalı.");
            }
            DbConnection.Execute(storedProcedure.ProcedureName, commandType: CommandType.StoredProcedure);
        }

        public IList<T> ExecuteReader<T>(StoredProcedure storedProcedure) {
            if (storedProcedure == null) {
                throw new ArgumentException("StoredProcedure null olmamalı.");
            }
            var queryResult = DbConnection.
                                    Query<T>(storedProcedure.ProcedureName, commandType: CommandType.StoredProcedure); // IEnumerable<T> return ediyor.

            return (IList<T>)queryResult;
        }
        public IList<T> ExecuteReader<T>(StoredProcedure storedProcedure, DynamicParameters objParameter) {
            if (storedProcedure == null) {
                throw new ArgumentException("StoredProcedure null olmamalı.");
            }
            var queryResult = DbConnection.
                                    Query<T>(storedProcedure.ProcedureName, objParameter, commandType: CommandType.StoredProcedure);

            return (IList<T>)queryResult;
        }
        /// <summary>
        /// Parametre olarak aldığı sorguyu çalıştırır, sonucunu dönderir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>

        public IList<T> ExecuteQuery<T>(string query) {
            var queryResult = DbConnection.Query<T>(query);
            return (IList<T>)queryResult;
        }
        /// <summary>
        /// Parametre olarak aldığı commandi çalıştırır, etkilenen satır sayısını dönderir.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int ExecuteCommand(string query) {
            return DbConnection.Execute(query);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                DbConnection.Close();
                DbConnection.Dispose();
            }
        }
    }
}
