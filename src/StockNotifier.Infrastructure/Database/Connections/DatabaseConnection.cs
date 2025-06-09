using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StockNotifier.Application.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.Database.Connections
{
    public class DatabaseConnection : DatabaseConnectionStringProvider, IDatabaseConnection
    {
        public DatabaseConnection(DatabaseOptions dbOptions, IConfiguration configuration) : base(dbOptions, configuration)
        { }

        public IDbConnection GetConnection() => new SqlConnection(GetConnectionString());
    }
}
