using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;


namespace DataLayer
{
    public class DB
    {
        
        public static string ConnectionString
        {
            get { 
                var connString = ConfigurationManager.ConnectionStrings["AWconnect"].ToString(); 

                // this allows to parse a conneciotn string and manipulate it.
                var sb = new SqlConnectionStringBuilder(connString);
                // in case the application name already exists we replace it
                sb.ApplicationName = ApplicationName ?? sb.ApplicationName;
                sb.ConnectTimeout = (ConnectionTimeout > 0) ? ConnectionTimeout : sb.ConnectTimeout;
                return sb.ToString();
            }
        }

        /// <summary>
        /// application name
        /// </summary>
        public static string ApplicationName { get; set; }

        /// <summary>
        /// override time out
        /// </summary>
        public static int ConnectionTimeout { get; set; }

        /// <summary>
        /// get the connection string in sql and opens it
        /// </summary>
        /// <returns>connectoin string</returns>
        public static SqlConnection GetSqlConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        public static DbConnection GetConnectionWithFactory()
        {
            // requires mysql connection 
            var factory = DbProviderFactories.GetFactory(ConnectionString);
            var conn = factory.CreateConnection();
            conn.Open();
            return conn;
        }
    }
}
