using geoPet.Entities;
using geoPet.Repositories;

namespace geoPet.Services
{
    public class OwerService
    {
        private OwerRepository _owerRepository;
        public OwerService(OwerRepository repository)
        {
            this._owerRepository = repository;
        }

        public void PostOwer(OwerRequest request)
        {
            _owerRepository.PostOwer(request);
        }
    }
}

