//using _Funkcje;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funkcje {
    internal class Program {
        static void Main(string[] args) {

            Func<int, int> potega = x => x * x;
            Func<int, int, int> dodawanie = (a, b) => a + b;

            Action<int> wypisz = x => Console.WriteLine(x);

            wypisz(potega(2));
            wypisz(dodawanie(2, 8));

            IEnumerable<Pracownik> programisci = new Pracownik[]{
                new Pracownik { ID = 1, Name = "Marcin", Surname = "Nowak"},
                new Pracownik { ID = 2, Name = "Tomek", Surname = "Kowal"},
                new Pracownik { ID = 3, Name = "Jacek", Surname = "Sobota"},
                new Pracownik { ID = 4, Name = "Adam", Surname = "Wrona"}
                };
            IEnumerable<Pracownik> kierowcy = new List<Pracownik>(){
                new Pracownik { ID = 5, Name = "Olek", Surname = "Sroka"},
                new Pracownik { ID = 6, Name = "Pawel", Surname = "Wrobel"},
                new Pracownik { ID = 7, Name = "Marek", Surname = "Piatek"}
            };

            //###metoda nazwana
            //foreach (var osoba in programisci.Where(RozpoczynaM) ){
            //    Console.WriteLine(osoba.Name);
            //}

            //###metoda anonimowa
            //foreach (var osoba in programisci.Where(delegate (Pracownik pracownik) {
            //    return pracownik.Name.StartsWith("A");
            //})) {
            //    Console.WriteLine(osoba.Name);
            //}

            //###lambda
            foreach (var osoba in programisci
                .Where(p => p.Name.Length == 5)
                .OrderByDescending(p => p.Name)) {
                Console.WriteLine(osoba.Name);
            }
        }
        private static bool RozpoczynaM(Pracownik pracownik) {
            return pracownik.Name.StartsWith("M");
        }
    }
}
