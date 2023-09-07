using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Pet? Pet { get; set; }
    }
}
