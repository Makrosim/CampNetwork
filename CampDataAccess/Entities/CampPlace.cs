using System.Collections.Generic;

namespace CampDataAccess.Entities
{
    public class CampPlace : BaseEntity
    {
        public enum Rates
        {
            Ужасно = 1,
            Плохо = 2,
            Средне = 3,
            Хорошо = 4,
            Отлично = 5
        }

        public CampPlace()
        {
            Posts = new List<Post>();
        }

        public string Name { get; set; }
        public virtual string LocationX { get; set; } // Координаты
        public virtual string LocationY { get; set; } // Координаты
        public Rates Purity { get; set; }
        public Rates Crowdy { get; set; }
        public Rates Approachability { get; set; }
        public Rates Comfortableness { get; set; }
        public string ShortDescription { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
