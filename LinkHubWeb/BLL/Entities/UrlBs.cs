using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using BLL.Base;
using BLL.Util;
using BOL;

namespace BLL.Entities
{
    public class UrlBs : BaseBs<T_Url>
    {
        public static UrlBs New()
        {
            return new UrlBs();
        }

        public virtual IEnumerable<T_Url> GetAllApproved(int page, int pageSize, string sortBy, string sortOrder)
        {
            return GetAllByStatus("A", page, pageSize, sortBy, sortOrder);
        }
        public virtual IEnumerable<T_Url> GetAllByStatus(string status, int page, int pageSize, string sortBy, string sortOrder)
        {

            var urls = base.GetAll().Where(x => x.IsApproved == status);

            sortBy = String.IsNullOrEmpty(sortBy) ? String.Empty : sortBy.ToUpper().Trim();

            bool asc = !(!String.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper().Equals("DESC"));
                

            switch (sortBy)
            {
                
                case "URL":
                    urls = (asc) ? urls.OrderBy(x => x.Url) : urls.OrderByDescending(x => x.Url);
                    break;
                case "DESCRIPTION":
                    urls = (asc) ? urls.OrderBy(x => x.UrlDesc) : urls.OrderByDescending(x => x.UrlDesc);
                    break;
                case "CATEGORY":
                    urls = (asc) ? urls.OrderBy(x => x.T_Category.CategoryName) : urls.OrderByDescending(x => x.T_Category.CategoryName);
                    break;
                default:
                    urls = (asc) ? urls.OrderBy(x => x.UrlTitle) : urls.OrderByDescending(x => x.UrlTitle);
                    break;
            }

            return urls.Skip((page - 1)*pageSize).Take(pageSize).ToList();



        }

        public virtual long GetAllApprovedCount()
        {
            return GetAllByStatusCount("A");
        }

        public virtual long GetAllByStatusCount(string status)
        {
            return base.GetAll().LongCount(x => x.IsApproved == status);
        }

        public virtual bool UrlExist(T_Url url)
        {
            return UrlExist(url.Url);
        }

        public virtual bool UrlExist(string str)
        {
            return base.GetAll().Count(x => x.Url == str) > 0;
        }

        public virtual IResponse Approve(int id)
        {
            var myUrl = GetById(id);
            myUrl.IsApproved = "A";
            IResponse retResp = Update(myUrl);

            if (retResp.IsError)
            {
                Response response = new Response(true, new Msg("Approval Failed"));

                response.Include(retResp);

                return response;
            }
                

            return new Response(false, new Msg("Approved Successfully"));

        }

        public virtual IResponse Reject(int id)
        {
            var myUrl = GetById(id);
            myUrl.IsApproved = "R";
            IResponse retResp = Update(myUrl);

            if (retResp.IsError)
            {
                Response response = new Response(true, new Msg("Rejection Failed"));

                response.Include(retResp);

                return response;
            }


            return new Response(false, new Msg("Rejected Successfully"));

        }


        public IResponse InsertQuickURL(QuickURLSubmitModel quickUrl)
        {
            IResponse Resp;

            using (TransactionScope transaction = new TransactionScope())
            {
                T_User myUser = quickUrl.MyUser;
                myUser.Password = myUser.ConfirmPassword = "123456";

                Resp = UserBs.GetInstance().Insert(myUser);

                if (Resp.IsError)
                    return Resp;

                T_Url myUrl = quickUrl.MyUrl;
                myUrl.UserId = myUser.UserId;
                myUrl.UrlDesc = myUrl.UrlTitle;
                myUrl.IsApproved = "P";

                Resp = base.Insert(myUrl);
                if (Resp.IsError)
                    return Resp;

                transaction.Complete();
            }

            return Resp;
        }

        public IResponse ApproveOrReject(List<int> ids, string status)
        {
            if (status != "A" && status != "R")
                return new Response(true, new Msg("Invalid status: [" + status + "]"));

            using (TransactionScope transaction = new TransactionScope())
            {
                foreach (var item in ids)
                {
                    var myUrl = GetById(item);
                    myUrl.IsApproved = status;
                    IResponse retResp = Update(myUrl);

                    if (retResp.IsError)
                    {
                        Response response = new Response(true, new Msg("Operation Failed"));
                        response.Include(retResp);
                        return response;
                    }
                }

                transaction.Complete();
            }

            return new Response(false, new Msg("Operation Successfull"));

        }
    }
}