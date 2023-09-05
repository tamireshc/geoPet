using System.ComponentModel.DataAnnotations;

namespace geoPet.Models
{
    public class Ower
    {
        [Key]
        public int OwerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CEP { get; set; }
        public string Password { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
