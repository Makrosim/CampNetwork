using System.Collections.Generic;
using CampDataAccess.EF;
using CampDataAccess.Entities;
using System.Data.Entity;
using CampDataAccess.Interfaces;

namespace CampDataAccess.Repositories
{
    class GroupsRepository : IRepository<Group>
    {
        private AppContext db;

        public GroupsRepository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<Group> GetAll()
        {
            return db.Groups;
        }

        public Group Get(int id)
        {
            return db.Groups.Find(id);
        }

        public void Create(Group mes)
        {
            db.Groups.Add(mes);
        }

        public void Update(Group mes)
        {
            db.Entry(mes).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Group mes = db.Groups.Find(id);
            if (mes != null)
                db.Groups.Remove(mes);
        }
    }
}