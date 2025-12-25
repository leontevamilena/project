using LabWork12.Contexts;
using LabWork12.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12.Services
{
    public class TicketService(MovieDbContext context)
    {
        private readonly MovieDbContext _context = context;

        public async Task<List<Ticket>> GetTicketsByVisitorPhoneAsync(string phone)
        => await _context.Tickets.FromSql($"dbo.GetTicketsByPhone {phone}")
            .ToListAsync();
    }
}
