namespace geoPet.Entities
{
    public class PositionRequest
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? DateTime { get; set; }
        public int PetId { get; set; }

    }
}
