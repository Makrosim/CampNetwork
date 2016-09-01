using System.Collections.Generic;
using CampDataAccess.EF;
using CampDataAccess.Entities;
using System.Data.Entity;
using CampDataAccess.Interfaces;
using System;
using System.Threading.Tasks;

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
            return db.Set<T>().Find(id);
        }

        public async Task<T> GetAsync(string id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public T Get(string id)
        {
            return db.Set<T>().Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await db.Set<T>().FindAsync(id);
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
            {
                throw new ArgumentNullException("entity");
            }

            db.Set<T>().Remove(entity);
            db.SaveChanges();
        }
    }
}