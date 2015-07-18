using BLL.Entities;

namespace BLL.Areas
{
    public class BaseAreaBs : IAreaBs
    {
        private UrlBs _urlBs = null;
        private CategoryBs _categoryBs = null;
        private UserBs _userBs = null;

        public virtual UrlBs UrlBs
        {
            get { return _urlBs ?? (_urlBs = new UrlBs()); }
            set { _urlBs = value; }
        }

        public virtual CategoryBs CategoryBs
        {
            get { return _categoryBs ?? (_categoryBs = new CategoryBs()); }
            set { _categoryBs = value; }
        }

        public virtual UserBs UserBs
        {
            get { return _userBs ?? (_userBs = new UserBs()); }
            set { _userBs = value; }
        }

    }
}