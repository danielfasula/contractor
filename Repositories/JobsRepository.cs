using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using contractor.Models;
using Dapper;

namespace contractor.Repositories
{
    public class JobsRepository
    {
        private readonly IDbConnection _db;

        public JobsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Job> GetByProfileId(string id)
        {
            string sql = @"
            SELECT
            j.*,
            p.*
            FROM jobs j
            JOIN profiles p ON j.creatorId = p.id
            WHERE creatorId = @id;
            ";
            return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
            {
                job.CreatorId = profile.Id;
                job.Creator = profile;
                return job;
            }, new { id }, splitOn: "id");
        }

        internal Job GetById(int id)
        {
            string sql = @"
            SELECT
            j.*,
            p.*
            FROM jobs j
            JOIN profiles p ON j.creatorId = p.id
            WHERE j.id = @id;
            ";
            return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
            {
                job.Creator = profile;
                return job;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal IEnumerable<Job> GetAll()
        {
            string sql = @"
            SELECT
            j.*,
            p.*
            FROM jobs j
            JOIN profiles p ON j.creatorId = p.id
            ";
            return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
            {
                job.Creator = profile;
                return job;
            }, splitOn: "id");
        }


        internal Job Create(Job newJob)
        {
            string sql = @"
            INSERT INTO jobs
            (id, name, creatorId)
            VALUES
            (@Id, @Name, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newJob);
            newJob.Id = id;
            return newJob;
        }

        internal Job Edit(Job data)
        {
            string sql = @"
            UPDATE jobs
            SET
                name = @Name,
            WHERE id = @Id;
            SELECT * FROM jobs WHERE id = @Id;
            ";
            Job returnJob = _db.QueryFirstOrDefault<Job>(sql, data);
            return returnJob;
        }

        internal void Delete(int id, string userId)
        {
            string sql = @"
            DELETE FROM jobs WHERE id = @id AND creatorId = @userId LIMIT 1;
            ";
            _db.Execute(sql, new { id, userId });
        }

        internal IEnumerable<ContractorJobViewModel> GetJobsByContractorId(int id)
        {
            string sql = @"
            SELECT
            j.*,
            cj.id AS ContractorJobId
            FROM contractorjobs cj
            JOIN jobs j ON j.id = cj.jobId
            WHERE contractorId = @id;
            ";
            return _db.Query<ContractorJobViewModel>(sql, new { id });
        }

    }
}