using System;
using System.Linq;
using BLL.Util;
using DAL;

namespace BLL.Base
{
    public class BaseBs<T> where T : class
    {
        protected virtual Repository<T> Rep { get; set; }

        public BaseBs()
        {
            Rep = new Repository<T>();
        }

        public IQueryable<T> GetAll()
        {
            return Rep.GetAll();
        }

        public virtual T GetById(int id)
        {
            return Rep.GetById(id);
        }

        public virtual IResponse Insert(T item)
        {
            try
            {
                Rep.Insert(item);
            }
            catch (Exception e)
            {
                return new Response(true, new Msg("Create Failed: " + e.Message, MsgType.DbException, e));
            }

            return new Response(false, new Msg("Created", MsgType.Information));
        }

        public virtual void Delete(int id)
        {
            Rep.Delete(id);
        }

        public virtual IResponse Update(T item)
        {
            try
            {
                Rep.Update(item);
            }
            catch (Exception e)
            {
                return new Response(true, new Msg(e.Message, MsgType.DbException, e));
            }

            return new Response(false, new Msg("Updated", MsgType.Information));
        }

    }
}