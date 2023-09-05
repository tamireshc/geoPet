using geoPet.Entities;
using geoPet.Models;
using Microsoft.EntityFrameworkCore;

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
                ower.Password = request.Password;

                _context.Owers.Add(ower);
                _context.SaveChanges();
                return null;

            }
            catch (DbUpdateException e) {
                return "Email already exists";
            }

        }
    }
}
