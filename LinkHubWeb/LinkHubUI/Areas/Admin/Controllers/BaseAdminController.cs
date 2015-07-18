using System.Web.Mvc;
using BLL.Areas;

namespace LinkHubUI.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        protected const int PageSize = 10;

        private IAreaBs _areaBs = null;
        protected IAreaBs AreaBs
        {
            get { return _areaBs ?? (_areaBs = new AdminAreaBs()); }
            
        }
        
    }
}