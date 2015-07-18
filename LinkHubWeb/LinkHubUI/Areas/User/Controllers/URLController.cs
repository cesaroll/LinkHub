using System.EnterpriseServices;
using System.Web.Mvc;
using BLL.Areas;
using BOL;

namespace LinkHubUI.Areas.User.Controllers
{
    [Authorize(Roles = "A,U")]
    public class URLController : Controller
    {
        protected IAreaBs AreaBs { get; set; }
        protected SelectList CategoryList
        {
            get { return new SelectList(AreaBs.CategoryBs.GetAll(), "CategoryId", "CategoryName"); }
        }

        protected SelectList UserList
        {
            get { return new SelectList(AreaBs.UserBs.GetAll(), "UserId", "UserEmail"); }
        }

        public URLController()
        {
            AreaBs = new UserAreaBs();
        }

        // GET: User/URL
        public ActionResult Index()
        {
            ViewBag.CategoryId = CategoryList;
            ViewBag.UserId = UserList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(T_Url myUrl)
        {
            myUrl.IsApproved = "P";
            myUrl.UserId = AreaBs.UserBs.GetUserId(User.Identity.Name);

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = CategoryList;
                ViewBag.UserId = UserList;
                return View("Index");
            }
            
            TempData["Resp"] = AreaBs.UrlBs.Insert(myUrl);

            return RedirectToAction("Index");
            

        }

    }
}