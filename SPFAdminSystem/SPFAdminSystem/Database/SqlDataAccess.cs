using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace SPFAdminSystem.Database
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public string ConnectionStringName { get; set; } = "Default";

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string DbPath = System.IO.Path.Join(path, "Spilforsyning.db");
            string connectionString = $"Data Source={DbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);

                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string DbPath = System.IO.Path.Join(path, "Spilforsyning.db");
            string connectionString = $"Data Source={DbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                object value = await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
