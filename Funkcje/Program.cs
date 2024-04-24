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

            //Console.WriteLine(NaszLinq.Count(programisci));

            Console.WriteLine(programisci.Count());
            Console.WriteLine(kierowcy.Count());

            IEnumerator<Pracownik> enumerator = kierowcy.GetEnumerator();

            while (enumerator.MoveNext()) {
                Console.WriteLine(enumerator.Current.Name);
            }
        }
    }
}
