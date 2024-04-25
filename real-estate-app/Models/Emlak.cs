using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace real_estate_app.Models
{
    public class Emlak
    {
        
            public int EmlakId { get; set; }

            [Required]
            public string Adres { get; set; }
            public string Sehir { get; set; }
            public string Ilce { get; set; }

            public int OdaSayisi { get; set; }

            public int MetreKare { get; set; }

            public int Fiyat { get; set; }

            public bool SatildiMi { get; set; }

            // Emlağın bağlı olduğu Satıcı Id'si
            public int SaticiId { get; set; }

            // Navigation property - Emlak modeli ile Satıcı modeli arasında bir ilişki kuruyor
            public Satici Satici { get; set; }
        
    }
}
