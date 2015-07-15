using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    // this is in full repo pattern
    public class PersonRepository : Repository<Person>
    {


        public override void Insert(Person person)
        {
            using (var connection = DB.GetSqlConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Person.APerson (FirstName, LastName, ModifiedDate) VALUES(@firstName, @lastName, SYSDATETIME())";
                    command.AddParameter("firstName", person.FirstName);
                    command.AddParameter("lastName", person.LastName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public override void Update(Person person)
        {
            using (var connection = DB.GetSqlConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE Person.APerson SET FirstName = @firstName, LastName = @lastName WHERE Id = @Id";
                    command.AddParameter("firstName", person.FirstName);
                    command.AddParameter("lastName", person.LastName);
                    command.AddParameter("Id", person.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public override void Delete(Person person)
        {
            using (var connection = DB.GetSqlConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM Person.APerson WHERE Id = @Id";
                    command.AddParameter("Id", person.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public override IEnumerable<Person> Read()
        {
            using (var connection = DB.GetSqlConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM Person.APerson";
                    return ToList(command);
                }
            }
        }

        public override  Person Find(int id)
        {
            using (var connection = DB.GetSqlConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM Person.APerson WHERE Id = @id";
                    command.AddParameter("id", id);
                    var a = ToList(command);
                    return a.FirstOrDefault();
                }
            }            
        }

        public IEnumerable<Person> GetByFirstName(string firstName)
        {
            var p = new List<Person>();
            using (var conn = DB.GetSqlConnection())
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"select * from Person.APerson where FirstName LIKE @firstName";
                    command.AddParameter("firstName", firstName);
                    return ToList(command);
                }
            }
        }

        protected override void Map(IDataRecord record, Person person)
        {
            person.FirstName =          (string)record["FirstName"];
            person.LastName =           (string)record["LastName"];
            person.ModifiedDate =       (DateTime)record["ModifiedDate"];
            person.Id =                 (int) record["Id"];

        }

        protected override Person CreateEntity()
        {
            return new Person();
        }
    }
}
