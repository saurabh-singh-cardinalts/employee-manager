using System;
using EM.ApplicationServices.ServiceModel;

namespace EM.ApplicationServices.Interfaces
{
    public interface IAccountMembershipService
    {
        void CreateUserAndAccount(string userName, string password, AccountData userData = null);
        void CreateEmployeeAccount(string userName, string password, AccountData userData = null);
        AccountServiceResponse ValidateUser(string userName, string password, int invalidattempts);
        AccountServiceResponse ResetPassword(string userName, string passwordToken, string password);
        //bool UpdateUserProfile(string userName, string oldPassword, string newPassword, AccountData userData = null);
        //string ResetStudentPassword(string userName);
        void ChangePassword(string userName, string oldPassword, string newPassword, int invalidattempts);
        bool UserExist(string userName);
        bool IsUserExist(string userName, string loggedInUser);
        DateTime GetUserCreatedDate(string userName);
    }
}