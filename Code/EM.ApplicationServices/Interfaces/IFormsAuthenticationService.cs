namespace EM.ApplicationServices.Interfaces
{
    public interface IFormsAuthenticationService
    {
        void SignIn(UserData userData, bool createPersistentCookie);
        void SignOut(UserData userData);
        void UpdateUserData(UserData userData, bool createPersistentCookie);
    }
}