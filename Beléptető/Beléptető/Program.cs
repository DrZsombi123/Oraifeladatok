namespace Beléptető
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. feladat
            List<Esemeny> esemenyek = new List<Esemeny>();

            using (StreamReader sr = new StreamReader("bedat.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    esemenyek.Add(new Esemeny(sor));
                }
            }

            // 2. feladat
            Console.WriteLine("2. feladat");

            Esemeny elsoBelepes = esemenyek.First(x => x.Tipus == 1);
            Esemeny utolsoKilepes = esemenyek.Last(x => x.Tipus == 2);

            Console.WriteLine($"Az első tanuló {elsoBelepes.IdoString()}-kor lépett be a főkapun.");
            Console.WriteLine($"Az utolsó tanuló {utolsoKilepes.IdoString()}-kor lépett ki a főkapun.");

            // 3. feladat
            using (StreamWriter sw = new StreamWriter("kesok.txt"))
            {
                List<Esemeny> kesok = esemenyek
                    .Where(x => x.Tipus == 1)
                    .Where(x => x.Perc > 470 && x.Perc <= 495)
                    .ToList();

                foreach (Esemeny e in kesok)
                {
                    sw.WriteLine(e.IdoString() + " " + e.Azonosito);
                }
            }

            // 4. feladat
            Console.WriteLine("4. feladat");

            int ebedelok = esemenyek.Count(x => x.Tipus == 3);

            Console.WriteLine("A menzán aznap " + ebedelok + " tanuló ebédelt.");

            // 5. feladat
            Console.WriteLine("5. feladat");

            int kolcsonzok = esemenyek
                .Where(x => x.Tipus == 4)
                .Select(x => x.Azonosito)
                .Distinct()
                .Count();

            Console.WriteLine("Aznap " + kolcsonzok + " tanuló kölcsönzött a könyvtárban.");

            if (kolcsonzok > ebedelok)
            {
                Console.WriteLine("Többen voltak, mint a menzán.");
            }
            else
            {
                Console.WriteLine("Nem voltak többen, mint a menzán.");
            }

            // 6. feladat
            Console.WriteLine("6. feladat");
            Console.WriteLine("Az érintett tanulók:");

            List<string> tanulok = esemenyek
                .Select(x => x.Azonosito)
                .Distinct()
                .ToList();

            List<string> erintettek = new List<string>();

            foreach (string tanulo in tanulok)
            {
                List<Esemeny> kapuEsemenyek = esemenyek
                    .Where(x => x.Azonosito == tanulo)
                    .Where(x => x.Tipus == 1 || x.Tipus == 2)
                    .Where(x => x.Perc < 645)
                    .ToList();

                if (kapuEsemenyek.Count > 0)
                {
                    Esemeny utolsoEsemeny = kapuEsemenyek.Last();

                    bool bentVolt = utolsoEsemeny.Tipus == 1;

                    bool szabalyosanKiment = esemenyek.Any(x =>
                        x.Azonosito == tanulo &&
                        x.Tipus == 2 &&
                        x.Perc >= 645 &&
                        x.Perc <= 650);

                    bool visszajott = esemenyek.Any(x =>
                        x.Azonosito == tanulo &&
                        x.Tipus == 1 &&
                        x.Perc > 650 &&
                        x.Perc <= 660);

                    if (bentVolt && !szabalyosanKiment && visszajott)
                    {
                        erintettek.Add(tanulo);
                    }
                }
            }

            Console.WriteLine(string.Join(" ", erintettek));

            // 7. feladat
            Console.WriteLine("7. feladat");
            Console.Write("Egy tanuló azonosítója=");

            string azonosito = Console.ReadLine();

            List<Esemeny> adott = esemenyek
                .Where(x => x.Azonosito == azonosito)
                .ToList();

            if (adott.Count == 0)
            {
                Console.WriteLine("Ilyen azonosítójú tanuló aznap nem volt az iskolában.");
            }
            else
            {
                Esemeny elso = adott.First(x => x.Tipus == 1);
                Esemeny utolso = adott.Last(x => x.Tipus == 2);

                int kul = utolso.Perc - elso.Perc;
                int ora = kul / 60;
                int perc = kul % 60;

                Console.WriteLine("A tanuló érkezése és távozása között " + ora + " óra " + perc + " perc telt el.");
            }
        }
    }
}
