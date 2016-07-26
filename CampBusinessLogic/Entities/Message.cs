using System;

namespace CampDataAccess.Entities
{
    public class Message : BaseEntity
    {
        public UserProfile Author { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}