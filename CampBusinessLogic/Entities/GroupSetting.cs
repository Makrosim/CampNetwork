﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampBusinessLogic.Entities
{
    public class GroupSetting : BaseEntity
    {
        [Key]
        [ForeignKey("Group")]
        public new int Id { get; set; }
        public Group Group { get; set; }
    }
}
