using System;
using System.Collections.Generic;
using EM.Model;

namespace EM.Data.Models
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string DisplayName { get; set; }
        public DateTime? BirthDay { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public UserStatus Status { get; set; }
        public string StatusString
        {
            get { return Status.ToString(); }
            set
            {
                UserStatus status;
                if (Enum.TryParse(value, true, out status))
                    Status = status;
            }
        }

        public Gender Gender { get; set; }
        public string GenderString
        {
            get { return Gender.ToString(); }
            set
            {
                Gender gender;
                if (Enum.TryParse(value, true, out gender))
                    Gender = gender;
            }
        }

        public Address Address { get; set; }
        public int? AddressId { get; set; }

        public List<Role> Roles { get; set; }
        public OtherInformation OtherInformation { get; set; }
       
        public DateTime? LastLoggedInDate { get; set; }
        public DateTime RegistrationDate { get; set; }
     
        public EMMembership EMMembership { get; set; }
    }
}