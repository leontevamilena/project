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
    public class DetailsModel : PageModel
    {
        private readonly CinemaDbLibrary.Contexts.CinemaDbContext _context;

        public DetailsModel(CinemaDbLibrary.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public Frame Frame { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frame = await _context.Frames.FirstOrDefaultAsync(m => m.FrameId == id);

            if (frame is not null)
            {
                Frame = frame;

                return Page();
            }

            return NotFound();
        }
    }
}
