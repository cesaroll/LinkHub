using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.Areas;

namespace LinkHubUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ListCategoryController : BaseAdminController
    {
        // GET: Common/BrowseURL
        public ActionResult Index(string sortBy, string sortOrder, string page)
        {
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.TotalPages = (int)Math.Ceiling(AreaBs.CategoryBs.GetAllCount() / (double)PageSize);

            int pageNumber = int.Parse(page ?? "1");
            ViewBag.Page = pageNumber;

            var items = AreaBs.CategoryBs.GetAll(pageNumber, PageSize, sortBy, sortOrder);

            return View(items);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                AreaBs.CategoryBs.Delete(id);
                TempData["Msg"] = "Deleted Succesfully";
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                TempData["Msg"] = "Delete Failed : " + e.Message;
                return RedirectToAction("Index");
            }
        }

    }
}