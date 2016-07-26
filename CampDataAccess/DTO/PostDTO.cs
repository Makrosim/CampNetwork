using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBusinessLogic.DTO
{
    public class PostDTO
    {
        public string Text { get; set; }
        public ICollection<MessageDTO> Messages { get; set; }

        public string CampPlace { get; set; }
    }
}
