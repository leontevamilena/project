using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;

namespace LabWork20.Pages.Frames
{
    public class EditModel : PageModel
    {
        private readonly CinemaDbLibrary.Contexts.CinemaDbContext _context;

        public EditModel(CinemaDbLibrary.Contexts.CinemaDbContext context)
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

            var frame =  await _context.Frames.FirstOrDefaultAsync(m => m.FrameId == id);
            if (frame == null)
            {
                return NotFound();
            }
            Frame = frame;
           ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Frame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FrameExists(Frame.FrameId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FrameExists(int id)
        {
            return _context.Frames.Any(e => e.FrameId == id);
        }
    }
}
