using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBusinessLogic.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int CampPlaceId { get; set; }
        public string CampPlaceName { get; set; }
    }
}
