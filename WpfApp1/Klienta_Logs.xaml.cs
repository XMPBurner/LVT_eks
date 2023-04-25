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
        public Klienta_Logs(string email, string Vards, string Uzvards)
        {
            InitializeComponent();

            Liet_email.Text = email;
            Liet_vards.Text = Vards + " " + Uzvards;
        }
        
    }
}
