using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using BOL;

namespace DAL
{
    public class Repository<T> where T : class
    {
        protected virtual LinkHubEntities Db { get; set; }

        public Repository()
        {
            Db = new LinkHubEntities();
        }

        public virtual IQueryable<T> GetAll()
        {
            return Db.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return Db.Set<T>().Find(id);
        }

        public virtual void Insert(T item)
        {
            Db.Set<T>().Add(item);
            Save();
        }

        public virtual void Delete(int id)
        {
            var item = Db.Set<T>().Find(id);
            if (item != null)
            {
                Db.Set<T>().Remove(item);
                Save();
            }
        }

        public virtual void Update(T item)
        {
            Db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public virtual void Save()
        {
            try
            {
                Db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                var errorMsgs = dbEx.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                string msg = string.Join("\n", errorMsgs);

                throw new DbEntityValidationException(msg, dbEx);
            }
        }

    }
}