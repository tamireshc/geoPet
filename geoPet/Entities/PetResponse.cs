using geoPet.Entities.Enuns;

namespace geoPet.Entities
{
    public class PetResponse
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Size Size { get; set; }
        public string Breed { get; set; }
        public int OwerId { get; set; }
    }
}
