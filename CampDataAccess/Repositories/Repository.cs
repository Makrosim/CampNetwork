using System.Collections.Generic;
using CampDataAccess.EF;
using CampDataAccess.Entities;
using System.Data.Entity;
using CampDataAccess.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace CampDataAccess.Repositories
{
    class Repository<T> : IRepository<T> where T : class
    {
        private AppContext db;

        public Repository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>();
        }

        public T Get(int id)
        {
            var entity = db.Set<T>().Find(id);

            if (entity == null)
                throw new Exception("Запрашиваемый ресурс не найден");

            return entity;
        }

        public T Get(string id)
        {
            var entity = db.Set<T>().Find(id);

            if (entity == null)
                throw new Exception("Запрашиваемый ресурс не найден");

            return entity;
        }

        public virtual IEnumerable<T> List()
        {
            return db.Set<T>().AsEnumerable();
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).AsEnumerable();
        }

        public void Create(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = db.Set<T>().Find(id);

            if (entity == null)
                throw new Exception("Запрашиваемый ресурс не найден");

            db.Set<T>().Remove(entity);
            db.SaveChanges();
        }
    }
}