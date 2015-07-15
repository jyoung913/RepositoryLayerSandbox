using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SSApplicationLog
    {
        public static void Add(string comment)
        {
            //this is the disposable connection
            using (var conn = DB.GetSqlConnection())
            {
                // this is the command that will be exectured
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"AddAppLog";
                    // must set that it is a stored procedure, normally it expects an in line command
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // declare the parameter
                    var p1 = new SqlParameter("comment", System.Data.SqlDbType.NVarChar,100);
                    p1.Value = comment;

                    // add the parameter
                    cmd.Parameters.Add(p1);

                     int res = cmd.ExecuteNonQuery();
                }
            }
        }

        public static void AddWithFactory(string comment)
        {
            //this is the disposable connection
            using (var conn = DB.GetConnectionWithFactory())
            {
                // this is the command that will be exectured
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"AddAppLog";
                    // must set that it is a stored procedure, normally it expects an in line command
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // declare the parameter
                    var p1 = new SqlParameter("comment", System.Data.SqlDbType.NVarChar, 100);
                    p1.Value = comment;

                    // add the parameter
                    cmd.Parameters.Add(p1);

                    int res = cmd.ExecuteNonQuery();
                }
            }           
        }

        public static void DeleteCommentsForApp(string appName)
        {
            //this is the disposable connection
            using (var conn = DB.GetSqlConnection())
            {
                // this is the command that will be exectured
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DeleteAppLog";
                    // must set that it is a stored procedure, normally it expects an in line command
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // declare the parameter
                    var p1 = new SqlParameter("appName", System.Data.SqlDbType.NVarChar, 100);
                    p1.Value = appName;

                    // add the parameter
                    cmd.Parameters.Add(p1);
                    cmd.ExecuteNonQuery();
                }
            }
            
        }
    }
}


//create table ApplicationLog (
//    id int IDENTITY(1,1) primary key,
//    date_added DATETIME NOT NULL Default(getutcdate()),
//    comment ntext not null,
//    application_name nvarchar(100)
//)

//create procedure AddAppLog
// @comment ntext
// as
// set nocount on
// insert into ApplicationLog Values (
// default, @comment, (select app_name()))

//AddAppLog 'some message'

//select * from ApplicationLog


//Create procedure DeleteAppLog
//@appname varchar(100)

//as

//set nocount on

//delete from ApplicationLog where application_name = @appname

//go
