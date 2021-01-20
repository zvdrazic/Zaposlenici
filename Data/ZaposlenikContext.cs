using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zaposlenici.Models
{
    public class ZaposlenikContext : DbContext
    {
        public ZaposlenikContext (DbContextOptions<ZaposlenikContext> options)
            : base(options)
        {
        }

        public DbSet<Zaposlenici.Models.Zaposlenik> Zaposlenik { get; set; }
    }
}
