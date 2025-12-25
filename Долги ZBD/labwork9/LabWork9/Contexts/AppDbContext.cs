using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork9.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Visitor> Visitors => Set<Visitor>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=mssql;Initial Catalog=ispp3103;User ID=ispp3103;Password=3103;Trust Server Certificate=True");
        }
    }   
}
