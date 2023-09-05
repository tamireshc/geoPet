using geoPet.Entities;
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
        public string PostOwer(OwerRequest request)
        {
            try
            {
                Ower ower = new Ower();
                ower.Name = request.Name;
                ower.Email = request.Email;
                ower.CEP = request.CEP;
                ower.Password = new Hash(SHA512.Create()).CriptografarSenha(request.Password);

                _context.Owers.Add(ower);
                _context.SaveChanges();
                return null;

            }
            catch (DbUpdateException e) {
                return "Email already exists";
            }
        }

        public List<Ower> findAll()
        {
           return _context.Owers.ToList();
        }

        public Ower findById(int id)
        {
            return _context.Owers.FirstOrDefault(x=>x.OwerId == id);
        }


    }
}
