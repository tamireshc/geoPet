using geoPet.Entities;
using geoPet.Exceptions;
using geoPet.Models;
using geoPet.Repositories;
using geoPet.Utils;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace geoPet.Services
{
    public class OwerService
    {
        private readonly OwerRepository _owerRepository;

        public OwerService(OwerRepository repository)
        {
            this._owerRepository = repository;
        }

        public string PostOwer(OwerRequest request)
        {
            var ckeckcep = new CheckCEP();
            var resultOfCheckCEP =  ckeckcep.CheckAsyncCEP(request.CEP);
            if (resultOfCheckCEP.Result.Contains("cep"))
            {
                var response =_owerRepository.PostOwer(request);
                return response;
            }
            else
            {
                return resultOfCheckCEP.Result;
            }
        }

        public List<OwerResponse> findAll()
        {
            List<OwerResponse> listOwerResponse = new List<OwerResponse>();
            List<Ower> owers = _owerRepository.findAll();
           
            foreach (Ower ower in owers)
            {
                OwerResponse owerResponse = new OwerResponse();
                owerResponse.OwerId = ower.OwerId;
                owerResponse.Name = ower.Name;
                owerResponse.CEP = ower.CEP;
                owerResponse.Email = ower.Email;
                owerResponse.Pets = ower.Pets;

                listOwerResponse.Add(owerResponse);
            }

            return listOwerResponse;
        }

        public OwerResponse findById(int id)
        {
            Ower ower = _owerRepository.findById(id);

            if( ower == null)
            {
                throw new NotFoundException("Ower not found");
            }

            OwerResponse owerResponse = new OwerResponse();
            owerResponse.OwerId = ower.OwerId;
            owerResponse.Name = ower.Name;
            owerResponse.CEP = ower.CEP;
            owerResponse.Email = ower.Email;
            owerResponse.Pets = ower.Pets;

            return owerResponse;
        }
    }
}


