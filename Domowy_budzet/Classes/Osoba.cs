using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Domowy_budzet
{
    class Osoba
    {
        private int id_osoby;
        private string imie;
        private string nazwisko;
        private string login;
        private string haslo;
        private bool isAdmin;

        public static void ErrorHandler(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ReadLine();
            Console.ResetColor();
            Console.Clear();
        }

        public static void zaloguj()
        {
            while (true)
            {
                try
                {
                    Console.Write("Podaj nazwe użytkownika: ");
                    string username = Console.ReadLine();
                    Console.Write("Podaj hasło:");
                    string password = Console.ReadLine();
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (FormatException)
                {
                    ErrorHandler("Błędnie podany format znaków!");
                }
                catch (System.IO.IOException)
                {
                    ErrorHandler("Błędnie zamknięty program! Zmiany mogły zostać nie zapisane!");
                }
                catch (ArgumentNullException)
                {
                    ErrorHandler("Błąd! Następnym razem wprowadź wartość!");
                }

            }
        }
        public void limit_wydatków()
        {

        }


    }
}
