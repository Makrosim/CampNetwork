using System;
using System.Collections.Generic;


namespace CampDataAccess.Entities
{
    public class Post : BaseEntity
    {
        public Post()
        {
            MediaAttachments = new List<Media>();
            Messages = new List<Message>();
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<Media> MediaAttachments { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public virtual CampPlace CampPlace { get; set; }
    }
}
