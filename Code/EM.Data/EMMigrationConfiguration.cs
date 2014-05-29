using System.Data.Entity.Migrations;

namespace EM.Data
{
    public class EMMigrationConfiguration<T> : DbMigrationsConfiguration<T> where T:EMContext
    {
        public EMMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }
    }
}