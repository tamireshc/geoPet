using geoPet.Entities.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace geoPet.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Size Size { get; set; }
        public string Breed { get; set; }
        public int OwerId { get; set; }
        [JsonIgnore]
        public Ower? Ower { get; set; }
        public ICollection<Position>? Positions { get; set; }

    }
}
