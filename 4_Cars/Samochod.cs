using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Cars {
    internal class Samochod {
        public int Rok {get; set; }
        public string Producent { get; set; }
        public string Model { get; set; }
        public double Pojemnosc{get; set; }
        public int IloscCylindrow { get; set; }
        public int SpalanieMiasto { get; set; }
        public int SpalanieAutostrada { get; set; }
        public int SpalanieMieszane { get; set; }

        internal static Samochod ParseCSV(string line) {
            var kolumny = line.Split(',');

            return new Samochod {
                Rok = int.Parse(kolumny[0]),
                Producent = kolumny[1],
                Model = kolumny[2],
                Pojemnosc = double.Parse("2"), //3,3 -> 3.3 zmiana searatora dziesetnego w panmelu sterowania 
                IloscCylindrow = int.Parse(kolumny[4]),
                SpalanieMiasto = int.Parse(kolumny[5]),
                SpalanieAutostrada = int.Parse(kolumny[6]),
                SpalanieMieszane = int.Parse(kolumny[7])
            };
        }
    }
}
