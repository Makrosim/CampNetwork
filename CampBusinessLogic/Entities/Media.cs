namespace CampBusinessLogic.Entities
{
    public class Media : BaseEntity
    {
        public string Type { get; set; } // Add Enum
        public byte[] Bytes { get; set; }
    }
}
