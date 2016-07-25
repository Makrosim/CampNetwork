﻿using System.Collections.Generic;
using CampBusinessLogic.EF;
using CampBusinessLogic.Entities;
using System.Data.Entity;

namespace CampBusinessLogic.Repositories
{
    class MessagesRepository
    {
        private AppContext db;

        public MessagesRepository(AppContext db)
        {
            this.db = db;
        }

        public IEnumerable<Message> GetMessagesList()
        {
            return db.Messages;
        }

        public Message GetMessage(int id)
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