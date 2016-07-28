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
        public int Name { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public string ShortDescription { get; set; }
    }
}
