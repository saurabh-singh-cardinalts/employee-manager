using System.Data.Entity;
using EM.Model;

namespace EM.Data
{
    public class EMContext : DbContext
    {
        public EMContext() : base("emContext") { }

        public DbSet<EMMembership> Membership { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
