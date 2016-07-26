using System.Collections.Generic;
using CampDataAccess.EF;
using CampDataAccess.Entities;
using System.Data.Entity;
using CampDataAccess.Interfaces;

namespace CampDataAccess.Repositories
{
    class MessagesRepository : IRepository<Message>
    {
        private AppContext db;

        public MessagesRepository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<Message> GetAll()
        {
            return db.Messages;
        }

        public Message Get(int id)
        {
            return db.Messages.Find(id);
        }

        public void Create(Message mes)
        {
            db.Messages.Add(mes);
        }

        public void Update(Message mes)
        {
            db.Entry(mes).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Message mes = db.Messages.Find(id);
            if (mes != null)
                db.Messages.Remove(mes);
        }
    }
}