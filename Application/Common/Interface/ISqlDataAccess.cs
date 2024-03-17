using System.Data;

namespace Application.Common.Interface
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters, CommandType commandtype, string connectionId = "Default");
        Task<int> SaveData<T>(string sql, T parameters, CommandType commandtype, string connectionId = "Default");
        Task<int> InsertSingle<U>(string sql, U parameters, CommandType commandtype, string connectionId = "Default");
    }
}
