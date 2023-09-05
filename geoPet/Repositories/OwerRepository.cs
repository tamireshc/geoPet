using geoPet.Entities;
using geoPet.Models;

namespace geoPet.Repositories
{
    public class OwerRepository
    {
        private GeoPetContext _context;
        public OwerRepository(GeoPetContext context)
        {
            _context = context;
        }
        public void PostOwer(OwerRequest request)
        {
            Ower ower = new Ower();
            ower.Name = request.Name;
            ower.Email = request.Email;
            ower.CEP = request.CEP;
            ower.Password = request.Password;

            _context.Owers.Add(ower);
            _context.SaveChanges();
        }
    }
}
