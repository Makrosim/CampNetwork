namespace CampDataAccess.Entities
{
    public class Media : BaseEntity
    {
        public string Type { get; set; } // Add Enum
        public string Path { get; set; }
    }
}
