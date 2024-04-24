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

            public int OdaSayisi { get; set; }

            public double MetreKare { get; set; }

            public decimal Fiyat { get; set; }

            // Emlağın bağlı olduğu Satıcı Id'si
            public int SaticiId { get; set; }

            // Navigation property - Emlak modeli ile Satıcı modeli arasında bir ilişki kurar
            public Satici Satici { get; set; }
        
    }
}
