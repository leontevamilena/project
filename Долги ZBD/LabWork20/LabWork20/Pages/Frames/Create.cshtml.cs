using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;

namespace LabWork20.Pages.Frames
{
    public class CreateModel : PageModel
    {
        private readonly CinemaDbLibrary.Contexts.CinemaDbContext _context;

        public CreateModel(CinemaDbLibrary.Contexts.CinemaDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Name");
            return Page();
        }

        [BindProperty]
        public Frame Frame { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Frame.Movie");
            ModelState.Remove("Frame.FileName");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var file = HttpContext.Request.Form.Files.FirstOrDefault();

            if (file?.Length > 0 && file?.Length >> 20 <= 2)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                Frame.FileName = file.FileName;
            }

            _context.Frames.Add(Frame);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
