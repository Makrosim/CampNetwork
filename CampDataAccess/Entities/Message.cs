using System;

namespace CampDataAccess.Entities
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        public virtual Post Post { get; set; }
    }
}