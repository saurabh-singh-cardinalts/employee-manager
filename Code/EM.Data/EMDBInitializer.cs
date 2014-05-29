using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace EM.Data
{
    public class DropCreateInitializer:DropCreateDatabaseIfModelChanges<EMContext>
    {   
        
         
    }

    public class EMMigrationConfiguration<T> : DbMigrationsConfiguration<T> where T:EMContext
    {
        public EMMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }
    }

    public class EMMigrationInitializer<T> : MigrateDatabaseToLatestVersion<T, EMMigrationConfiguration<T>> where T : EMContext
    { 
        
    }



    
   
}