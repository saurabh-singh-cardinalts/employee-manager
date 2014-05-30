using System.Data.Entity.ModelConfiguration;
using EM.Data.Models;

namespace EM.Data.Mappings
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            HasKey(t => t.RoleName);
            Property(t => t.RoleName).HasColumnName("RoleName").IsRequired().HasMaxLength(128);
            Ignore(t => t.Value);
        }
    }
}