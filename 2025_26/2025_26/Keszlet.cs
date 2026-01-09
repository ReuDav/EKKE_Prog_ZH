using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2025_26
{
    internal class Keszlet
    {
        public int MinEletkor { get; set; }
        public int Ar { get; set; }
        public int SorozatSzam { get; set; }
        public string Kategoria { get; set; }
        public int ElemekSzama { get; set; }
        public int Mennyiseg { get; set; }

        public  Keszlet(string sor) {
            string [] adatok = sor.Split(";");
            MinEletkor = int.Parse(adatok[0]);
            Ar = int.Parse(adatok[1]);
            SorozatSzam = int.Parse(adatok[2]);
            Kategoria = adatok[3];
            ElemekSzama = int.Parse(adatok[4]);
            Mennyiseg = int.Parse(adatok[5]);

        }
    }
}
