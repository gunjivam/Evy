using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Eevee.Data;
using Eevee.Models;

namespace Eevee.Pages.Songs
{
    public class DetailsModel : PageModel
    {
        private readonly Eevee.Data.EeveeContext _context;

        public DetailsModel(Eevee.Data.EeveeContext context)
        {
            _context = context;
        }

        public Song Song { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Song = await _context.Song.FirstOrDefaultAsync(m => m.SongID == id);

            if (Song == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
