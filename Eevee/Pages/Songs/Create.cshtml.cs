using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eevee.Data;
using Eevee.Models;

namespace Eevee.Pages.Songs
{
    public class CreateModel : PageModel
    {
        private readonly Eevee.Data.EeveeContext _context;

        private readonly ITP _textprocessor;

        public string error = "";

        public CreateModel(Eevee.Data.EeveeContext context, ITP textprocessor)
        {
            _context = context;
            _textprocessor = textprocessor;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Song Song { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                {
                    error = string.Join(" | ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));
                return Page();
            }

            Song.WordVec = _textprocessor.Predict(Song.Lyrics);

            _context.Song.Add(Song);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
