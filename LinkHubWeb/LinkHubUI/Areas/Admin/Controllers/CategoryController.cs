using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.Areas;
using BOL;

namespace LinkHubUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class CategoryController : BaseAdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(T_Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AreaBs.CategoryBs.Insert(category);
                    TempData["Msg"] = "Created Succesfully";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["Msg"] = "Create Failed : " + e.Message;
                    return RedirectToAction("Index");
                }
                
            }
            else
            {
                return View("Index");
            }

        }
    }
}