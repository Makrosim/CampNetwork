using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CampBusinessLogic.DTO
{
    public class CampPlaceDTO
    {
        public enum Rates
        {
            VeryBad = 1,
            Bad = 2,
            Medium = 3,
            Good = 4,
            VeryGood = 5
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
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
