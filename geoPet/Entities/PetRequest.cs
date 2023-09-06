using geoPet.Entities.Enuns;
using geoPet.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace geoPet.Entities
{
    public class PetRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Size { get; set; }
        public string Breed { get; set; }
        public int OwerId { get; set; }
    }
}
