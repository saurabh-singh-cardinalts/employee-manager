using System.Data.Entity;
using EM.Data.Mappings;
using EM.Data.Models;

namespace EM.Data
{
    public class EMContext : DbContext
    {
        public EMContext() : base("emContext") { }

        public DbSet<Membership> Membership { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new MembershipConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
