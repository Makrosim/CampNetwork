﻿using System.Collections.Generic;

namespace CampDataAccess.Entities
{
    public class Group : BaseEntity
    {
        public Group()
        {
            Members = new List<User>();
            Posts = new List<Post>();
        }

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<User> Members { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public GroupSetting GroupSetting { get; set; }
    }
}
