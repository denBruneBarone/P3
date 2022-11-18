using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPFAdminSystem.Database
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
        Task SaveDataNoParams<T>(string sql);
        Task<T> GetSingleData<T>(string sql);
    }
}