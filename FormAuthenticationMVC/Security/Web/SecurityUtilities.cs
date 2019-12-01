using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FormAuthenticationMVC.Security.Web
{
    public class SecurityUtilities
    {
        public Identity FormsAuthTicketToIdentity(FormsAuthenticationTicket ticket)
        {
            var identity = new Identity
            {
                Id = SetId(ticket),
                Firstname = SetFirstname(ticket),
                Lastname = SetLastname(ticket),
                Email = SetEmail(ticket),
                Password = SetPassword(ticket),
                Roles = SetRoles(ticket),
                Status = SetStatus(ticket),
                AuthenticationType = SetAuthType(),
                IsAuthenticated = SetIsAuthenticated()
            };
            return identity;
        }
        private bool SetIsAuthenticated()
        {
            return true;
        }
        private string SetAuthType()
        {
            return "Forms";
        }
        private bool SetStatus(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            var status = data[6];
            if (status == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string[] SetRoles(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            string[] roles = data[5].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return roles;
        }
        private string SetPassword(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[4];
        }
        private string SetEmail(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[3];
        }
        private string SetLastname(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[2];
        }
        private string SetFirstname(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[1];
        }
        private int SetId(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[0] == null ? 0 : Convert.ToInt32(data[0]);
        }
    }
}