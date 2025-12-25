using LabWork12.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12.Services
{
    public class VisitorService(MovieDbContext context)
    {
        private readonly MovieDbContext _context = context;

        public async Task<int> AddVisitorByPhoneAsync(string phone)
        {
            var id = new SqlParameter("@id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
            };
            await _context.Database.ExecuteSqlRawAsync($"dbo.AddVisitor {phone}, @id OUTPUT", id);
            return (int)id.Value;
        }
    }
}
