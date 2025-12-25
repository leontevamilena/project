using Dapper;
using LabWork8.Models;

namespace LabWork8.Repository
{
    public class GenreRepository(DatabaseContext databaseContext) : IRepository<Genre>
    {
        private readonly DatabaseContext _dbContext = databaseContext;

        public async Task<Genre?> GetByIdAsync(int id)
            => await _dbContext.CreateConnection()
            .QueryFirstOrDefaultAsync<Genre>("SELECT * FROM Genre WHERE GenreId=@id", new { id });

        public async Task<IEnumerable<Genre>> GetAllAsync()
            => await _dbContext.CreateConnection()
            .QueryAsync<Genre>("SELECT * FROM Genre");
    }
}
