using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Coelsa.Challenge.Api.Data.Cnx
{
    public class ConnectionFactory : IConnectionFactory
    {
        IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection
        {
            get
            {
                var sqlCnx = new SqlConnection();
                if (sqlCnx == null) return null;

                sqlCnx.ConnectionString = _configuration.GetConnectionString("CnxDatabase");
                sqlCnx.Open();
                return sqlCnx;
            }
        }
    }
}
