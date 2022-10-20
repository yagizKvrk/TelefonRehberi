using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberi
{
    internal class Kisi
    {
        public string Adi { get; set; }
        public string Rol { get; set; }
        public string Telefon { get; set; }
        public TelefonTipi TelefonTipi { get; set; }
    }

    enum TelefonTipi
    {
        Dahili, Cep, İş, Ev
    }
}
