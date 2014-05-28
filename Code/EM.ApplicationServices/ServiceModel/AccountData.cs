using System;
using System.Collections.Generic;

namespace EM.ApplicationServices.ServiceModel
{
    public class AccountData
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string MobileNumber { get; set; }
        public string DisplayName { get; set; }
        public string RoleType { get; set; }
        public UserType UserType { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public enum UserType
    {
        Employee,
        Admin
    }

    public class PasswordTokenResponse
    {
        public string PasswordToken { get; set; }
        public string MailType { get; set; }
    }
}