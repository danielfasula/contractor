using System.Collections.Generic;
using System.Data;
using System.Linq;
using contractor.Models;
using Dapper;

namespace contractor.Repositories
{
    public class ContractorsRepository
    {
        private readonly IDbConnection _db;

        public ContractorsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Contractor> GetByProfileId(string id)
        {
            string sql = @"
            SELECT
            c.*,
            p.*
            FROM contractors c
            JOIN profiles p ON c.creatorId = p.id
            WHERE creatorId = @id;
            ";
            return _db.Query<Contractor, Profile, Contractor>(sql, (contractor, profile) =>
            {
                contractor.CreatorId = profile.Id;
                contractor.Creator = profile;
                return contractor;
            }, new { id }, splitOn: "id");
        }

        internal Contractor GetById(int id)
        {
            string sql = @"
            SELECT
            c.*,
            p.*
            FROM contractors c
            JOIN profiles p ON c.creatorId = p.id
            WHERE c.id = @id;
            ";
            return _db.Query<Contractor, Profile, Contractor>(sql, (contractor, profile) =>
            {
                contractor.Creator = profile;
                return contractor;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal IEnumerable<Contractor> GetAll()
        {
            string sql = @"
            SELECT
            c.*,
            p.*
            FROM contractors c
            JOIN profiles p ON c.creatorId = p.id
            ";
            return _db.Query<Contractor, Profile, Contractor>(sql, (contractor, profile) =>
            {
                contractor.Creator = profile;
                return contractor;
            }, splitOn: "id");
        }
        internal Contractor Create(Contractor newContractor)
        {
            string sql = @"
            INSERT INTO contractors
            (id, name, creatorId)
            VALUES
            (@Id, @Name, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newContractor);
            newContractor.Id = id;
            return newContractor;
        }

        internal Contractor Edit(Contractor data)
        {
            string sql = @"
            UPDATE contractors
            SET
                name = @Name,
            WHERE id = @Id;
            SELECT * FROM contractors WHERE id = @Id;
            ";
            Contractor returnContractor = _db.QueryFirstOrDefault<Contractor>(sql, data);
            return returnContractor;
        }

        internal void Delete(int id, string userId)
        {
            string sql = @"
            DELETE FROM contractors WHERE id = @id AND creatorId = @userId LIMIT 1;
            ";
            _db.Execute(sql, new { id, userId });
        }

    }
}