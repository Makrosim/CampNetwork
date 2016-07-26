using System.Collections.Generic;

namespace CampDataAccess.Entities
{
    public class CampPlace : BaseEntity
    {
        public enum Rates
        {
            VeryBad = 1,
            Bad = 2,
            Medium = 3,
            Good = 4,
            VeryGood = 5
        }

        public string Name { get; set; }
        public virtual string LocationX { get; set; } // Координаты
        public virtual string LocationY { get; set; } // Координаты
        public Rates Purity { get; set; }
        public Rates Crowdy { get; set; }
        public Rates Appriachibility { get; set; }
        public Rates Comfortableness { get; set; }
        public string ShortDescription { get; set; }

        public virtual ICollection<int?> PostsId { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual User User { get; set; }
    }
}
