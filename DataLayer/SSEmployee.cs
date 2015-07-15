using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Employees
    {
        public List<SSEmployee> EmployeeList { get; set; }

        /// <summary>
        /// get employee with store procedure
        /// </summary>
        /// <param name="emploeeId"></param>
        /// <returns></returns>
        public SSEmployee GetEmployee(int employeeId)
        {
            // this is the store procedure in the database
            // create procedure GetEmployeeDetails
            //    @BusinessEntityId int

            //AS

            //set nocount on

            //select * from HumanResources.Employee E 
            //                JOIN Person.Person P ON E.BusinessEntityID = P.BusinessEntityID AND P.PersonType = 'EM'
            //                JOIN HumanResources.EmployeeDepartmentHistory EH ON E.BusinessEntityID = EH.BusinessEntityID
            //                JOIN HumanResources.Department D on D.DepartmentID = EH.DepartmentID
            //                WHERE
            //                E.BusinessEntityID = @BusinessEntityId
            
            
            var e = new SSEmployee();
            //this is the disposable connection
            using (var conn = DB.GetSqlConnection())
            {
                // this is the command that will be exectured
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"GetEmployeeDetails";
                    // must set that it is a stored procedure, normally it expects an in line command
                    // To avoid SQL injection one should always use parameterized queries. In more recent versions of SQL Server they are actually as fast as stored procedures.
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // declare the parameter
                    var p1 = new SqlParameter("businessEntityId", System.Data.SqlDbType.Int);
                    p1.Value = employeeId;

                    // add the parameter
                    cmd.Parameters.Add(p1);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        e.Load(reader);
                    }
                }

            }
            return e; 
        }

        public SSEmployee GetEmployeeNoSproc(int emploeeId)
        {
            var e = new SSEmployee();
            //this is the disposable connection
            using (var conn = DB.GetSqlConnection())
            {
                // this is the command that will be exectured
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // this is the sql query that is run in the database
                    cmd.CommandText = @"select * from HumanResources.Employee E 
				JOIN Person.Person P ON E.BusinessEntityID = P.BusinessEntityID AND P.PersonType = 'EM'
				JOIN HumanResources.EmployeeDepartmentHistory EH ON E.BusinessEntityID = EH.BusinessEntityID
				JOIN HumanResources.Department D on D.DepartmentID = EH.DepartmentID
				WHERE
				E.BusinessEntityID = {0}";
                    cmd.CommandText = string.Format(cmd.CommandText, emploeeId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        e.Load(reader);
                    }
                }
                
            }
            return e; 
        }

        
    }
    public class SSEmployee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName  { get; set; }

        public void Load(SqlDataReader reader)
        {
            EmployeeId = Int32.Parse(reader["BusinessEntityId"].ToString());
            FirstName = reader["FirstName"].ToString();
            LastName = reader["LastName"].ToString();
            DepartmentId = Int32.Parse(reader["DepartmentId"].ToString());
            DepartmentName = reader["Name"].ToString();
        }
    }
}
