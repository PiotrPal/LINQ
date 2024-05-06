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

            var query = from samochod in samochody
                        group samochod by samochod.Producent.ToUpper() into producent
                        orderby producent.Key
                        select producent;

            var query2 = samochody.GroupBy(s => s.Producent.ToUpper())
                .OrderBy(g => g.Key);

            //var query69 = samochody.Select(s => s.Producent);
            //var query70 = samochody.GroupBy(s => s.Producent);

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
                (p,g) => new {
                    Producent = p, 
                    Samochody = g
                }).OrderBy(p => p.Producent.Siedziba) 
                .GroupBy(g => g.Producent.Siedziba);


            foreach (var grupa in query4) {
                //Console.WriteLine($"{ item.Key } ma { item.Count() } samochodow");

                Console.WriteLine($"{grupa.Key}");

                foreach (var car in grupa.SelectMany(g => g.Samochody)
                    .OrderByDescending(c => c.SpalanieAutostrada).Take(3)) {
                    Console.WriteLine($"\t{car.Model} : {car.SpalanieAutostrada}");
                }
            }

            //foreach (var car in query70) {
            //    Console.WriteLine(car.Key);
            //}



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
