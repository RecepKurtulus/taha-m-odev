using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace real_estate_app.Models
{
    public class Satici
    {
        public int SaticiId { get; set; }

       
        public string Ad { get; set; }
        public string Soyad { get; set; }


        public string Telefon { get; set; }
    }
}
