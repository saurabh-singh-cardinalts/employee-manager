using System.Data.Entity;
using EM.Data.Mappings;
using EM.Data.Models;
using EM.Model;

namespace EM.Data
{
    public class EMContext : DbContext
    {
        public EMContext() : base("emContext") { }

        public DbSet<EMMembership> Membership { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
