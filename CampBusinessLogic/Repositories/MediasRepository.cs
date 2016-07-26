using System.Collections.Generic;
using CampDataAccess.EF;
using CampDataAccess.Entities;
using System.Data.Entity;
using CampDataAccess.Interfaces;

namespace CampDataAccess.Repositories
{
    class MediasRepository : IRepository<Media>
    {
        private AppContext db;

        public MediasRepository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<Media> GetAll()
        {
            return db.Medias;
        }

        public Media Get(int id)
        {
            return db.Medias.Find(id);
        }

        public void Create(Media cp)
        {
            db.CampPlaces.Add(cp);
        }

        public void Update(Media cp)
        {
            db.Entry(cp).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Media cp = db.Medias.Find(id);
            if (cp != null)
                db.CampPlaces.Remove(cp);
        }
    }
}