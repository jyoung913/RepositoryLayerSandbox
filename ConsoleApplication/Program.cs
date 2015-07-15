using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace ConsoleApplication
{
    class Program
    {
        
        // left at stored procedures
        static void Main(string[] args)
        {
            DataLayer.DB.ApplicationName = "ciccio pasticcio";
            DataLayer.DB.ConnectionTimeout = 60;


            //var conn = DataLayer.DB.GetSqlConnection();
            // Console.WriteLine(DataLayer.DB.ConnectionString);

            var employees = new Employees();
            var id = 1;
            var emp = employees.GetEmployee(id);
            //Console.Write(emp.FirstName);

            DataLayer.SSApplicationLog.Add("serached for id " + id);

            //DataLayer.ApplicationLog.DeleteCommentsForApp("ciccio pasticcio");
            var personRepo = new PersonRepository();
            var andrea = new Person()
            {
                FirstName = "Chelsea",
                LastName = "Cremese"
            };
            personRepo.Insert(andrea);
            //personRepo.Delete(andrea);
            
            //var p = personRepo.Find(1);
            //Console.Write(p.LastName);
            var a = personRepo.Read();
            // fot this you'd need to create your own expression tree.
            //var p = personRepo.Find(p => p.FirstName == "Andrea");
            Console.Read();
        }
    }
}

