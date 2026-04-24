using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Autokmozgasa_giranzsombor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Jeladas> jeladasok = FajlbolOlvas("jeladas.txt");

            // 2. feladat
            Console.WriteLine("2. feladat:");

            Jeladas utolso = jeladasok.Last();

            Console.WriteLine("Az utolsó jeladás időpontja " + utolso.IdoString() +
                              ", a jármű rendszáma " + utolso.Rendszam);

            // 3. feladat
            Console.WriteLine("3. feladat:");

            string elsoJarmu = jeladasok.First().Rendszam;

            List<string> idopontok = jeladasok
                .Where(x => x.Rendszam == elsoJarmu)
                .Select(x => x.IdoString())
                .ToList();

            Console.WriteLine("Az első jármű: " + elsoJarmu);
            Console.WriteLine("Jeladásainak időpontjai: " + string.Join(" ", idopontok));

            // 4. feladat
            Console.WriteLine("4. feladat:");

            Console.Write("Kérem, adja meg az órát: ");
            int bekertOra = int.Parse(Console.ReadLine());

            Console.Write("Kérem, adja meg a percet: ");
            int bekertPerc = int.Parse(Console.ReadLine());

            int darabJeladas = jeladasok
                .Count(x => x.Ora == bekertOra && x.Perc == bekertPerc);

            Console.WriteLine("A jeladások száma: " + darabJeladas);

            // 5. feladat
            Console.WriteLine("5. feladat:");

            int maxSebesseg = jeladasok.Max(x => x.Sebesseg);

            List<string> maxRendszamok = jeladasok
                .Where(x => x.Sebesseg == maxSebesseg)
                .Select(x => x.Rendszam)
                .ToList();

            Console.WriteLine("A legnagyobb sebesség km/h: " + maxSebesseg);
            Console.WriteLine("A járművek: " + string.Join(" ", maxRendszamok));

            // 6. feladat
            Console.WriteLine("6. feladat:");

            Console.Write("Kérem, adja meg a rendszámot: ");
            string bekertRendszam = Console.ReadLine();

            List<Jeladas> jarmuJeladasai = jeladasok
                .Where(x => x.Rendszam == bekertRendszam)
                .ToList();

            if (jarmuJeladasai.Count == 0)
            {
                Console.WriteLine("Nem szerepel ilyen rendszámú jármű.");
            }
            else
            {
                double tavolsag = 0;

                for (int i = 0; i < jarmuJeladasai.Count; i++)
                {
                    Jeladas aktualis = jarmuJeladasai[i];

                    Console.WriteLine(aktualis.IdoString() + " " + tavolsag.ToString("F1") + " km");

                    if (i < jarmuJeladasai.Count - 1)
                    {
                        Jeladas kovetkezo = jarmuJeladasai[i + 1];

                        int elteltPerc = kovetkezo.Percben() - aktualis.Percben();

                        tavolsag += aktualis.Sebesseg * (elteltPerc / 60.0);
                    }
                }
            }

            // 7. feladat
            Console.WriteLine("7. feladat:");

            using (StreamWriter sw = new StreamWriter("ido.txt"))
            {
                List<IGrouping<string, Jeladas>> csoportok = jeladasok
                    .GroupBy(x => x.Rendszam)
                    .ToList();

                foreach (IGrouping<string, Jeladas> csoport in csoportok)
                {
                    Jeladas elso = csoport.First();
                    Jeladas utolsoJeladas = csoport.Last();

                    sw.WriteLine(csoport.Key + " " +
                                 elso.Ora + " " + elso.Perc + " " +
                                 utolsoJeladas.Ora + " " + utolsoJeladas.Perc);
                }
            }

            Console.WriteLine("ido.txt elkészült.");
        }

        static List<Jeladas> FajlbolOlvas(string fajlnev)
        {
            List<Jeladas> lista = new List<Jeladas>();

            using (StreamReader sr = new StreamReader(fajlnev))
            {
                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    lista.Add(new Jeladas(sor));
                }
            }

            return lista;
        }
    }
}