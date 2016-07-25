using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampBusinessLogic.Entities
{
    public class UserSetting
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public User User { get; set; }
    }
}
