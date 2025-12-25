using LabWork9.Contexts;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Services
{
    public class VisitorService(AppDbContext context) : IService<Visitor>
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Visitor>> GetAsync()
            => await _context.Visitors.Include(v => v.Tickets).ToListAsync();

        public async Task AddAsync(Visitor entity)
        {
            await _context.Visitors.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {

        }

        public async Task UpdateAsync(Visitor entity)
        {

        }
    }
}
