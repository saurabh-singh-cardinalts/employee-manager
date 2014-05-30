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

    public class QualificationConfiguration : EntityTypeConfiguration<Qualification>
    {
        public QualificationConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(t => t.Languages).WithOptional().HasForeignKey(t => t.Id).WillCascadeOnDelete();
            HasMany(t => t.WorkExperiences).WithOptional().HasForeignKey(t => t.Id).WillCascadeOnDelete();
            HasMany(t => t.Educations).WithOptional().HasForeignKey(t => t.Id).WillCascadeOnDelete();
            HasMany(t => t.Skills).WithOptional().HasForeignKey(t => t.Id).WillCascadeOnDelete();
            HasMany(t => t.Languages).WithOptional().HasForeignKey(t => t.Id).WillCascadeOnDelete();
            HasMany(t => t.Licenses).WithOptional().HasForeignKey(t => t.Id).WillCascadeOnDelete();
        }
    }

    public class LicenseConfiguration : EntityTypeConfiguration<License>
    {
        public LicenseConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t=>t.LicenseType).IsOptional().HasMaxLength(128);
            Property(t => t.IssuedDate).IsOptional();
            Property(t => t.ExpiryDate).IsOptional();
        }
    }

    public class SkillConfiguration : EntityTypeConfiguration<Skill>
    {
        public SkillConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsOptional().HasMaxLength(256);
            Property(t => t.Year).IsOptional();
        }
    }

    public class LanguageConfiguration : EntityTypeConfiguration<Language>
    {
        public LanguageConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsOptional().HasMaxLength(256);
            Property(t => t.Fluency).IsOptional().HasMaxLength(256);
            Property(t => t.Competency).IsOptional().HasMaxLength(256);
            Property(t => t.Comments).IsOptional().HasMaxLength(256);
        }
    }

    public class WorkExperienceConfiguration : EntityTypeConfiguration<WorkExperience>
    {
        public WorkExperienceConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CompanyName).IsOptional().HasMaxLength(256);
            Property(t => t.JobTitle).IsOptional().HasMaxLength(256);
            Property(t => t.StartDate).IsOptional();
            Property(t => t.EndDate).IsOptional();
            Property(t => t.Comment).IsOptional().HasMaxLength(256);
        }
    }

    public class EducationConfiguration : EntityTypeConfiguration<Education>
    {
        public EducationConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.EducationLevel).IsOptional().HasMaxLength(256);
            Property(t => t.Year).IsOptional();
            Property(t => t.Score).IsOptional();
        }
    }

}