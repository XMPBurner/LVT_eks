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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Klienta_Logs.xaml
    /// </summary>
    public partial class Klienta_Logs : Window
    {
        public string AccEmail { get; set; }
        public string AccVards { get; set; }
        public string AccUzvards { get; set; }
        public bool AccStatus { get; set; }

        private string email;
        private string Vards;
        private string Uzvards;

        public Klienta_Logs(string email, string Vards, string Uzvards, bool Status)
        {
            InitializeComponent();

            this.email = email;
            this.Vards = Vards;
            this.Uzvards = Uzvards;


            MainFrame.Navigate(new Search_page(email, Vards, Uzvards));

            if (!Status)
            {
                Admin_status.Visibility = Visibility.Visible;
            }
            else
            {
                Admin_status.Visibility = Visibility.Hidden;
            }

            Liet_email.Text = email;
            Liet_vards.Text = Vards + " " + Uzvards;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Admin_nav(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Admin_page());
        }

        private void Search_nav(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Search_page(email, Vards, Uzvards));
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow Login = new MainWindow();

            Close();
            Login.Show();
        }
    }
}
