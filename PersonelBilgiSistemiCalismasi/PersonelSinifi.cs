using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelBilgiSistemiCalismasi
{
    public class PersonelSinifi
    {
        public string PersonelTcKimlikNo { get; set; }

        public string PersonelAdi { get; set; }

        public string PersonelSoyad { get; set; }

        public DateTime PersonelDogumTarihi { get; set; }

        public string PersonelTelefon { get; set; }

        public string PersonelAdres { get; set; }

        public string PerosnelMail { get; set; }

        public DateTime PersoneliseGirisTarihi { get; set; }

        public PersonelUnvalari PersonelUnvani { get; set; }

        public string PersonelResmi { get; set; }


    }
}
