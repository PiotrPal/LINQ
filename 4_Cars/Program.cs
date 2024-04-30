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

            foreach (var car in samochody) {
                //Console.WriteLine(car.Rok);
                Console.WriteLine(car.Producent + " " + car.Model);
            }
        }

        private static List<Samochod> WczytywanieZpliku(string path) {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Samochod.ParseCSV).ToList();
        }

    }
}
