using System;
using System.Collections.Generic;
using contractor.Models;
using contractor.Repositories;

namespace contractor.Services
{
    public class ContractorsService
    {
        private readonly ContractorsRepository _crepo;
        public ContractorsService(ContractorsRepository crepo)
        {
            _crepo = crepo;
        }

        internal IEnumerable<Contractor> GetContractorsByProfileId(string id)
        {
            return _crepo.GetByProfileId(id);
        }

        internal IEnumerable<Contractor> GetAll()
        {
            return _crepo.GetAll();
        }

        internal Contractor GetById(int id)
        {
            Contractor data = _crepo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid ID");
            }
            return data;
        }

        internal Contractor Create(Contractor newContractor)
        {
            return _crepo.Create(newContractor);
        }

        internal Contractor Edit(Contractor updated)
        {
            Contractor data = GetById(updated.Id);
            data.Name = updated.Name != null ? updated.Name : data.Name;

            return _crepo.Edit(data);
        }

        internal string Delete(int id, string userId)
        {
            GetById(id);
            _crepo.Delete(id, userId);
            return "Contractor Deleted";
        }
    }
}