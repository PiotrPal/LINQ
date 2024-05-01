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

            var query = samochody
                .Join(producenci,
                    s => s.Producent,
                    p => p.Nazwa,
                    (s, p) => new {
                        //p.Siedziba,   // wersja1
                        //s.Model,
                        //s.SpalanieAutostrada,
                        //s.Producent,
                        samochod = s,
                        producent = p
                    })
                .OrderByDescending(x => x.samochod.SpalanieAutostrada)
                .ThenBy(x => x.samochod.Producent)
                .Select(x => new { // wersja2
                    x.producent.Siedziba,
                    x.samochod.Producent,
                    x.samochod.Model,
                    x.samochod.SpalanieAutostrada
                });
                //.Select(x => new { x.Siedziba, x.Producent, x.Model, x.SpalanieAutostrada }); // wersja1

            var query2 = from samochod in samochody
                         join producent in producenci on samochod.Producent equals producent.Nazwa
                         orderby samochod.SpalanieAutostrada descending, samochod.Producent ascending
                         select new {
                             producent.Siedziba,
                             samochod.Model,
                             samochod.SpalanieAutostrada,
                             samochod.Producent
                         };

            foreach (var car in query.Take(10)) {

                // Console.WriteLine(car.Producent + " " + car.Siedziba + " " + car.Model + " " + car.SpalanieAutostrada); // wersja 1
                //Console.WriteLine(car.producent.Siedziba, car.samochod.Model, car.samochod.Producent, car.samochod.SpalanieAutostrada); // bez selecta
                Console.WriteLine(car.Siedziba, car.Model, car.Producent, car.SpalanieAutostrada); // wersja 2

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
