using BLL.Entities;
using BLL.Util;

namespace BLL.Areas
{
    public interface IAreaBs
    {
        UrlBs UrlBs { get; }
        CategoryBs CategoryBs { get; }
        UserBs UserBs { get; }
    }
}