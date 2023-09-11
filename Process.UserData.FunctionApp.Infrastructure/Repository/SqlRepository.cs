using Microsoft.Data.SqlClient;
using Process.UserData.FunctionApp.Domain.Interfaces;
using System.Data;

namespace Process.UserData.FunctionApp.Infrastructure.Repository
{
    public class SqlRepository : ISqlRepository
    {
        private readonly SqlConnection _sqlConnection;

        public SqlRepository(string connectionString)
        {
            _sqlConnection = CreateConnection(connectionString);
        }

        public DataTable ExecuteReader(string storedProcedureName, ICollection<SqlParameter> parameters)
        {
            var dataTable = new DataTable();
            var sqlCommand = _sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = storedProcedureName;
            sqlCommand.Parameters.Add(parameters);

            using (sqlCommand)
            {
                try
                {
                    if (sqlCommand.Connection.State == ConnectionState.Closed) sqlCommand.Connection.Open();
                    sqlCommand.CommandTimeout = 3600;
                    var dataReader = sqlCommand.ExecuteReader();
                
                    dataTable.Load(dataReader);
                }
                finally
                {
                    sqlCommand.Connection.Close();
                }
            }

            return dataTable;
        }

        private SqlConnection CreateConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("Connection-String is not defined in app config.");
            }

            return new SqlConnection(connectionString);
        }
    }
}
