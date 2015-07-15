//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataLayer
//{
//    //
//    public class SSPersonRepository : Repository<Person>
//    {
//        public List<Person> GetAll()
//        {
//            var p = new List<Person>();
//            using (var conn = DB.GetSqlConnection())
//            {
//                using (var command = conn.CreateCommand())
//                {
//                    command.CommandText = "select * from Person.Person";
//                    using (var reader = command.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            var p1 = new Person()
//                            {
//                                FirstName = reader["FirstName"].ToString(),
//                                LastName = reader["LastName"].ToString()

//                            };
//                            p.Add(p1);
//                        }
//                    }

//                }
                
//            }
//            return p;
//        }

//        // obviouslly terible, but it is not the point...
//        public Person GetByFirstName(string firstName)
//        {
//            var p = new Person();
//            using (var conn = DB.GetSqlConnection())
//            {
//                using (var command = conn.CreateCommand())
//                {
//                    command.CommandText = "select * from Person.Person where FirstName LIKE '" + firstName + "'" ;
//                    using (var reader = command.ExecuteReader())
//                    {
//                        reader.Read();
//                        p.FirstName = reader["FirstName"].ToString();
//                        p.LastName = reader["LastName"].ToString();
//                    }

//                }

//            }
//            return p;
//        }

//        // this is a safe query
//        public IEnumerable<Person> GetByFirstNameProc(string firstName)
//        {
//            var p = new List<Person>();
//            using (var conn = DB.GetSqlConnection())
//            {
//                using (var command = conn.CreateCommand())
//                {
//                    // the stored proc style is as performant AND as fast as
//                    command.CommandText = "select * from Person.Person where FirstName LIKE @firstName";
//                    // this is the out of the box method that is provided
//                    //command.Parameters.AddWithValue("firstName", firstName);
                    
//                    command.AddParameter("firstName", firstName);

//                    // alternative to add a parameter
//                    //var p1 = new SqlParameter("firstName", System.Data.SqlDbType.NVarChar, 100);
//                    //p1.Value = firstName;
//                    return ToList(command);
//                    //using (var reader = command.ExecuteReader())
//                    //{
//                    //    while (reader.Read())
//                    //    {
//                    //        // mapping should not be here.
//                    //        // mapping method in the class itslef
//                    //        var p1 = new Person();
//                    //        Map(reader,p1);
                            
//                    //        p.Insert(0, p1);
//                    //    }
//                    //}

//                }

//            }
//            return p;
//        }


//        protected override void Map(IDataRecord record, Person person)
//        {
//            person.FirstName = (string)record["FirstName"];
//            person.LastName = (string)record["LastName"];
//        }

//        protected override Person CreateEntity()
//        {
//            return new Person();
//        }
//    }
//}
