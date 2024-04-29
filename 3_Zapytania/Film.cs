using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Zapytania {
    internal class Film {
        public string Tytul { get; set; }
        public string Gatunek { get; set; }
        public float Ocena { get; set; }
        private int _rok;
        public int Rok {
            get {
                //throw new Exception("Error!!"); //temp
                Console.WriteLine($"Zwaraca {_rok} i {Tytul}");
                return _rok;
            }
            set {
                _rok = value;
            }
        }
    }
}
