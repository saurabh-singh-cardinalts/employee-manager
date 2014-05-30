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
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Education> Educations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new MembershipConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
