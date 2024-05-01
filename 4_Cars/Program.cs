using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Cars {
    class Program {
        static void Main(string[] args) {
            var samochody = WczytywanieZpliku("paliwo.csv");

            var query = samochody
                .Where(s => s.Producent == "Audi" && s.Rok == 2018)
                .OrderByDescending(s => s.SpalanieAutostrada)
                .ThenBy(s => s.Producent)
                .Select(s => new { s.Producent, s.Model, s.SpalanieAutostrada });
            //.First(); jak nie ma pasujacego to sie wywali 
            //.FirstOrDefault();

            //var zapytanie = from samochod in samochody
            //                where samochod.Producent == "Audi" && samochod.Rok == 2018
            //                orderby samochod.SpalanieAutostrada descending, samochod.Producent ascending
            //                select new {
            //                    samochod.Producent,
            //                    samochod.Model,
            //                    samochod.SpalanieAutostrada
            //                };

            //var query2 = samochody.Contains<Samochod>(samochody[5]);

            //Console.WriteLine(query2);

            //query = query.Reverse();

            foreach (var car in query.Take(10)) {
                //Console.WriteLine(car.Rok);
                Console.WriteLine(car.Producent + " " + car.Model + " " + car.SpalanieAutostrada);
            }


            var zapytanie2 = samochody
                .SelectMany(s => s.Producent)
                .OrderBy(s => s);

            foreach (var litera in zapytanie2) {
                Console.WriteLine("# " + litera);
                //Console.WriteLine("=> "+ producent);
            }

            //if (query != null) {
            //    Console.WriteLine("\n" + query.Producent + " " + query.Model);
            //}
        }

        //private static List<Samochod> WczytywanieZpliku2(string path) {
        //    return (from line in File.ReadAllLines(path).Skip(1)
        //            where line.Length > 1
        //            select Samochod.ParseCSV(line)).ToList();
        //}

        private static List<Samochod> WczytywanieZpliku(string path) {
            var test = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                //.Select(Samochod.ParseCSV);
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
