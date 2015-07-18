using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Base;
using BLL.Util;
using BOL;

namespace BLL
{
    public class UserBs : BaseBs<T_User>
    {
        public static UserBs GetInstance()
        {
            return new UserBs();
        }

        public virtual new IEnumerable<T_User> GetAll()
        {
            return base.GetAll().OrderBy(x => x.UserEmail).ToList();
        }
        public virtual IEnumerable<T_User> GetAll(int page, int pageSize, string sortBy, string sortOrder)
        {
            var items = base.GetAll();

            sortBy = String.IsNullOrEmpty(sortBy) ? String.Empty : sortBy.ToUpper().Trim();

            bool asc = !(!String.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper().Equals("DESC"));


            switch (sortBy)
            {

                case "ROLE":
                    items = (asc) ? items.OrderBy(x => x.Role) : items.OrderByDescending(x => x.Role);
                    break;
                default:
                    items = (asc) ? items.OrderBy(x => x.UserEmail) : items.OrderByDescending(x => x.UserEmail);
                    break;
            }

            return items.Skip((page - 1) * pageSize).Take(pageSize).ToList();



        }

        public virtual long GetAllCount()
        {
            return base.GetAll().LongCount();
        }

        public virtual int GetUserId(string userName)
        {
            T_User user = UserBs.GetInstance().GetAll().FirstOrDefault(x => x.UserEmail == userName);
            if (user != null)
            {
                return user.UserId;
            }

            return -1;
        }

        public override IResponse Insert(T_User item)
        {
            item.Role = "U";
            IResponse retResp = base.Insert(item);

            if (retResp.IsError)
            {
                Response response = new Response(true, new Msg("User Creation Failed"));

                response.Include(retResp);

                return response;
            }

            return new Response(false, new Msg("User Created Successfully"));
        }

        internal bool ValidateUser(string username, string password)
        {
            return base.GetAll().Count(x => x.UserEmail == username && x.Password == password) > 0;
        }

        internal string GetUserRoles(string username)
        {
            string role = string.Empty;

            try
            {
                T_User user = base.GetAll().FirstOrDefault(x => x.UserEmail == username);

                role = (user != null) ? user.Role : string.Empty;
            }
            catch (Exception)
            {
            }

            return role;
        }
    }
}