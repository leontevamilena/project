using LabWork12.Contexts;
using LabWork12.Models;
using Microsoft.EntityFrameworkCore;


namespace LabWork12.Services
{
    public class MovieService(MovieDbContext context)
    {
        private readonly MovieDbContext _context = context;

        public async Task<List<Movie>> GetMoviesAsync(string sortColumn)
            => await context.Movies
                .FromSqlRaw($"SELECT * FROM Movie ORDER BY {sortColumn}")
                .ToListAsync();

        public async Task<List<Movie>> GetMoviesByYearAndNameAsync(string name, int year)
            => await _context.Movies
                .FromSql($"SELECT * FROM Movie WHERE Name = {name} AND Year >= {year}")
                .ToListAsync();

        public async Task<List<string>> GetGenresMoviesByIdAsync(int id)
            => await _context.Database
                .SqlQuery<string>(@$"SELECT Genre.[Name]
                FROM GenreMovie INNER JOIN
                Genre ON GenreMovie.GenreId = Genre.GenreId
                WHERE GenreMovie.MovieId = {id}")
                .ToListAsync();

        public async Task<List<Movie>> GetMoviesByLetterRangeAsync(char startRange, char endRange)
            => await context.Movies
                .Where(m => EF.Functions.Like(m.Name, $"[{startRange}-{endRange}]%"))
                .ToListAsync();

    }
}
