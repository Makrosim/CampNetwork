using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CampBusinessLogic.DTO
{
    public class CampPlaceDTO
    {
        public enum Rates
        {
            Ужасно = 1,
            Плохо = 2,
            Средне = 3,
            Хорошо = 4,
            Отлично = 5
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string LocationX { get; set; } // Координаты
        public string LocationY { get; set; } // Координаты
        [JsonConverter(typeof(StringEnumConverter))]
        public Rates Purity { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Rates Crowdy { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Rates Approachability { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Rates Comfortableness { get; set; }
        public string ShortDescription { get; set; }
        public int PostsCount { get; set; }
    }
}
