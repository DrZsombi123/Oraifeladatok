using System;
using System.Collections.Generic;
using System.Text;

namespace Beléptető
{
    internal class Esemeny
    {
        public string Azonosito { get; set; }
        public int Perc { get; set; }
        public int Tipus { get; set; }

        public Esemeny(string sor)
        {
            string[] adatok = sor.Split(' ');
            Azonosito = adatok[0];

            string[] ido = adatok[1].Split(':');
            Perc = int.Parse(ido[0]) * 60 + int.Parse(ido[1]);

            Tipus = int.Parse(adatok[2]);
        }

        public string IdoString()
        {
            int ora = Perc / 60;
            int perc = Perc % 60;
            return $"{ora:D2}:{perc:D2}";
        }
    }
}
