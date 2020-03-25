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
    public class IndexModel : PageModel
    {
        private readonly Eevee.Data.EeveeContext _context;

        private readonly ITP _textprocessor;

        public IndexModel(Eevee.Data.EeveeContext context, ITP textprocessor)
        {
            _context = context;

            _textprocessor = textprocessor;
        }

        public IList<Song> Song { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = "";

        public string msg = "";

        public string lv = "";

        public async Task OnGetAsync()
        {
            if (SearchString.Length > 0)
            {
                List<Song> songs = _context.Song.ToList();

                var word_vector = _textprocessor.ToArray(_textprocessor.Predict(SearchString));
                lv = _textprocessor.ToString(word_vector);

                if (!string.IsNullOrEmpty(SearchString))
                {
                    foreach (var song in songs)
                    {
                        msg += song.Name + ": " + _textprocessor.Loss(word_vector, _textprocessor.ToArray(song.WordVec)) + ", ";
                    }

                    songs.Sort((a, b) => _textprocessor.Loss(word_vector, _textprocessor.ToArray(a.WordVec)).CompareTo(_textprocessor.Loss(word_vector, _textprocessor.ToArray(b.WordVec))));
                }
                Song = songs;
            }
            else
            {
                Song = await _context.Song.ToListAsync();
            }
        }
    }
}
