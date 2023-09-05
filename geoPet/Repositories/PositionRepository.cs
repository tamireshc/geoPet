using geoPet.Entities;
using geoPet.Models;

namespace geoPet.Repositories
{
    public class PositionRepository
    {
        private readonly GeoPetContext _context;
        public PositionRepository(GeoPetContext context)
        {
            _context = context;
        }

        public void PostPosition(PositionRequest request)
        {
            var position = new Position();
            position.Latitude = request.Latitude;
            position.Longitude = request.Longitude;
            position.DateTime = request.DateTime;
            position.PetId = request.PetId;
            _context.Positions.Add(position);
            _context.SaveChanges();
        }
    }
}
