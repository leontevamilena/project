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
    public class DeleteModel : PageModel
    {
        private readonly CinemaDbLibrary.Contexts.CinemaDbContext _context;

        public DeleteModel(CinemaDbLibrary.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frame = await _context.Frames.FindAsync(id);
            if (frame != null)
            {
                Frame = frame;
                _context.Frames.Remove(Frame);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
