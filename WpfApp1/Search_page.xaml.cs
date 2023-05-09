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
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class Search_page : Page
    {
        string connstring = @"server=localhost;userid=root;password=;database=Porikis;port=3306";
        public Search_page()
        {
            InitializeComponent();
            //Rezult_Grid.Visibility = Visibility.Hidden;
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

        private void WIFI_ir(object sender, RoutedEventArgs e)
        {
            bool isChecked = WIFI.IsChecked == true;
            bool Wifi = true;
        }

        private void WIFI_nav(object sender, RoutedEventArgs e)
        {
            bool isChecked = WIFI.IsChecked == false;
            bool Wifi = false;
        }

        private void AC_ir(object sender, RoutedEventArgs e)
        {
            bool isChecked = AC.IsChecked == true;
            bool Ac = true;
        }

        private void AC_nav(object sender, RoutedEventArgs e)
        {
            bool isChecked = AC.IsChecked == false;
            bool Ac = false;
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            Search_Grid.Visibility = Visibility.Hidden;

            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);
            String Valsts = Valsts_poga.Content.ToString();
            String Skaits = Skaits_poga.Content.ToString();

            bool Wifi;
            bool Ac;

            int row = 0;
            int col = 0;
            int maxCols = 2;

            cnn.Open();

            MySqlCommand command = new MySqlCommand("SELECT h.Hotelis_ID, h.Valsts, h.Pilsēta, h.Adresse, i.Izstaba_ID, i.Skaits, i.Wifi, i.AC, i.Siltums, i.Ratings, i.Cena " +
                                                    "FROM izstaba AS i LEFT JOIN hotelis AS h ON i.Hotelis_ID = h.Hotelis_ID " +
                                                    "WHERE h.Valsts = @Valsts and i.Skaits = @Skaits", cnn);
            command.Parameters.AddWithValue("@Valsts", Valsts);
            command.Parameters.AddWithValue("@Skaits", Skaits);

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                for (int i = 0; i < 5; i++)
                {
                    RowDefinition Row = new RowDefinition();
                    Rezult_Grid.RowDefinitions.Add(Row);
                    Rezult_Grid.RowDefinitions[i].Height = new GridLength(300);
                }

                for (int i = 0; i < 2; i++)
                {
                    ColumnDefinition Col = new ColumnDefinition();
                    Rezult_Grid.ColumnDefinitions.Add(Col);
                    Rezult_Grid.ColumnDefinitions[i].Width = new GridLength(480);
                }

                Rezult_Grid.HorizontalAlignment = HorizontalAlignment.Center;

                for (int i = 0; i < 10; i++)
                {

                    Button testpoga = new Button();
                    testpoga.Content = "test poga " + (i + 1);
                    testpoga.Width = 450;
                    testpoga.Height = 250;

                    Rezult_Grid.Children.Add(testpoga);

                    Grid.SetRow(testpoga, row);
                    Grid.SetColumn(testpoga, col);

                    col++;
                    if (col >= maxCols)
                    {
                        col = 0;
                        row++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Kautkas neiet");
            }
        }

        private void create_test(object sender, RoutedEventArgs e)
        {
            Search_Grid.Visibility = Visibility.Hidden;

            int row = 0;
            int col = 0;
            int maxCols = 2;

            for (int i = 0; i < 5; i++)
            {
                RowDefinition Row = new RowDefinition();
                Rezult_Grid.RowDefinitions.Add(Row);
                Rezult_Grid.RowDefinitions[i].Height = new GridLength(300);
            }

            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition Col = new ColumnDefinition();
                Rezult_Grid.ColumnDefinitions.Add(Col);
                Rezult_Grid.ColumnDefinitions[i].Width = new GridLength(480);
            }

            Rezult_Grid.HorizontalAlignment = HorizontalAlignment.Center;

            for (int i = 0; i < 10; i++)
            {

                Button testpoga = new Button();
                testpoga.Content = "test poga " + (i + 1);
                testpoga.Width = 450;
                testpoga.Height = 250;

                Rezult_Grid.Children.Add(testpoga);

                Grid.SetRow(testpoga, row);
                Grid.SetColumn(testpoga, col);

                col++;
                if (col >= maxCols)
                {
                    col = 0;
                    row++;
                }
            }
        }


        //  <Button HorizontalAlignment = "Left" VerticalAlignment="Top" Width="450" Height="250" Margin="507,25,0,0">
        //      <StackPanel Height = "251" >
        //          < Grid Width="450" Height="250">
        //              <TextBlock HorizontalAlignment = "Right" Width="104" Margin="0,62,135,165">Valsts:"Nosaukums"</TextBlock>
        //              <Image Source = "/Images/Room1.jpg" Stretch="Uniform"  Margin="0,0,244,3" />
        //              <TextBlock HorizontalAlignment = "Right" Width="104" Margin="0,85,135,142"><Run Text = "Piseta" />< Run Text=":&quot;Nosaukums&quot;"/></TextBlock>
        //              <TextBlock HorizontalAlignment = "Right" Width="115" Margin="0,108,124,119"><Run Text = "Adresse" />< Run Text=":&quot;Nosaukums&quot;"/></TextBlock>
        //              <TextBlock HorizontalAlignment = "Right" Width="115" Margin="0,136,124,91"><Run Text = "Rating" />< Run Text="s: 5/5"/></TextBlock>
        //              <TextBlock HorizontalAlignment = "Right" Width="104" Margin="0,164,135,63"><Run Text = "Cena" />< Run Text=":"/><Run Text = "40$" /></ TextBlock >
        
        //              <Image Source="/Images/Wifi.png" Margin="211,192,209,28" Width="30" Height="30"/>
        //              <Image Source = "/Images/AC.png" Margin="246,192,174,28" Width="30" Height="30"/>

        //          </Grid>
        //      </StackPanel>
        //  </Button>






    }
}
