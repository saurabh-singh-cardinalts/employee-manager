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

                int choice = Convert.ToInt32(Console.ReadKey());

                switch (choice)
                {
                    case 1: 
                        CreateDatabase();
                        break;
                    case 2:
                        MigrateDatabase();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please select a proper option");
                        break;
                }

            }
           
        }

        public static void CreateDatabase()
        {
            var context = new EMContext();
            Database.SetInitializer(new DropCreateInitializer());
            context.Database.Initialize(true);
        }

        public static void MigrateDatabase()
        {
            var context = new EMContext();
            Database.SetInitializer(new EMMigrationInitializer<EMContext>());
            context.Database.Initialize(true);

        }

    }

   

    
}
