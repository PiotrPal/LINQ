using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_Basics {
    internal class Program {
        static void Main(string[] args) {
            var path = @"c:\windows";

            ShowData(path);
            Console.WriteLine("-----------");
            ShowDataLinq(path);
            Console.WriteLine("-----------");
            ShowDataLinq2(path);

        }

        private static void ShowDataLinq2(string path) { //(method syntax)
            var query = new DirectoryInfo(path).GetFiles()
                        .OrderByDescending(f => f.Length)
                        .Take(5);

            foreach (var file in query) {
                Console.WriteLine($"{file.Name,-20} : {file.Length,15:N0}");
            }
        }

        private static void ShowDataLinq(string path) { //query syntax
            var query = from plik in new DirectoryInfo(path).GetFiles()
                        orderby plik.Length descending
                        select plik;

            foreach (var file in query.Take(5)) {
                Console.WriteLine($"{file.Name,-20} : {file.Length,15:N0}");
            }
        }

        private static void ShowData(string path) {
            DirectoryInfo dir = new DirectoryInfo(path);
            var pliki = dir.GetFiles();//tablica

            Array.Sort(pliki, new FileInfoComparer());

            for (int i = 0; i < 5; i++) {
                FileInfo file = pliki[i];
                Console.WriteLine($"{file.Name,-20} : {file.Length,15:N0}");
            }
        }
    }

    public class FileInfoComparer : IComparer<FileInfo> {
        public int Compare(FileInfo x, FileInfo y) {
            return y.Length.CompareTo(x.Length);
        }
    }
}
