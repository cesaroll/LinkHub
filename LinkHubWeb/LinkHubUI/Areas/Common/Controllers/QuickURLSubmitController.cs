using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;

namespace LinkHubUI.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class QuickURLSubmitController : BaseCommonController
    {
        protected SelectList CategoryList
        {
            get { return new SelectList(AreaBs.CategoryBs.GetAll(), "CategoryId", "CategoryName"); }
        }

        // GET: Common/QuickURLSubmit
        public ActionResult Index()
        {
            ViewBag.CategoryId = CategoryList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuickURLSubmitModel quickUrl)
        {
            ModelState.Remove("MyUser.Password");
            ModelState.Remove("MyUser.ConfirmPassword");
            ModelState.Remove("MyUrl.UrlDesc");

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = CategoryList;
                return View("Index");
            }


            TempData["Resp"] = AreaBs.UrlBs.InsertQuickURL(quickUrl);
            return RedirectToAction("Index");
        }
    }
}