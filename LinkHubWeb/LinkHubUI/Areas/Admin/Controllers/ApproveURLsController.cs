using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Util;

namespace LinkHubUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ApproveURLsController : BaseAdminController
    {
        // GET: Admin/ApproveURLs
        public ActionResult Index(string status)
        {
            status = status ?? "P";
            ViewBag.Status = status;

            var urls = AreaBs.UrlBs.GetAllByStatus(status, 1, 100, null, null);

            return View(urls);
        }

        public ActionResult Approve(int id)
        {
            IResponse response = AreaBs.UrlBs.Approve(id);
            TempData["Resp"] = response;

            return RedirectToAction("Index");
        }

        public ActionResult Reject(int id)
        {
            IResponse response = AreaBs.UrlBs.Reject(id);
            TempData["Resp"] = response;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ApproveOrRejectAll(List<int> ids, string status, string currentStatusTab)
        {
            IResponse resp = AreaBs.UrlBs.ApproveOrReject(ids, status);
            TempData["Resp"] = resp;

            var urls = AreaBs.UrlBs.GetAllByStatus(currentStatusTab, 1, 100, null, null);
            return PartialView("pvApproveURLs", urls);
        }

    }
}