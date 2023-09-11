using Microsoft.Data.SqlClient;
using System.Data;

namespace Process.UserData.FunctionApp.Domain.Interfaces
{
    public interface ISqlRepository
    {
        DataTable ExecuteReader(string storedProcedureName, ICollection<SqlParameter> parameters);
    }
}
