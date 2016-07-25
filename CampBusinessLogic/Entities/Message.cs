using System;

namespace CampDataAccess.Entities
{
    public class Message : BaseEntity
    {
        public User Author { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}