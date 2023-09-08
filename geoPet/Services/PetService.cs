﻿using geoPet.Entities;
using geoPet.Entities.Enuns;
using geoPet.Exceptions;
using geoPet.Models;
using geoPet.Repositories;

namespace geoPet.Services
{
    public class PetService
    {
        private readonly PetRepository _petRepository;
        private readonly OwerRepository _owerRepository;
        public PetService(PetRepository petRepository, OwerRepository owerRepository)
        {
            this._petRepository = petRepository;
            this._owerRepository = owerRepository;
        }

        public void post(PetRequest request)
        {
            Ower ower = _owerRepository.findById(request.OwerId);

            if (ower == null) throw new NotFoundException("Ower not found");

            Size size;

            switch (request.Size)
            {
                case "SMALL":
                    size = Size.SMALL;
                    break;
                case "MEDIUM":
                    size = Size.MEDIUM;
                    break;
                case "LARGE":
                    size = Size.LARGE;
                    break;
                default:
                    throw new InvalidValueException("Invalid Size");
            }

            Pet pet = new Pet();
            pet.Name = request.Name;
            pet.Age = request.Age;
            pet.Size = size;
            pet.Breed = request.Breed;
            pet.OwerId = request.OwerId;

            _petRepository.post(pet);
        }

        public Pet findById(int id)
        {
            Pet pet = _petRepository.findById(id);
            if (pet == null) throw new NotFoundException("Pet not found");
            return pet;
        }

        public List<Pet> findAll()
        {
            return _petRepository.findAll();
        }

        public void delete(int id)
        {
            Pet pet = _petRepository.findById(id);
            _petRepository.delete(pet);
        }

        public void update(int id, PetRequest request)
        {
            this.findById(id);

            Size size;

            switch (request.Size)
            {
                case "SMALL":
                    size = Size.SMALL;
                    break;
                case "MEDIUM":
                    size = Size.MEDIUM;
                    break;
                case "LARGE":
                    size = Size.LARGE;
                    break;
                default:
                    throw new InvalidValueException("Invalid Size");
            }

            Pet pet = new Pet();
            pet.PetId = id;
            pet.Name = request.Name;
            pet.Age = request.Age;
            pet.Size = size;
            pet.Breed = request.Breed;
            pet.OwerId = request.OwerId;

            _petRepository.update(pet);
        }
    }
}
