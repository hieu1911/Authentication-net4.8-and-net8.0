using Microsoft.AspNet.SignalR.Client.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace App1.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var userId = 1;
            HttpCookie _c = new HttpCookie("CookieAuthen",
                FormsAuthentication.Encrypt(
                    new FormsAuthenticationTicket(1, "linhdh", 
                    DateTime.Now, DateTime.Now.AddSeconds(
                        FormsAuthentication.Timeout.TotalSeconds), true, "1")));
            HttpContext.Response.Cookies.Add(_c);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}