using System.ComponentModel.DataAnnotations;

namespace geoPet.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime DateTime { get; set; }
        public int PetId { get; set; }
        public Pet? Pet { get; set; }
    }
}
