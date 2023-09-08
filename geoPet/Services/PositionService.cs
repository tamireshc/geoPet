using geoPet.Entities;
using geoPet.Exceptions;
using geoPet.Models;
using geoPet.Repositories;
using Microsoft.EntityFrameworkCore;

namespace geoPet.Services
{
    public class PositionService
    {
        private readonly PositionRepository _positionRepository;
        private readonly PetService _petService;

        public PositionService(PositionRepository positionRepository, PetService petService)
        {
            _positionRepository = positionRepository;
            _petService = petService;
        }

        public void post(PositionRequest request)
        {
            _petService.findById(request.PetId);

            var position = new Position();
            position.Latitude = request.Latitude;
            position.Longitude = request.Longitude;
            position.DateTime = DateTime.Now;
            position.PetId = request.PetId;
     
            _positionRepository.post(position);

            if(_positionRepository.countNumberPositionsOdPet(request.PetId) > 5)
            {
                var firstPosition = _positionRepository.firstPositionOfPet(request.PetId);
                _positionRepository.delete(firstPosition);
            }
        }

        public List<Position> findAll()
        {
            return _positionRepository.findAll();
        }

        public Position findById(int id)
        {
            Position position = _positionRepository.findById(id);
            if (position == null) throw new NotFoundException("Position not found");
            return position;
        }

        public void delete(int id)
        {
            Position position = this.findById(id);
            _positionRepository.delete(position);
        }

        public void update(int id, PositionRequest request)
        {
            this.findById(id);
            _petService.findById(request.PetId);
            Position position = new Position();
            position.PositionId = id;
            position.Latitude = request.Latitude;
            position.Longitude = request.Longitude;
            position.DateTime = (DateTime)request.DateTime;
            position.PetId = request.PetId;

            _positionRepository.update(position);
        }

        public Position lastPositionOfPet(int id)
        {
            _petService.findById(id);
            var position = _positionRepository.lastPositionOfPet(id);
            if (position == null) throw new NotFoundException("There isn´t position for this pet");
            return position;
        }
    }
}
