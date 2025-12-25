using LabWork12.Contexts;
using LabWork12.DTOs;
using LabWork12.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12.Services
{
    public class SessionService(MovieDbContext context)
    {
        private readonly MovieDbContext _context = context;

        public async Task<int> IncreaseSessionPriceByHallIdAsync(int hallId, decimal price)
            => await _context.Database
                .ExecuteSqlAsync($"UPDATE Session SET Price += {price} WHERE HallId = {hallId} ");

        public async Task<SessionDto> GetPriceInfoByMovieIdAsync(int id)
        {
            var selectedPrices = _context.Sessions.Where(s => s.MovieId == id).Select(s => s.Price);

            var minPrice = await selectedPrices.MinAsync();
            var maxPrice = await selectedPrices.MaxAsync();
            var averagePrice = await selectedPrices.AverageAsync();

            return new SessionDto(minPrice, maxPrice, averagePrice);
        }

        public async Task<DateTime> GetSessionDateAndTimeByTicketIdAsync(int id)
            => await _context.Database
                .SqlQuery<DateTime>(@$"SELECT Session.StartDate AS value
                FROM Ticket INNER JOIN 
                Session ON Ticket.SessionId = Session.SessionId 
                WHERE Ticket.TicketId = {id}")
                .FirstOrDefaultAsync();

        public async Task<List<Session>> GetSessionsByMovieIdAsync(int id)
        => await _context.Sessions
            .FromSql($"SELECT * FROM dbo.GetSessionsByMovieId({id})")
            .ToListAsync();
    }
}
