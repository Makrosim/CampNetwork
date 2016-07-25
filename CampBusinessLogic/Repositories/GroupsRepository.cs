using System.Collections.Generic;
using CampBusinessLogic.EF;
using CampBusinessLogic.Entities;
using System.Data.Entity;

namespace CampBusinessLogic.Repositories
{
    class GroupsRepository
    {
        private AppContext db;

        public GroupsRepository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<Group> GetGroupsList()
        {
            return db.Groups;
        }

        public Group GetGroup(int id)
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