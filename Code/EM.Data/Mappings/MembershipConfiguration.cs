using System.Data.Entity.ModelConfiguration;
using EM.Data.Models;

namespace EM.Data.Mappings
{
    public class MembershipConfiguration : EntityTypeConfiguration<Membership>
    {
        public MembershipConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("MemberShipId");
            Property(t => t.ConfirmationToken).HasMaxLength(128);
            Property(t => t.Password).IsRequired().HasMaxLength(128);
            Property(t => t.PasswordVerificationToken).HasMaxLength(128);

        }
    }
}