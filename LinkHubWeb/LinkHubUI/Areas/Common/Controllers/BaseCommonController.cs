using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Areas;

namespace LinkHubUI.Areas.Common.Controllers
{
    public class BaseCommonController : Controller
    {
        protected const int PageSize = 10;
        protected IAreaBs AreaBs { get; set; }
        public BaseCommonController()
        {
            AreaBs = new CommonAreaBs();
        }

    }
}