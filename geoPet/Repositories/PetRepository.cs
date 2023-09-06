namespace geoPet.Repositories
{
    public class PetRepository
    {
        private readonly GeoPetContext _context;

        public PetRepository(GeoPetContext context)
        {
            _context = context;
        }
    }
}
