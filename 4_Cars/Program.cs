using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Cars {
    class Program {
        static void Main(string[] args) {
            var samochody = WczytywanieSamochodu("paliwo.csv");
            var producenci = WczytywanieProducenci("producent.csv");

            var query = from car in samochody
                        group car by car.Producent into samochodGrupa
                        select new {
                            Nazwa = samochodGrupa.Key,
                            Max = samochodGrupa.Max(s => s.SpalanieAutostrada),
                            Min = samochodGrupa.Min(s => s.SpalanieAutostrada),
                            Avg = samochodGrupa.Average(s => s.SpalanieAutostrada)
                        } into wynik
                        orderby wynik.Max descending
                        select wynik;

            var query2 = samochody.GroupBy(s => s.Producent)
                .Select(g => {
                    return new {
                        Nazwa = g.Key,
                        Max = g.Max(s => s.SpalanieAutostrada),
                        Min = g.Min(s => s.SpalanieAutostrada),
                        Avg = g.Average(s => s.SpalanieAutostrada)
                    };
                }).OrderByDescending( g => g.Max);

            var query3 = from producent in producenci
                         join samochod in samochody
                         on producent.Nazwa equals samochod.Producent into samochodGrupa
                         orderby producent.Siedziba
                         select new {
                             Producent = producent,
                             Samochody = samochodGrupa
                         } into wynik
                         group wynik by wynik.Producent.Siedziba;

            var query4 = producenci.GroupJoin(samochody, p => p.Nazwa, s => s.Producent,
                (p, g) => new {
                    Producent = p,
                    Samochody = g
                }).OrderBy(p => p.Producent.Siedziba)
                .GroupBy(g => g.Producent.Siedziba);


            foreach (var wynik in query2) {
                Console.WriteLine($"{wynik.Nazwa} ");
                Console.WriteLine($"  Max: {wynik.Max}");
                Console.WriteLine($"  Min: {wynik.Min}");
                Console.WriteLine($"  Avg: {wynik.Avg}");
            }
        }

        private static List<Producent> WczytywanieProducenci(string path) {
            var query = File.ReadAllLines(path)
                .Where(p => p.Length > 1)
                .Select(p => {
                    var kolumny = p.Split(',');
                    return new Producent {
                        Nazwa = kolumny[0],
                        Siedziba = kolumny[1],
                        Rok = int.Parse(kolumny[2])
                    };
                });
            return query.ToList();
        }

        private static List<Samochod> WczytywanieSamochodu(string path) {
            var test = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .WSamochod();
            return test.ToList();
        }
    }

    public static class SamochodExtension {
        public static IEnumerable<Samochod> WSamochod(this IEnumerable<string> zrodlo) {

            foreach (var line in zrodlo) {

                var kolumny = line.Split(',');

                yield return new Samochod {
                    Rok = int.Parse(kolumny[0]),
                    Producent = kolumny[1],
                    Model = kolumny[2],
                    Pojemnosc = double.Parse("2"),
                    IloscCylindrow = int.Parse(kolumny[4]),
                    SpalanieMiasto = int.Parse(kolumny[5]),
                    SpalanieAutostrada = int.Parse(kolumny[6]),
                    SpalanieMieszane = int.Parse(kolumny[7])
                };
            }
        }
    }
}
