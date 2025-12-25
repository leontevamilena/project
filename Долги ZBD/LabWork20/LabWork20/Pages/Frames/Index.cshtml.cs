using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;

namespace LabWork20.Pages.Frames
{
    public class IndexModel : PageModel
    {
        private readonly CinemaDbLibrary.Contexts.CinemaDbContext _context;

        public IndexModel(CinemaDbLibrary.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IList<Frame> Frame { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Frame = await _context.Frames
                .Include(f => f.Movie).ToListAsync();
        }
    }
}
