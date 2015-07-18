using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Util;
using BOL;
using LinkHubUI.Areas.Admin.Controllers;

namespace LinkHubUI.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class RegisterController : BaseAdminController
    {
        // GET: Security/Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(T_User user)
        {
            if (ModelState.IsValid)
            {
                IResponse response = AreaBs.UserBs.Insert(user);
                TempData["Resp"] = response;

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index");
            }
        }

    }
}