using System.Collections.Generic;
using CampDataAccess.EF;
using CampDataAccess.Entities;
using System.Data.Entity;
using CampDataAccess.Interfaces;

namespace CampDataAccess.Repositories
{
    class CampPlacesRepository : IRepository<CampPlace>
    {
        private AppContext db;

        public CampPlacesRepository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<CampPlace> GetAll()
        {
            return db.CampPlaces;
        }

        public CampPlace Get(int id)
        {
            return db.CampPlaces.Find(id);
        }

        public void Create(CampPlace cp)
        {
            db.CampPlaces.Add(cp);
        }

        public void Update(CampPlace cp)
        {
            db.Entry(cp).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            CampPlace cp = db.CampPlaces.Find(id);
            if (cp != null)
                db.CampPlaces.Remove(cp);
        }
    }
}