using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBusinessLogic.DTO
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public bool IsCreator { get; set; }
        public string Name { get; set; }
        public int MembersCount { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public string ShortDescription { get; set; }
    }
}
