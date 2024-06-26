﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Zapytania {
    internal class Program {
        static void Main(string[] args) {
            var liczby = NaszLinq.LiczbyLosowe().Where(l => l > 0.5).Take(10);

            foreach (var l in liczby) {
                Console.WriteLine("liczba:" + l);
            }

            var filmy = new List<Film>{
                new Film {Tytul = "Siedem", Gatunek="Thriller", Ocena = 8.3f, Rok = 1995},
                new Film {Tytul = "Efekt motyla", Gatunek="Thriller", Ocena = 7.8f, Rok = 2004},
                new Film {Tytul = "127 godzin", Gatunek="Dramat", Ocena = 7.1f, Rok = 2010},
                new Film {Tytul = "Skazani na Shawshank", Gatunek="Dramat", Ocena=  8.7f, Rok = 1994},
                new Film {Tytul = "Zielona mila", Gatunek="Dramat", Ocena = 8.6f, Rok = 1999},
                new Film {Tytul = "Forrest Gump", Gatunek="Komedia", Ocena = 8.5f, Rok =1994},
                new Film {Tytul = "Piękny umysł ", Gatunek="Dramat", Ocena = 8.3f, Rok = 2001},
                new Film {Tytul = "Gladiator", Gatunek="Dramat", Ocena = 8.1f, Rok = 2000}
            };

            //var query = Enumerable.Empty<Film>();


            //var query = filmy.Filtr(f => f.Rok > 2002);
            var query = filmy.Where(f => f.Rok > 2002)
                .OrderByDescending(f => f.Ocena);

            //var query = filmy.Filtr(f => f.Rok > 2002).ToList();
            //var query = filmy.Where(f => f.Rok > 2002);

            //Console.WriteLine($"ilosc: {query.Count()}");

            var enumerator = query.GetEnumerator();

            while (enumerator.MoveNext()) {
                Console.WriteLine(enumerator.Current.Tytul);
            }
        }
    }
}
