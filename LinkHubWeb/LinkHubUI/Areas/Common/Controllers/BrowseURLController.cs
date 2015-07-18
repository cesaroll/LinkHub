using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.Areas;
using BOL;

namespace LinkHubUI.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class BrowseURLController : BaseCommonController
    {

        // GET: Common/BrowseURL
        public ActionResult Index(string sortBy, string sortOrder, string page)
        {
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.TotalPages = (int)Math.Ceiling(AreaBs.UrlBs.GetAllApprovedCount() / (double)PageSize);

            int pageNumber = int.Parse(page ?? "1");
            ViewBag.Page = pageNumber;

            var urls = AreaBs.UrlBs.GetAllApproved(pageNumber, PageSize, sortBy, sortOrder);

            return View(urls);
        }
    }
}