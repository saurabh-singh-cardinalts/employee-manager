using System.Data.Entity;

namespace EM.Data
{
    public class EMMigrationInitializer<T> : MigrateDatabaseToLatestVersion<T, EMMigrationConfiguration<T>> where T : EMContext
    { 
        
    }
}