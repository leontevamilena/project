using Dapper;
using LabWork8.Models;

namespace LabWork8.Repository
{
    public class VisitorRepository(DatabaseContext databaseContext) : IRepository<Visitor>
    {
        private readonly DatabaseContext _dbContext = databaseContext;

        public async Task<Visitor?> GetByIdAsync(int id)
            => await _dbContext.CreateConnection()
            .QueryFirstOrDefaultAsync<Visitor>("SELECT * FROM Visitor WHERE VisitorId=@id", new { id });

        public async Task<IEnumerable<Visitor>> GetAllAsync()
            => await _dbContext.CreateConnection()
            .QueryAsync<Visitor>("SELECT * FROM Visitor");
    }
}
