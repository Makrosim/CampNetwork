using System;
using System.Collections.Generic;


namespace CampDataAccess.Entities
{
    public class Post : BaseEntity
    {
        public Post()
        {
            MediaAttachments = new List<int>();
            Messages = new List<Message>();
        }

        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
        public virtual ICollection<int> MediaAttachments { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public virtual CampPlace CampPlace { get; set; }
    }
}
