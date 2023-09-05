using geoPet.Entities;
using geoPet.Repositories;

namespace geoPet.Services
{
    public class PositionService
    {
        private readonly PositionRepository _positionRepository;
        public PositionService(PositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }
        public void PostPosition(PositionRequest request)
        {
            _positionRepository.PostPosition(request);
        }
    }
}
