using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampDataAccess.Entities
{
    public class UserProfile
    {
        public UserProfile()
        {
            CampPlacesId = new List<int?>();
            CampPlaces = new List<CampPlace>();
            Groups = new List<Group>();
            Dialogs = new List<Dialog>();
            Friends = new List<User>();
            Media = new List<int>();
        }

        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDateDay { get; set; }
        public string BirthDateMounth { get; set; }
        public string BirthDateYear { get; set; }
        public string Address { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }
        public string AdditionalInformation { get; set; }

        public virtual int Avatar { get; set; }
        public virtual ICollection<int> Media { get; set; }
        public virtual ICollection<int?> CampPlacesId { get; set; }
        public virtual ICollection<CampPlace> CampPlaces { get; set; }
        public ICollection<Group> Groups { get; set; }
        public UserSetting UserSettings { get; set; }
        public ICollection<User> Friends { get; set; }
        public ICollection<Dialog> Dialogs { get; set; }
        public ICollection<Message> Messages { get; set; }

        public virtual User User { get; set; }
    }
}
