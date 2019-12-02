using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace FormAuthenticationMVC.Security.Web
{
    public class AuthenticationHelper
    {
        public static void CreateAuthCookie(int id, string firstname, string lastname, string email, string password, string[] roles, bool status)
        {
            var authTicket = new FormsAuthenticationTicket(1, firstname, DateTime.Now, DateTime.Now.AddDays(1), false, CreateAuthTags(id, firstname, lastname, email, password, roles, status));
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }
        private static string CreateAuthTags(int id, string firstname, string lastname, string email, string password, string[] roles, bool status)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(id);
            sb.Append("|");

            sb.Append(firstname);
            sb.Append("|");

            sb.Append(lastname);
            sb.Append("|");

            sb.Append(email);
            sb.Append("|");

            sb.Append(password);
            sb.Append("|");

            for (int i = 0; i < roles.Length; i++)
            {
                sb.Append(roles[i]);
                if (i < roles.Length - 1)
                {
                    sb.Append(',');
                }
            }
            sb.Append("|");
            sb.Append(status);
            return sb.ToString();

        }
        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}