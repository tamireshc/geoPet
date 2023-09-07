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
            _positionRepository.PostPosition(request);
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
            position.DateTime = request.DateTime;
            position.PetId = request.PetId;

            _positionRepository.update(position);
        }
    }
}
