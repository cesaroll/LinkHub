using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Util;
using BOL;

namespace LinkHubUI.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Security/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(T_User user)
        {
            try
            {
                if (Membership.ValidateUser(user.UserEmail, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserEmail, false);
                    return RedirectToAction("Index", "Home", new {area = "Common"});
                }
                else
                {
                    TempData["Resp"] = new Response(true, new Msg("Login Failed"));
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["Resp"] = new Response(true, new Msg("Login Failed<BR>" + e.Message));
                return RedirectToAction("Index");
            }
           

        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new {area = "Common"});
        }
    }
}