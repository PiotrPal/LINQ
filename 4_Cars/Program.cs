using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _4_Cars {
    class Program {
        static void Main(string[] args) {
            TworzenieXML();
            ZapytanieXML();
            Console.WriteLine("\t -----");
            ZapytanieXML2();
        }

        private static void ZapytanieXML2() {
            var dokument = XDocument.Load("paliwo.xml");
            var query = dokument
                //.Descendants("Samochod") 
                .Element("Samochody")
                .Elements("Samochod")
                .Where(s => s.Attribute("Producent")?.Value == "Ferrari") //? <- bezpieczneij bo nie wywali wyjatku
                .Select(s => new {
                    Model = s.Attribute("Model").Value,
                    Producent = s.Attribute("Producent").Value
                });

                foreach (var car in query) {
                Console.WriteLine($"{car.Producent} : {car.Model}");
            }
        }

        private static void ZapytanieXML() {
            var dokument = XDocument.Load("paliwo.xml");
            var query = from element in dokument.Element("Samochody").Elements("Samochod")
                        where element.Attribute("Producent").Value == "Ferrari"
                        select element.Attribute("Model").Value;

            foreach (var car in query) {
                Console.WriteLine($"{car}");
            }
        }

        private static void TworzenieXML() {

            var rekordy = WczytywanieSamochodu("paliwo.csv");

            var dokument = new XDocument();
            var samochody = new XElement("Samochody",
                from rekord in rekordy
                select new XElement("Samochod",
                    new XAttribute("Rok", rekord.Rok),
                    new XAttribute("Producent", rekord.Producent),
                    new XAttribute("Model", rekord.Model),
                    new XAttribute("SpalanieAutostrada", rekord.SpalanieAutostrada),
                    new XAttribute("SpalanieMiasto", rekord.SpalanieMiasto),
                    new XAttribute("SpalanieMieszane", rekord.SpalanieMieszane)
                )
            );

            #region wersjadluga
            //foreach (var rekord in rekordy) {
            //var samochod = new XElement("Samochod");

            //var producent = new XAttribute("Producent", rekord.Producent);
            //var model = new XAttribute("Model",rekord.Model);
            //var spalanieAutostrada = new XAttribute("SpalanieAutostrada", rekord.SpalanieAutostrada);
            //var spalanieMiasto = new XAttribute("SpalanieMiasto", rekord.SpalanieMiasto);

            //var samochod = new XElement("Samochod", producent,model, spalanieAutostrada, spalanieMiasto); // funkcjonalna konstrukacja

            //wersja skrocona od razu 
            //var samochod = new XElement("Samochod",
            //    new XAttribute("Producent", rekord.Producent),
            //    new XAttribute("Model", rekord.Model),
            //    new XAttribute("SpalanieAutostrada", rekord.SpalanieAutostrada),
            //    new XAttribute("SpalanieMiasto", rekord.SpalanieMiasto),
            //    new XAttribute("SpalanieMieszane", rekord.SpalanieMieszane)
            //);

            //samochod.Add(producent);
            //samochod.Add(model);
            //samochod.Add(spalanieAutostrada);
            //samochod.Add(spalanieMiasto);

            //    samochody.Add(samochod);
            //}
            #endregion 

            dokument.Add(samochody);
            dokument.Save("paliwo.xml");
        }

        private static List<Producent> WczytywanieProducenci(string path) {
            var query = File.ReadAllLines(path)
                .Where(p => p.Length > 1)
                .Select(p => {
                    var kolumny = p.Split(',');
                    return new Producent {
                        Nazwa = kolumny[0],
                        Siedziba = kolumny[1],
                        Rok = int.Parse(kolumny[2])
                    };
                });
            return query.ToList();
        }

        private static List<Samochod> WczytywanieSamochodu(string path) {
            var test = File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
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
