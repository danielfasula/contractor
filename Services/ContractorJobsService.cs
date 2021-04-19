using contractor.Models;
using contractor.Repositories;

namespace contractor.Services
{
    public class ContractorJobsService
    {
        private readonly ContractorJobsRepository _cjrepo;
        public ContractorJobsService(ContractorJobsRepository cjrepo)
        {
            _cjrepo = cjrepo;
        }

        internal ContractorJob Create(ContractorJob newCJ)
        {
            //TODO if they are creating a wishlistproduct, make sure they are the creator of the list
            return _cjrepo.Create(newCJ);

        }

        internal void Delete(int id)
        {
            //NOTE getbyid to validate its valid and you are the creator
            _cjrepo.Delete(id);
        }
    }
}