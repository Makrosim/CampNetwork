using System.Collections.Generic;
using CampBusinessLogic.EF;
using CampBusinessLogic.Entities;
using System.Data.Entity;

namespace CampBusinessLogic.Repositories
{
    class CampPlacesRepository
    {
        private AppContext db;

        public CampPlacesRepository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<CampPlace> GetCampPlacesList()
        {
            return db.CampPlaces;
        }

        public CampPlace GetCampPlace(int id)
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