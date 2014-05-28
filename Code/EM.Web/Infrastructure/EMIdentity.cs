using System.Security.Principal;
using System.Web.Security;
using EM.ApplicationServices;
using Newtonsoft.Json;

namespace EM.Web.Infrastructure
{
    public class EMIdentity : IIdentity
    {
        private readonly FormsAuthenticationTicket _ticket;
        private readonly UserData _user;

        public EMIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
            _user = JsonConvert.DeserializeObject<UserData>(ticket.UserData);
        }

        public string AuthenticationType
        {
            get { return "Cardinal Employee Manager"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public UserData UserData
        {
            get { return _user; }
        }

        public string Name
        {
            get { return _ticket.Name; }
        }

        public string UserType
        {
            get { return _user.UserType; }
        }

        public string DisplayName
        {
            get { return _user.DisplayName; }
        }
    }
}