using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MySql.Data.MySqlClient;
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
                    int validation = ValidateUser(username, password);
                    if (validation == 0) { Console.WriteLine("Admin"); }
                    else if (validation == 1) { Console.WriteLine("User"); }
                    else if (validation == 2) { Console.WriteLine("Zły login lub hasło"); }
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

        private static int ValidateUser(string username, string password)
        {
            //Połączenie bazy danych, zmienic jak kiedyś będzie online
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=budzetdatabase;";
            // Zapytanie
            string query = "SELECT * FROM osoba where login ='"+username+"'";
            
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                // Otwarcie bazy danych
                databaseConnection.Open();

                // Wykonanie zapytania
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        //Bool sprawdza czy użytkownik jest adminem, hashedPassword pobiera stringa z bazy
                        bool isAdmin = reader.GetBoolean(3);
                        string hashedPassword = reader.GetString(5);
                        //Walidacja hasła podanego z hashem z bazy
                        bool passwordValidateHash = PasswordHashing.Validate(password, hashedPassword);
                        if (passwordValidateHash && isAdmin == true) { return 0; }
                        else if (passwordValidateHash && isAdmin == false) { return 1; }
                    }
                }
                else
                {
                //Nie znaleziono żadnego rekordu
                    return 2;
                }

                // Zamykanie połączenia
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Error jakiś
                Console.WriteLine("exce");
            }
            return 2;
        }

    }
}
