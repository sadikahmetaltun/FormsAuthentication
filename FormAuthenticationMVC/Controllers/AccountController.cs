using FormAuthenticationMVC.Models;
using FormAuthenticationMVC.Models.ViewModels;
using FormAuthenticationMVC.Security.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormAuthenticationMVC.Controllers
{
    public class AccountController : Controller
    {
        DemoDBEntities demoDB = new DemoDBEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                Users user = demoDB.Users.Where(w => w.Email == loginViewModel.Email && w.Password == loginViewModel.Password).FirstOrDefault();
                if (user != null)
                {
                    AuthenticationHelper.CreateAuthCookie(user.Id, user.Firstname, user.Lastname, user.Email, user.Password, user.UserRoles.Select(s => s.Roles.Rolename).ToArray(), user.Status);
                    return Redirect("~/");
                }
                return Redirect("~/Account/Login");
            }
            catch (Exception ex)
            {
                return Redirect("~/Account/Login");
            }
        }

        public ActionResult Logout()
        {
            AuthenticationHelper.Logout();
            return Redirect("~/Account/Login");
        }
    }
}