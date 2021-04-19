using System.Data;
using contractor.Models;
using Dapper;

namespace contractor.Repositories
{
    public class ContractorJobsRepository
    {
        private readonly IDbConnection _db;

        public ContractorJobsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal ContractorJob Create(ContractorJob newCJ)
        {
            string sql = @"
            INSERT INTO contractorjobs 
            (contractorId, jobId, creatorId) 
            VALUES 
            (@ContractorId, @JobId, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newCJ);
            newCJ.Id = id;
            return newCJ;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM constructorjobs WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}