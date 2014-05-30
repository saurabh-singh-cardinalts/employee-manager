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
            HasMany(t => t.Roles).WithMany().Map(m => { m.ToTable("UserRole"); m.MapLeftKey("UserId"); m.MapRightKey("RoleName"); });
            HasRequired(t => t.Membership).WithRequiredPrincipal(t => t.User).WillCascadeOnDelete();
            
            HasOptional(t => t.Address).WithMany().HasForeignKey(t => t.AddressId);
            HasOptional(t => t.UserProfile).WithMany().HasForeignKey(t => t.UserProfileId).WillCascadeOnDelete();
        }
    }


    public class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.OtherId).IsOptional();
            Property(t => t.DLNumber).IsOptional();
            Property(t => t.DLExpiryDate).IsOptional();
            Property(t => t.AdharCard).IsOptional();
            Property(t => t.BankAccount).IsOptional();
            Property(t => t.PanCard).IsOptional();
            Property(t => t.VoterId).IsOptional();
        }
    }
}