using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Login_Grid.Visibility = Visibility.Visible;
            Create_Grid.Visibility = Visibility.Hidden;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Min(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close(object sender, System.EventArgs e)
        {
            Close();
        }

        public bool email_clear = true;
        public bool password_clear = true;

        private void Clear_email(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (email_clear)
            {
                textBox.Text = string.Empty;
                email_clear = false;
            }
        }

        private void Clear_password(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (password_clear)
            {
                textBox.Text = string.Empty;
                password_clear = false;
            }
        }

        string connstring = @"server=localhost;userid=root;password=;database=Porikis;port=3306";

        private void KeyLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);
            bool Status = false;
            string email = epasts.Text;
            string Password = password.Text;

            cnn.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM lietotajs WHERE Email=@email", cnn);
            command.Parameters.AddWithValue("@email", email);

            // Execute the query and read the results
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string dbPassword = reader["Password"].ToString();
                Status = Convert.ToBoolean(reader["Status"]);

                string Vards = reader["Vards"].ToString();
                string Uzvards = reader["Uzvards"].ToString();

                Klienta_Logs klients = new Klienta_Logs(email, Vards, Uzvards);

                if (dbPassword == Password && Status)
                {
                    klients.AccEmail = email;
                    klients.AccVards = Vards;
                    klients.AccUzvards = Uzvards;
                    Close();
                    klients.Show();
                }
                else if (dbPassword == Password)
                {
                    klients.AccEmail = email;
                    klients.AccVards = Vards;
                    klients.AccUzvards = Uzvards;
                    Close();
                    klients.Show();
                }
                else
                {
                    MessageBox.Show("Kļūda sistēmā!");
                }
            }
            else
            {
                MessageBox.Show("Nepareizi ievadīts e-pasts vai parole!");
            }

            epasts.Text = string.Empty;
            password.Text = string.Empty;
            reader.Close();
            command.Dispose();
            cnn.Close(); // always close connection }
            
        }
        private void AUTO_CONNECT(object sender, RoutedEventArgs e)   //AUTO CONNECT: JADZĒŠ ĀRĀ PĒCTAM
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);
            bool Status = false;
            string email = "emils@gmail.com";
            string Password = "Emils123";

            cnn.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM lietotajs WHERE Email=@email", cnn);
            command.Parameters.AddWithValue("@email", email);

            // Execute the query and read the results
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string dbPassword = reader["Password"].ToString();
                Status = Convert.ToBoolean(reader["Status"]);

                string Vards = reader["Vards"].ToString();
                string Uzvards = reader["Uzvards"].ToString();

                Klienta_Logs klients = new Klienta_Logs(email, Vards, Uzvards);

                if (dbPassword == Password && Status)
                {
                    klients.AccEmail = email;
                    klients.AccVards = Vards;
                    klients.AccUzvards = Uzvards;
                    Close();
                    klients.Show();
                }
                else if (dbPassword == Password)
                {
                    klients.AccEmail = email;
                    klients.AccVards = Vards;
                    klients.AccUzvards = Uzvards;
                    Close();
                    klients.Show();
                }
                else
                {
                    MessageBox.Show("Kļūda sistēmā!");
                }
            }
            else
            {
                MessageBox.Show("Nepareizi ievadīts e-pasts vai parole!");
            }

            epasts.Text = string.Empty;
            password.Text = string.Empty;
            reader.Close();
            command.Dispose();
            cnn.Close();
        }

        private void Konta_izveide(object sender, RoutedEventArgs e)
        {
            Login_Grid.Visibility = Visibility.Hidden;
            Create_Grid.Visibility = Visibility.Visible;

            epasts.Text = string.Empty;
            password.Text = string.Empty;
        }

        private void Atpakaļ(object sender, RoutedEventArgs e)
        {
            Login_Grid.Visibility = Visibility.Visible;
            Create_Grid.Visibility = Visibility.Hidden;

            Vards.Text = string.Empty;
            Uzvards.Text = string.Empty;
            Nummurs.Text = string.Empty;
            Epasts.Text = string.Empty;
            Parole.Text = string.Empty;
        }

        private void Nummuru_Ievade(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Izveidot(object sender, RoutedEventArgs e)
        {

            TextBox[] Jaunie_dati = { Vards, Uzvards, Nummurs, Epasts, Parole };

            bool nav_ievadits = Jaunie_dati.Any(textBox => string.IsNullOrEmpty(textBox.Text));

            if (nav_ievadits)
            {
                MessageBox.Show("Nav ievadīti visi svarīgie dati!");
                return;
            }


            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);

            string Vards_j = Vards.Text;
            string Uzvards_j = Uzvards.Text;
            string Nummurs_j = Nummurs.Text;
            string Epasts_j = Epasts.Text;
            string Parole_j = Parole.Text;

            cnn.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM lietotajs WHERE Email=@email", cnn);
            command.Parameters.AddWithValue("@email", Epasts_j);

            // Execute the query and read the results
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Tāds e-pasts jau eksistē!");

                Epasts.Text = string.Empty;
                Parole.Text = string.Empty;
            }
            else
            {
                cnn = new MySqlConnection(connstring);

                cnn.Open();

                MySqlCommand insert = new MySqlCommand("INSERT INTO Lietotajs (Vards, Uzvards, Email, Password, Nummurs, Status) " +
                                                       "Values(@Vards, @Uzvards, @Epasts, @Parole, @Nummurs, 0)", cnn);

                insert.Parameters.AddWithValue("@Vards", Vards_j);
                insert.Parameters.AddWithValue("@Uzvards", Uzvards_j);
                insert.Parameters.AddWithValue("@Nummurs", Nummurs_j);
                insert.Parameters.AddWithValue("@Epasts", Epasts_j);
                insert.Parameters.AddWithValue("@Parole", Parole_j);

                int ievietots = insert.ExecuteNonQuery();

                if(ievietots > 0)
                {
                    MessageBox.Show("Jūsu konts ir izveidots!");

                    Login_Grid.Visibility = Visibility.Visible;
                    Create_Grid.Visibility = Visibility.Hidden;

                    Vards.Text = string.Empty;
                    Uzvards.Text = string.Empty;
                    Nummurs.Text = string.Empty;
                    Epasts.Text = string.Empty;
                    Parole.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Jūsu konts Netika izveidots!");

                    Epasts.Text = string.Empty;
                    Parole.Text = string.Empty;
                }

                reader.Close();
                command.Dispose();
                insert.Dispose();
                cnn.Close();
            }
        }
    } 
}
