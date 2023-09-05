using geoPet.Models;

namespace geoPet.Entities
{
    public class OwerResponse
    {
        public int OwerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CEP { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
