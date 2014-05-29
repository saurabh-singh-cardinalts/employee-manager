using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Data;

namespace EM.DbCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Initialize Database");
                Console.WriteLine("2. Migrate Database");
                Console.WriteLine("3. Exit");
                Console.WriteLine("---------------------------------");
                Console.Write("Enter your choice: ");
            }
           
        }
    }

    public class DatabaseInitializer
    {
        private readonly EMContext _context;

        public DatabaseInitializer()
        {
            _context = new EMContext();
        }


        public void InitializeDatabase(IDatabaseInitializer<EMContext> initializer)
        {

            Database.SetInitializer(initializer);

            _context.Database.Initialize(true);
        }

        public void MigrateDatabase()
        {
            
        }
    }

    
}
