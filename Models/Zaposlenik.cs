using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Zaposlenici.Models
{
    public class Zaposlenik
    {
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        [DisplayName("Datum i vrijeme zapošljavanja")]
        public DateTime Datum_zaposl { get; set; }
    }
}
