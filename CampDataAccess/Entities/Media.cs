using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampDataAccess.Entities
{
    public class Media : BaseEntity
    {
        public UserProfile UserProfile { get; set; }
        public string Type { get; set; } // Add Enum
        public string Path { get; set; }
    }
}
