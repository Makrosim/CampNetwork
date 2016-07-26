namespace CampBusinessLogic.DTO
{
    class CampPlaceDTO
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
        public string LocationX { get; set; } // Координаты
        public string LocationY { get; set; } // Координаты
        public Rates Purity { get; set; }
        public Rates Crowdy { get; set; }
        public Rates Appriachibility { get; set; }
        public Rates Comfortableness { get; set; }
        public string ShortDescription { get; set; }
    }
}
