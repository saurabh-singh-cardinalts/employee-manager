using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EM.Data.Models;

namespace EM.Data.Mappings
{
    public class UserConfiguration:EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FirstName).HasMaxLength(256).IsRequired();
            Property(t => t.LastName).HasMaxLength(256).IsRequired();
            Property(t => t.UserName).HasMaxLength(256).IsRequired();
            Property(t => t.Email).HasMaxLength(256).IsRequired();
            Property(t => t.Phone).HasMaxLength(256).IsRequired();
            Ignore(t => t.Status);
            Ignore(t => t.Gender);
            Ignore(t => t.OtherInformation);
            HasOptional(t => t.Address).WithMany().HasForeignKey(t => t.AddressId);
            HasMany(t => t.Roles).WithMany().Map(m => { m.ToTable("UserRole"); m.MapLeftKey("UserId"); m.MapRightKey("RoleName"); });
            HasRequired(t => t.EMMembership).WithRequiredPrincipal(t => t.User).WillCascadeOnDelete();
            //HasOptional(t => t.UserPreference).WithRequired().WillCascadeOnDelete(true);
            

        }
    }

    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            HasKey(t => t.RoleName);
            Property(t => t.RoleName).HasColumnName("RoleName").IsRequired().HasMaxLength(128);
            Ignore(t => t.Value);
        }
    }

    public class MembershipConfiguration : EntityTypeConfiguration<EMMembership>
    {
        public MembershipConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("PmlMemberShipId");
            Property(t => t.ConfirmationToken).HasMaxLength(128);
            Property(t => t.Password).IsRequired().HasMaxLength(128);
            Property(t => t.PasswordVerificationToken).HasMaxLength(128);

        }
    }
}