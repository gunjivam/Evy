using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eevee.Models;

namespace Eevee.Data
{
    public class EeveeContext : DbContext
    {
        public EeveeContext (DbContextOptions<EeveeContext> options)
            : base(options)
        {
        }

        public DbSet<Eevee.Models.Artist> Artist { get; set; }

        public DbSet<Eevee.Models.Song> Song { get; set; }

        public DbSet<Eevee.Models.User> User { get; set; }
    }
}
