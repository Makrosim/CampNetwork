using System;
using System.Collections.Generic;


namespace CampDataAccess.Entities
{
    public class Post : BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
        public ICollection<int> MediaAttachments { get; set; }
        public virtual ICollection<int> Messages { get; set; }

        public int? CampPlaceID { get; set; }
        public virtual CampPlace CampPlace { get; set; }
    }
}
