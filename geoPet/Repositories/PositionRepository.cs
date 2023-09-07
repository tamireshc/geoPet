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

        public List<Position> findAll()
        {
            return _context.Positions.ToList();
        }

        public Position findById(int id)
        {
            return _context.Positions.FirstOrDefault(p => p.PositionId == id);
        }

        public void delete(Position position)
        {
            _context.Positions.Remove(position);
            _context.SaveChanges();
        }

        public void update(Position position)
        {
            _context.Positions.Update(position);
            _context.SaveChanges();
        }
    }
}
