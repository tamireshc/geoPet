using geoPet.Entities.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace geoPet.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Size Size { get; set; }
        public string Breed { get; set; }
        [ForeignKey("OwerId")]
        public int OwerId { get; set; }
        public Ower Ower { get; set; }
        public ICollection<Position>? Positions { get; set; }

    }
}
