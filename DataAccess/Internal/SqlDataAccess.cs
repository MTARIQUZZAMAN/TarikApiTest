using Application.Common.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Internal
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string sql,
            U parameters,
            CommandType commandtype,
            string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(sql, parameters, commandType: commandtype);
        }

        public async Task<int> SaveData<T>(
            string sql,
            T parameters,
            CommandType commandtype,
            string connectionId = "Default")
        {

            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            int rowsAffected = await connection.ExecuteAsync(sql, parameters, commandType: commandtype);
            return rowsAffected;

        }





    }
}
