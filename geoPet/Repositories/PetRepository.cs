using geoPet.Entities;
using geoPet.Models;
using Microsoft.EntityFrameworkCore;

namespace geoPet.Repositories
{
    public class PetRepository
    {
        private readonly GeoPetContext _context;

        public PetRepository(GeoPetContext context)
        {
            _context = context;
        }

        public void post(Pet pet)
        {
            _context.Pets.Add(pet);
            _context.SaveChanges();
        }

        public Pet findById(int id)
        {
            return _context.Pets.FirstOrDefault(p => p.PetId == id);
        }

        public List<Pet> findAll()
        {
            return _context.Pets.ToList();
        }

        public void delete(Pet pet)
        {
            _context.Pets.Remove(pet);
            _context.SaveChanges();
        }

        public void update(Pet pet)
        {
            _context.Pets.Update(pet);
            _context.SaveChanges();
        }
    }
}
