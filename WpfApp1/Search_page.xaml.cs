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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Search_page.xaml
    /// </summary>
    public partial class Search_page : Page
    {
        public Search_page()
        {
            InitializeComponent();
        }

        private void Country_select(object sender, RoutedEventArgs e)
        {
            Valsts_izvēle.Visibility = Visibility.Visible;
            Valsts_izvēle.Focus();
        }

        private void Valsts_izvēles_maiņa(object sender, SelectionChangedEventArgs e)
        {
            if (Valsts_izvēle.SelectedItem != null)
            {
                ListBoxItem selectedItem = (ListBoxItem)Valsts_izvēle.SelectedItem;
                Valsts_poga.Content = selectedItem.Content;
                Valsts_izvēle.Visibility = Visibility.Collapsed;
            }
        }

        private void Skaits_select(object sender, RoutedEventArgs e)
        {
            Skaits_izvēle.Visibility = Visibility.Visible;
            Skaits_izvēle.Focus();
        }

        private void Skaitu_izvēles_maiņa(object sender, SelectionChangedEventArgs e)
        {
            if (Skaits_izvēle.SelectedItem != null)
            {
                ListBoxItem selectedItem = (ListBoxItem)Skaits_izvēle.SelectedItem;
                Skaits_poga.Content = selectedItem.Content;
                Skaits_izvēle.Visibility = Visibility.Collapsed;
            }
        }
    }
}
