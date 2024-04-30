using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Cars {
    internal class Program {
        static void Main(string[] args) {
            var samochody = WczytywanieZpliku("paliwo.csv");

            var query = samochody
                .Where(s => s.Producent == "xddd" && s.Rok == 2018)
                .OrderByDescending(s => s.SpalanieAutostrada)
                .ThenBy(s => s.Producent)
                //.First(); jak nie ma pasujacego to sie wywali 
                .FirstOrDefault();

            var query2 = from car in samochody
                         where car.Producent == "Audi" && car.Rok == 2018
                         orderby car.SpalanieAutostrada descending, car.Producent ascending
                         select car;

            //query = query.Reverse();

            foreach (var car in query2.Take(10)) {
                //Console.WriteLine(car.Rok);
                Console.WriteLine(car.Producent + " " + car.Model + " " + car.SpalanieAutostrada + " " + car.Rok);
            }

            if (query != null) {
                Console.WriteLine("\n" + query.Producent + " " + query.Model);
            }
        }

        private static List<Samochod> WczytywanieZpliku2(string path) {
            return (from line in File.ReadAllLines(path).Skip(1)
                    where line.Length > 1
                    select Samochod.ParseCSV(line)).ToList();
        }

        private static List<Samochod> WczytywanieZpliku(string path) {
            var test = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Samochod.ParseCSV).ToList();
            return test;
        }
    }
}
