using geoPet.Entities;
using geoPet.Exceptions;
using geoPet.Models;
using geoPet.Utils;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace geoPet.Repositories
{
    public class OwerRepository
    {
        private GeoPetContext _context;
        public OwerRepository(GeoPetContext context)
        {
            _context = context;
        }
        public string PostOwer(Ower ower)
        {
            _context.Owers.Add(ower);
            _context.SaveChanges();
            return null;
        }

        public List<Ower> findAll()
        {
            return _context.Owers.ToList();
        }

        public Ower findById(int id)
        {
            return _context.Owers.FirstOrDefault(x => x.OwerId == id);
        }

        public void delete(int id)
        {
            _context.Owers.Remove(findById(id));
            _context.SaveChanges();
        }

        public Ower findByEmail(String email)
        {
            return _context.Owers.FirstOrDefault(x => x.Email == email);
        }

        public void update(Ower ower)
        {
            _context.Owers.Update(ower);
            _context.SaveChanges();
        }


    }
}
