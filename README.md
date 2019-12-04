# FormsAuthentication
**Asp.Net MVC - Simple Forms Authentication**

In this example, we implemented a role-based input-output system with basic forms authentication in asp.net mvc project.

`FormAuthenticationMVC.Security.Web.AuthenticationHelper.CreateAuthCookie(...) // Create Cookie Class`

`FormAuthenticationMVC.Security.Web.SecurityUtilities(...) // Ticket to Identity Class`

`FormAuthenticationMVC.Filters.SecuredOperationFilter // filter method we intervene before and after the method// `

`Global.asax`

```csharp
        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null)
                {
                    return;
                }

                var encTicket = authCookie.Value;
                if (String.IsNullOrEmpty(encTicket))
                {
                    return;
                }

                var ticket = FormsAuthentication.Decrypt(encTicket); // ticket solve.
                var securityUtilities = new SecurityUtilities();
                var identity = securityUtilities.FormsAuthTicketToIdentity(ticket); // We create identity from solved ticket.
                var principal = new GenericPrincipal(identity, identity.Roles); // Create Principal

                HttpContext.Current.User = principal; // for use on the web side
                Thread.CurrentPrincipal = principal; // for use on the back-end
            }
            catch (Exception)
            {
            }
        }
```
```csharp
    [SecuredOperationFilter(Roles = "Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
```
