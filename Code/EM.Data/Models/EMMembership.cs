using System;

namespace EM.Model
{
    public class EMMembership
    {
            public int Id { get; set; }
            public User User { get; set; }
            public string ConfirmationToken { get; set; }
            public bool? IsConfirmed { get; set; }
            public DateTime? LastPasswordFailureDate { get; set; }
            public int? PasswordFailuresSinceLastSuccess { get; set; }
            public string Password { get; set; }
            public DateTime? PasswordChangedDate { get; set; }
            public string PasswordVerificationToken { get; set; }
            public DateTime? PasswordVerificationTokenExpirationDate { get; set; }
    }

    public enum Roles
    {
        Employee,
        Admin
    }

    public enum UserStatus
    {
        Active,
        InActive
    }

  
    public enum Gender
    {
        Male,
        Female
    }

    public class Address 
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string OtherNumber { get; set; }
    }

    public class Role
    {
        public Roles Value { get; set; }
        public string RoleName
        {
            get { return Value.ToString(); }
            set
            {
                Roles role;
                if (Enum.TryParse(value, true, out role))
                    Value = role;
            }
        }

    }

    public class OtherInformation
    {
        public string AdharCard { get; set; }
        public string PanCard { get; set; }
        public string BankAccount { get; set; }
        public string VoterId { get; set; }
    }















}
