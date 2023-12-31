﻿using geoPet.Entities;
using geoPet.Exceptions;
using geoPet.Models;
using geoPet.Repositories;
using geoPet.Utils;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace geoPet.Services
{
    public class OwerService
    {
        private readonly OwerRepository _owerRepository;

        public OwerService(OwerRepository repository)
        {
            this._owerRepository = repository;
        }

        public string post(OwerRequest request)
        {
            try
            {
                var ckeckcep = new CheckCEP();
                var resultOfCheckCEP = ckeckcep.CheckAsyncCEP(request.CEP);
                if (resultOfCheckCEP.Result.Contains("cep"))
                {
                    Ower ower = new Ower();
                    ower.Name = request.Name;
                    ower.Email = request.Email;
                    ower.CEP = request.CEP;
                    ower.Password = new Hash(SHA512.Create()).CriptografarSenha(request.Password);
                    var response = _owerRepository.PostOwer(ower);
                    return response;
                }
                else
                {
                    return resultOfCheckCEP.Result;
                }

            }
            catch (DbUpdateException e)
            {
                throw new DuplicatedValueException("Email already exists");
            }
        }

        public List<OwerResponseWithoutPets> findAll()
        {
            List<OwerResponseWithoutPets> listOwerResponse = new List<OwerResponseWithoutPets>();
            List<Ower> owers = _owerRepository.findAll();

            foreach (Ower ower in owers)
            {
                OwerResponseWithoutPets owerResponse = new OwerResponseWithoutPets();
                owerResponse.OwerId = ower.OwerId;
                owerResponse.Name = ower.Name;
                owerResponse.CEP = ower.CEP;
                owerResponse.Email = ower.Email;

                listOwerResponse.Add(owerResponse);
            }

            return listOwerResponse;
        }

        public OwerResponse findById(int id)
        {
            Ower ower = _owerRepository.findById(id);
            if (ower == null) throw new NotFoundException("Ower not found");

            OwerResponse owerResponse = new OwerResponse();
            owerResponse.OwerId = ower.OwerId;
            owerResponse.Name = ower.Name;
            owerResponse.CEP = ower.CEP;
            owerResponse.Email = ower.Email;
            owerResponse.Pets = ower.Pets;

            return owerResponse;
        }

        public void delete(int id)
        {
            this.findById(id);
            _owerRepository.delete(id);
        }

        public string update(int id, OwerRequest owerRequest)
        {
            try
            {
                var ckeckcep = new CheckCEP();
                var resultOfCheckCEP = ckeckcep.CheckAsyncCEP(owerRequest.CEP);
                if (resultOfCheckCEP.Result.Contains("cep"))
                {
                    Ower ower = new Ower();
                    ower.OwerId = id;
                    ower.Name = owerRequest.Name;
                    ower.Email = owerRequest.Email;
                    ower.CEP = owerRequest.CEP;
                    ower.Password = new Hash(SHA512.Create()).CriptografarSenha(owerRequest.Password);
                    _owerRepository.update(ower);
                    return null;
                }
                else
                {
                    return resultOfCheckCEP.Result;
                }
            }
            catch (DbUpdateException e)
            {
                throw new DuplicatedValueException("Email already exists");
            }
        }

        public string login(LoginRequest loginRequest)
        {
            Ower ower = _owerRepository.findByEmail(loginRequest.Email);

            if (ower == null) throw new NotFoundException("Wrong user ou password");

            Hash hash = new Hash(SHA512.Create());
            bool passwordDecode = hash.VerificarSenha(loginRequest.Password, ower.Password);

            if (passwordDecode)
            {
                TokenGenerator tokenGenerator = new TokenGenerator();
                return tokenGenerator.Generate(ower);
            }

            throw new NotFoundException("Wrong user ou password");
        }
    }
}



