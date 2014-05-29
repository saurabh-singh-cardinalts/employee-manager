using System.Data.Entity;

namespace EM.Data
{
    public class DropCreateInitializer:DropCreateDatabaseIfModelChanges<EMContext>
    {   
        
         
    }

    public class MigrateInitializer : IDatabaseInitializer<EMContext>
    {
        public void InitializeDatabase(EMContext context)
        {
            //context.
        }
    }

   
}