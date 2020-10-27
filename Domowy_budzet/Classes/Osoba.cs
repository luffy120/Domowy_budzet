using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

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
                    string passwordHashed = PasswordHashing.CreatePasswordHash(password);
                    int validation = ValidateUser(username, passwordHashed);
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
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            Console.WriteLine(path);
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            //chuj nie wiem jeszcze jak połączyć się jeszcze z tą bazą xD
            using (SqlConnection con = new SqlConnection(@"Data Source=@path\Database1.mdf;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("select * from Osoby where login like @username and password = @password;");
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Connection = con;
                con.Open();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                bool loginSuccessful = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));

                if (loginSuccessful)
                {
                    Console.WriteLine("Success!");
                }
                else
                {
                    Console.WriteLine("Invalid username or password");
                }
            }

            return 0;
        }

    }
}
