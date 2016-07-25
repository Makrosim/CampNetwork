namespace CampBusinessLogic.Entities
{
    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDateDay { get; set; }
        public string BirthDateMounth { get; set; }
        public string BirthDateYear { get; set; }
        public string Adress { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }
        public string AdditionalInformation { get; set; }

        public virtual User User { get; set; }
    }
}
