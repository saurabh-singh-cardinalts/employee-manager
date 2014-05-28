using System;
using System.Collections.Generic;
using System.Linq;
using EM.ApplicationServices.Infrastructure;
using EM.ApplicationServices.Interfaces;
using EM.ApplicationServices.ServiceModel;
using EM.Data.Models;
using EM.Framework.Data.Repository;
using EM.Framework.Extensions;
using EM.Model;
using EM.Specification.Interfaces;

namespace EM.ApplicationServices
{
    public class AccountMembershipService : IAccountMembershipService
    {
        #region Private Members
   
        private readonly IUnitOfWork _unitofwork;
        private readonly IRepository<User> _userRepository;
        //private const int TOKEN_EXPIRATION_IN_MINUTES_FROM_NOW = 1440; //24hrs 

        #endregion

        #region Ctor

        public AccountMembershipService(IRepository<User> userRepository,
            IUnitOfWork unitofwork)
        {
            _userRepository = userRepository;
            _unitofwork = unitofwork;
        }

        #endregion

        #region Public Methods

        public void CreateUserAndAccount(string userName, string password, AccountData userData = null)
        {
            if (userData != null)
                switch (userData.UserType)
                {
                    case UserType.Employee:
                    {
                        CreateEmployeeAccount(userName, password, userData);
                    }
                        break;
                    case UserType.Admin:
                    {
                        CreateEmployeeAccount(userName, password, userData);
                    }
                        break;
                }
        }

        public void CreateEmployeeAccount(string userName, string password, AccountData userData = null)
        {
            var user = _userRepository.Read<IUserSpecification>().WithName(userName).IncludeMemberShip().ToResult().SingleOrDefault();
            if (user != null)
                throw new EMApplicationException("UserName", EMApplicationConstants.DuplicateEmailAddress);


            var newUser = new User
            {
                Roles = new List<Role> { new Role { Value = Roles.Employee } },
                DisplayName = userData.DisplayName,
                UserName = userName,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userName,
                Status = UserStatus.Active,
                RegistrationDate = DateTime.UtcNow,
                
            };
            CreateEMMemberShip(password, newUser);
        }

        public AccountServiceResponse ValidateUser(string userName, string password, int invalidattempts)
        {
            var loginType = string.Empty;
            var user = _userRepository.Read<IUserSpecification>().WithName(userName).IncludeMemberShip().ToResult().SingleOrDefault();
            if (user == null)
            {
                throw new EMApplicationException("UserName", EMApplicationConstants.IncorrectUserName);
            }


            var membership = user.EMMembership ?? new EMMembership();
            var hashedPassword = (membership.IsConfirmed ?? true) ? membership.Password : membership.PasswordVerificationToken.EncryptToMd5Hash();

            var verificationSucceeded = (hashedPassword != null && password.EncryptToMd5Hash() == hashedPassword);
            if (verificationSucceeded)
            {
                membership.PasswordFailuresSinceLastSuccess = 0;
                user.LastLoggedInDate = DateTime.UtcNow;
                if (membership.IsConfirmed == false)
                {
                    loginType = "ResetPassword";
                }
            }
            else
            {
                var failures = membership.PasswordFailuresSinceLastSuccess ?? 0;
                if (failures < invalidattempts)
                {
                    membership.PasswordFailuresSinceLastSuccess += 1;
                    membership.LastPasswordFailureDate = DateTime.UtcNow;
                }
                else if (failures >= invalidattempts)
                {
                    membership.LastPasswordFailureDate = DateTime.UtcNow;
                }
            }
            _userRepository.Update(user);
            _unitofwork.SaveChanges();

            if (!verificationSucceeded)
                throw new EMApplicationException("Password", EMApplicationConstants.IncorrectPassword);

            return CreateUserDataForCookie(loginType, user);
        }
        
        public void ChangePassword(string userName, string oldPassword, string newPassword, int invalidattempts)
        {
            var user = _userRepository.Read<IUserSpecification>().WithName(userName).IncludeMemberShip().ToResult().SingleOrDefault();

            string hashedPassword = user.EMMembership.Password;
            bool verificationSucceeded = (hashedPassword != null && oldPassword.EncryptToMd5Hash() == hashedPassword);
            if (verificationSucceeded)
            {
                user.EMMembership.PasswordFailuresSinceLastSuccess = 0;
                string newHashedPassword = newPassword.EncryptToMd5Hash();
                if (newHashedPassword.Length > 128)
                {
                    throw new EMApplicationException("UserName", EMApplicationConstants.InvalidNewOrOldPassword);
                }
                user.EMMembership.Password = newHashedPassword;
                user.EMMembership.LastPasswordFailureDate = DateTime.UtcNow;
            }
            else
            {
                int failures = user.EMMembership.PasswordFailuresSinceLastSuccess ?? 0;
                if (failures < invalidattempts)
                {
                    user.EMMembership.PasswordFailuresSinceLastSuccess += 1;
                    user.EMMembership.LastPasswordFailureDate = DateTime.UtcNow;
                }
                else if (failures >= invalidattempts)
                {
                    user.EMMembership.LastPasswordFailureDate = DateTime.UtcNow;

                }
                verificationSucceeded = false;

            }

            _userRepository.Update(user);
            _unitofwork.SaveChanges();
            if (!verificationSucceeded)
            {
                throw new EMApplicationException("Password", EMApplicationConstants.InvalidNewOrOldPassword);
            }


        }

