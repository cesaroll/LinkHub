using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Base;
using BOL;

namespace BLL.Entities
{
    public class CategoryBs : BaseBs<T_Category>
    {
        public virtual new IEnumerable<T_Category> GetAll()
        {
            return base.GetAll().OrderBy(x => x.CategoryName).ToList();
        }
        public virtual IEnumerable<T_Category> GetAll(int page, int pageSize, string sortBy, string sortOrder)
        {
            var items = base.GetAll();

            sortBy = String.IsNullOrEmpty(sortBy) ? String.Empty : sortBy.ToUpper().Trim();

            bool asc = !(!String.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper().Equals("DESC"));


            switch (sortBy)
            {

                case "DESCRIPTION":
                    items = (asc) ? items.OrderBy(x => x.CategoryDesc) : items.OrderByDescending(x => x.CategoryDesc);
                    break;
                default:
                    items = (asc) ? items.OrderBy(x => x.CategoryName) : items.OrderByDescending(x => x.CategoryName);
                    break;
            }

            return items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }

        public virtual long GetAllCount()
        {
            return base.GetAll().LongCount();
        }
            
    }
}