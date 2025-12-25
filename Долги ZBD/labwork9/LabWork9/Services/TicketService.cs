using LabWork9.Contexts;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Services
{
    public class TicketService(AppDbContext context) : IService<Ticket>
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Ticket>> GetAsync()
            => await _context.Tickets.Include(t => t.Visitor).ToListAsync();

        public async Task AddAsync(Ticket entity)
        {
            await _context.Tickets.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {

        }

        public async Task UpdateAsync(Ticket entity)
        {

        }
    }
}
