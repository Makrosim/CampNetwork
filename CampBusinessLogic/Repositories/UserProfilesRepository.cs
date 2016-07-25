using System.Collections.Generic;
using CampDataAccess.EF;
using CampDataAccess.Entities;
using System.Data.Entity;

namespace CampDataAccess.Repositories
{
    class UserProfilesRepository
    {
        private AppContext db;

        public UserProfilesRepository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserProfile> GetUserProfilesList()
        {
            return db.UserProfiles;
        }

        public UserProfile GetUserProfile(int id)
        {
            return db.UserProfiles.Find(id);
        }

        public void Create(UserProfile up)
        {
            db.UserProfiles.Add(up);
        }

        public void Update(UserProfile up)
        {
            db.Entry(up).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            UserProfile up = db.UserProfiles.Find(id);
            if (up != null)
                db.CampPlaces.Remove(up);
        }
    }
}
