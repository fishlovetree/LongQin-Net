using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{

    public class DapperFactory
    {
        public static IDbConnection GetConnection()
        {
            return new SqlConnection(AppSettings.ConnectionString);
        }
    }
}
