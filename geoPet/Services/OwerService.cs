using geoPet.Entities;
using geoPet.Exceptions;
using geoPet.Repositories;
using geoPet.Utils;
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
    }
}


