using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampDataAccess.Entities
{
    public class UserProfile
    {
        public UserProfile()
        {
            CampPlaces = new List<CampPlace>();
            Groups = new List<Group>();
            Dialogs = new List<Dialog>();
            Friends = new List<UserProfile>();
            Medias = new List<Media>();
            Messages = new List<Message>();
        }

        [Key, ForeignKey("User")]
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

        public virtual Media Avatar { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
        public virtual ICollection<CampPlace> CampPlaces { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<UserProfile> Friends { get; set; }
        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public virtual UserSetting UserSettings { get; set; }
        public virtual User User { get; set; }
    }
}