        public AccountServiceResponse ResetPassword(string userName, string passwordToken, string password)
        {
            User user = null;
            if (!string.IsNullOrEmpty(userName))
            {
                user = _userRepository.Read<IUserSpecification>().WithName(userName).IncludeMemberShip().ToResult().SingleOrDefault();
                if (user == null)
                    throw new EMApplicationException("ResetPassword", EMApplicationConstants.InvalidUserName);

                var membership = user.EMMembership ?? new EMMembership();
                membership.LastPasswordFailureDate = DateTime.UtcNow;
                membership.PasswordChangedDate = DateTime.UtcNow;
                membership.PasswordFailuresSinceLastSuccess = 0;
                membership.PasswordVerificationToken = null;
                membership.PasswordVerificationTokenExpirationDate = null;
                membership.IsConfirmed = true;
                membership.Password = password.EncryptToMd5Hash();
            }
            else if (!string.IsNullOrEmpty(passwordToken))
            {

                user = _userRepository.Read<IUserSpecification>().WithToken(passwordToken).IncludeMemberShip().ToResult().SingleOrDefault();
                if (user == null)
                    throw new EMApplicationException("PasswordToken", EMApplicationConstants.InvalidToken);
                var membership = user.EMMembership ?? new EMMembership();
                membership.LastPasswordFailureDate = DateTime.UtcNow;
                membership.PasswordChangedDate = DateTime.UtcNow;
                membership.PasswordFailuresSinceLastSuccess = 0;
                membership.PasswordVerificationToken = null;
                membership.PasswordVerificationTokenExpirationDate = null;
                membership.IsConfirmed = true;
                membership.Password = password.EncryptToMd5Hash();
            }

            _userRepository.Update(user);
            _unitofwork.SaveChanges();

            return CreateUserDataForCookie(null, user);
        }

        //public bool UpdateUserProfile(string userName, string oldPassword, string newPassword, AccountData userData = null)
        //{
        //    var isSalesForceUser = UpdateSalesForceUser(UpdateUserMembership(userName, oldPassword, newPassword));


        //    switch (userData.UserType)
        //    {
        //        case UserType.Student:
        //            {
        //                UpdateStudentAccount(userName, userData);
        //            }
        //            break;
        //        case UserType.Educator:
        //            {
        //                UpdateEducatorAccount(userName, userData);

        //            }
        //            break;
        //        case UserType.Parent:
        //            {
        //                UpdateParentAccount(userName, userData);

        //            }
        //            break;
        //    }
        //    _unitofwork.SaveChanges();
        //    return isSalesForceUser;
        //}

        public bool UserExist(string userName)
        {
            bool isUserExist = true;
            var user = _userRepository.Read<IUserSpecification>().WithName(userName).ToResult().SingleOrDefault();
            if (user == null)
            {
                isUserExist = false;
                throw new EMApplicationException("UserName", EMApplicationConstants.InvalidParentUser);
            }
            return isUserExist;
        }

        public bool IsUserExist(string userName, string loggedInUser)
        {
            var user = _userRepository.Read<IUserSpecification>().WithName(userName).ToResult().SingleOrDefault();

            if (user == null)
            {
                throw new EMApplicationException("UserID", string.Format(EMApplicationConstants.UserNotExist, userName));
            }
            

            return true;
        }
        
        public DateTime GetUserCreatedDate(string userName)
        {
            var user = _userRepository.Read<IUserSpecification>().WithName(userName).ToResult().SingleOrDefault();
            if (user == null)
                throw new EMApplicationException("UserName", EMApplicationConstants.InvalidUserName);

            return user.CreatedOn.Value;

        }

        #endregion

        #region Private Methods

        private void CreateEMMemberShip(string password, User newUser)
        {
            newUser.EMMembership = new EMMembership
            {
                Id = newUser.Id,
                //User = newUser,
                Password = password.EncryptToMd5Hash(),
                IsConfirmed = true,
                LastPasswordFailureDate = DateTime.UtcNow,
                PasswordChangedDate = DateTime.UtcNow,
                PasswordFailuresSinceLastSuccess = 0,
                PasswordVerificationTokenExpirationDate = null,
                ConfirmationToken = null,
            };
            _userRepository.Create(newUser);
            _unitofwork.SaveChanges();
        }

        private AccountServiceResponse CreateUserDataForCookie(string loginType, User user)
        {
            var userType = user.Roles.FirstOrDefault(t => t.RoleName == Roles.Admin.ToString()) != null
                ? UserType.Admin
                : UserType.Employee;
           
            return new AccountServiceResponse { UserName = user.UserName, UserType = userType, LoginType = loginType, DisplayName = user.DisplayName};
        }

        private int UpdateUserMembership(string userName, string oldPassword, string newPassword)
        {
            var user = _userRepository.Read<IUserSpecification>().WithName(userName).IncludeMemberShip().ToResult().SingleOrDefault();
            if (user == null) 
                throw new EMApplicationException("UserName", EMApplicationConstants.IncorrectUserName);

            var membership = user.EMMembership;
            if (!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(oldPassword))
            {
                string hashedPassword = membership.Password;
                bool verificationSucceeded = (hashedPassword != null && oldPassword.EncryptToMd5Hash() == hashedPassword);
                if (!verificationSucceeded)
                    throw new EMApplicationException("OldPassword", EMApplicationConstants.ValidatePassword);

                membership.LastPasswordFailureDate = DateTime.UtcNow;
                membership.PasswordChangedDate = DateTime.UtcNow;
                membership.PasswordFailuresSinceLastSuccess = 0;
                membership.PasswordVerificationToken = null;
                membership.PasswordVerificationTokenExpirationDate = null;
                membership.IsConfirmed = true;
                membership.Password = newPassword.EncryptToMd5Hash();
                _userRepository.Update(user);
            }
            return user.Id;
        }

        #endregion
    }
}