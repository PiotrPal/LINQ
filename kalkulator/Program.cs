using System;

namespace kalkulator {
    internal class Program {
        static void Main(string[] args) {

            Func<string, double> convert = x => Convert.ToDouble(x);

            Console.WriteLine("Podaj pierwsza liczbe: ");
            var a = convert(Console.ReadLine());
            Console.WriteLine("Podaj druga liczbe: ");
            var b = convert(Console.ReadLine());

            Console.WriteLine("\tWybierz opcje 1:dodawanie 2:odejmowanie 3:dzielenie 4:mnozenie");
            var option = convert(Console.ReadLine());

            var result = 0.0;

            switch (option) {
                case 1:
                    result = a + b;
                    break;
                case 2:
                    result = a - b;
                    break;
                case 3:
                    result = a * b;
                    break;
                case 4:
                    if (b != 0) {
                        result = a / b;
                    } else {
                        Console.WriteLine("Nie można dzielić przez zero!");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Nieznana operacja!");
                    return;
            }
            Console.WriteLine($"Wynik: {result}");
        }
    }
}
