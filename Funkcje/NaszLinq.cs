using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Funkcje {
    public static class NaszLinq {
        public static int Count<T>(this IEnumerable<T> kolekcja) {
            int licznik = 0;
            foreach (var item in kolekcja) {
                licznik++;
            }
            return licznik;
        }
    }
}
