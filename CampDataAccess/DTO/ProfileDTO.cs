using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBusinessLogic.DTO
{
    public class ProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }
        public string BirthDateDay { get; set; }
        public string BirthDateMounth { get; set; }
        public string BirthDateYear { get; set; }
        public string AdditionalInformation { get; set; }
        public Stream Image { get; set; }
    }
}
