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
    public class ListUserController : BaseAdminController
    {
        // GET: Common/BrowseURL
        public ActionResult Index(string sortBy, string sortOrder, string page)
        {
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.TotalPages = (int)Math.Ceiling(AreaBs.UserBs.GetAllCount() / (double)PageSize);

            int pageNumber = int.Parse(page ?? "1");
            ViewBag.Page = pageNumber;

            var users = AreaBs.UserBs.GetAll(pageNumber, PageSize, sortBy, sortOrder);

            return View(users);
        }
    }
}