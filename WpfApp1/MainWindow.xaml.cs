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
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        string connstring = @"server=localhost;userid=Porikis;password=admin;database=Porikis;port=3306";

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
                    MessageBox.Show("kautkas nav.");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }

            epasts.Text = string.Empty;
            password.Text = string.Empty;
            reader.Close();
            command.Dispose();
            cnn.Close(); // always close connection }
            
        }
    } 
}
