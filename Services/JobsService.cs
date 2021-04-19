
using System;
using System.Collections.Generic;
using contractor.Models;
using contractor.Repositories;

namespace contractor.Services
{
    public class JobsService
    {
        private readonly JobsRepository _jrepo;
        public JobsService(JobsRepository jrepo)
        {
            _jrepo = jrepo;
        }

        internal IEnumerable<Job> GetJobsByProfileId(string id)
        {
            return _jrepo.GetByProfileId(id);
        }

        internal IEnumerable<Job> GetAll()
        {
            return _jrepo.GetAll();
        }

        internal Job GetById(int id)
        {
            Job data = _jrepo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid ID");
            }
            return data;
        }

        internal Job Create(Job newJob)
        {
            return _jrepo.Create(newJob);
        }

        internal Job Edit(Job updated)
        {
            Job data = GetById(updated.Id);
            data.Name = updated.Name != null ? updated.Name : data.Name;

            return _jrepo.Edit(data);
        }

        internal string Delete(int id, string userId)
        {
            GetById(id);
            _jrepo.Delete(id, userId);
            return "Job Deleted";
        }

        internal IEnumerable<ContractorJobViewModel> GetJobsByContractorId(int id)
        {
            return _jrepo.GetJobsByContractorId(id);
        }
    }
}