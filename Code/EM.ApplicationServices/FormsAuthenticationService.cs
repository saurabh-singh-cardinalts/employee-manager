using System;
using System.Web;
using System.Web.Security;
using EM.ApplicationServices.Interfaces;
using Newtonsoft.Json;

namespace EM.ApplicationServices
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(UserData userData, bool createPersistentCookie)
        {
            var cookieData = JsonConvert.SerializeObject(userData);
            var ticket = new FormsAuthenticationTicket(
                                                     1,
                                                     userData.UserName,
                                                     DateTime.Now,
                                                     DateTime.Now.AddMinutes(FormsAuthentication.Timeout.Minutes),
                                                     false,
                                                     cookieData,
                                                     FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                   
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
          
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut(UserData userData)
        {
            FormsAuthentication.SignOut();
        }

        public void UpdateUserData(UserData userData, bool createPersistentCookie)
        {
            var cookie = FormsAuthentication.GetAuthCookie(userData.UserName, createPersistentCookie);
            var cookieData = JsonConvert.SerializeObject(userData);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket != null)
            {
                var updatedticket = new FormsAuthenticationTicket(ticket.Version,
                    ticket.Name,
                    ticket.IssueDate,
                    ticket.Expiration,
                    false,
                    cookieData,
                    ticket.CookiePath);
                cookie.Value = FormsAuthentication.Encrypt(updatedticket);
            }

            HttpContext.Current.Response.Cookies.Set(cookie);
        }
    }
}