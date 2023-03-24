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

        string connstring = @"server=localhost;userid=Porikis;password=admin;database=Porikis;port=3306";

        private void btnconnect(object sender, EventArgs e)
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);
            bool lv = false;
            string username = txtUser.Text;
            string password = txtPass.Text;
            cnn.Open();

            MySqlCommand command = new MySqlCommand("SELECT Password, lv FROM logindb WHERE username=@username", cnn);
            command.Parameters.AddWithValue("@username", username);

            // Execute the query and read the results
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string dbPassword = reader.GetString(0);
                lv = reader.GetBoolean(1);

                if (dbPassword == password && lv)
                {
                    MessageBox.Show("Login successful as admin!");
                }
                else if (dbPassword == password)
                {
                    MessageBox.Show("Login successful!");
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }

            txtUser.Text = string.Empty;
            txtPass.Text = string.Empty;
            reader.Close();
            command.Dispose();
            cnn.Close(); // always close connection }
            
        }
    } 
}
