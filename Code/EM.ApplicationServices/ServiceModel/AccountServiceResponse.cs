namespace EM.ApplicationServices.ServiceModel
{
    public class AccountServiceResponse
    {
        public string UserName { get; set; }
        public UserType UserType { get; set; }
        public string LoginType { get; set; }
        public string DisplayName { get; set; }
    }
}