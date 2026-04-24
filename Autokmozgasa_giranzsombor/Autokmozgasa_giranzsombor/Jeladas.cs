using System;
using System.Collections.Generic;
using System.Text;

namespace Autokmozgasa_giranzsombor
{
    internal class Jeladas
    {
        public string Rendszam { get; set; }
        public int Ora { get; set; }
        public int Perc { get; set; }
        public int Sebesseg { get; set; }

        public Jeladas(string sor)
        {
            string[] adatok = sor.Split('\t');

            Rendszam = adatok[0];
            Ora = int.Parse(adatok[1]);
            Perc = int.Parse(adatok[2]);
            Sebesseg = int.Parse(adatok[3]);
        }

        public int Percben()
        {
            return Ora * 60 + Perc;
        }

        public string IdoString()
        {
            return Ora + ":" + Perc;
        }
    }
}
