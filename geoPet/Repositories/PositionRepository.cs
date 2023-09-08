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

        public void post(Position position)
        {
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

        public Position lastPositionOfPet(int id)
        {
            var position = _context.Positions.Where(p => p.PetId == id).ToList();
            return position.LastOrDefault();
        }

        public Position firstPositionOfPet(int id)
        {
            var position = _context.Positions.Where(p => p.PetId == id).ToList();
            return position.FirstOrDefault();
        }

        public int countNumberPositionsOdPet(int id)
        {
            var position = _context.Positions.Where(p => p.PetId == id).ToList();
            return position.Count;
        }
    }
}
