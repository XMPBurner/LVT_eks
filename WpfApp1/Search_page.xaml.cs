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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class Search_page : Page
    {
        public string AccEmail { get; set; }
        public string AccVards { get; set; }
        public string AccUzvards { get; set; }

        public bool Wifi = false;
        public bool Ac = false;
        public int cen;

        string connstring = @"server=localhost;userid=root;password=;database=Porikis;port=3306";
        public Search_page(string email, string Vards, string Uzvards)
        {
            InitializeComponent();
            Search_Grid.Visibility = Visibility.Visible;
            Rezult_Grid.Visibility = Visibility.Hidden;
            Rezervet.Visibility = Visibility.Hidden;

            Rezevetaja_Vards.Text = Vards;
            Rezevetaja_Uzvards.Text = Uzvards;
            Rezevetaja_Epasts.Text = email;
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
            Wifi = true;
        }

        private void WIFI_nav(object sender, RoutedEventArgs e)
        {
            bool isChecked = WIFI.IsChecked == false;
            Wifi = false;
        }

        private void AC_ir(object sender, RoutedEventArgs e)
        {
            bool isChecked = AC.IsChecked == true;
            Ac = true;
        }

        private void AC_nav(object sender, RoutedEventArgs e)
        {
            bool isChecked = AC.IsChecked == false;
            Ac = false;
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            Search_Grid.Visibility = Visibility.Hidden;
            Rezult_Grid.Visibility = Visibility.Visible;

            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);
            String Valsts = Valsts_poga.Content.ToString();
            String Skaits = Skaits_poga.Content.ToString();

            int row = 0;
            int col = 0;
            int maxCols = 2;

            cnn.Open();

            MySqlCommand countcommand = new MySqlCommand("SELECT COUNT(*) " +
                                                        "FROM izstaba AS i LEFT JOIN hotelis AS h ON i.Hotelis_ID = h.Hotelis_ID " +
                                                        "WHERE h.Valsts = @Valsts AND i.Skaits = @Skaits AND i.Wifi = " + Wifi + " and i.AC = " + Ac + " ", cnn);
            countcommand.Parameters.AddWithValue("@Valsts", Valsts);
            countcommand.Parameters.AddWithValue("@Skaits", Skaits);

            int count = Convert.ToInt32(countcommand.ExecuteScalar());

            if (count > 0)
            {
                MySqlCommand command = new MySqlCommand("SELECT h.Pilsēta, h.Adresse, i.Ratings, i.Cena " +
                                                        "FROM izstaba AS i LEFT JOIN hotelis AS h ON i.Hotelis_ID = h.Hotelis_ID " +
                                                        "WHERE h.Valsts = @Valsts AND i.Skaits = @Skaits AND i.Wifi = " + Wifi + " and i.AC = " + Ac + " ", cnn);
                command.Parameters.AddWithValue("@Valsts", Valsts);
                command.Parameters.AddWithValue("@Skaits", Skaits);

                using (MySqlDataReader reader = command.ExecuteReader())
                {

                    for (int i = 0; i < (count + 1) / 2; i++)
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

                    for (int i = 0; i < count; i++)
                    {
                        if (reader.Read())
                        {
                            string pil = reader.GetString(0);
                            string adr = reader.GetString(1);
                            string rat = reader["Ratings"].ToString();
                            cen = reader.GetInt32(reader.GetOrdinal("Cena"));

                            Button Naktsmītne = new Button();
                            Naktsmītne.Click += izvele;
                            Naktsmītne.Tag = new Tuple<string, string, string, string, int>(Valsts, pil, adr, rat, cen);

                            StackPanel stackPanel = new StackPanel();

                            TextBlock text1 = new TextBlock();
                            TextBlock text2 = new TextBlock();
                            TextBlock text3 = new TextBlock();
                            TextBlock text4 = new TextBlock();
                            TextBlock text5 = new TextBlock();

                            stackPanel.Children.Add(text1);
                            stackPanel.Children.Add(text2);
                            stackPanel.Children.Add(text3);
                            stackPanel.Children.Add(text4);
                            stackPanel.Children.Add(text5);

                            Naktsmītne.Width = 450;
                            Naktsmītne.Height = 250;

                            text1.Text = "Valsts:  " + Valsts;
                            text2.Text = "Pilsēta: " + pil;
                            text3.Text = "Adresse: " + adr;
                            text4.Text = "Rating:  " + rat;
                            text5.Text = "Cena:    " + cen;

                            Naktsmītne.Content = stackPanel;
                            Rezult_Grid.Children.Add(Naktsmītne);

                            Grid.SetRow(Naktsmītne, row);
                            Grid.SetColumn(Naktsmītne, col);

                            col++;
                            if (col >= maxCols)
                            {
                                col = 0;
                                row++;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Kautkas neiet");
            }
            cnn.Close();
        }

        private void izvele(object sender, RoutedEventArgs e)
        {

            Button izveleta_poga = (Button)sender;

            Tuple<string, string, string, string, int> buttonValues = (Tuple<string, string, string, string, int>)izveleta_poga.Tag;

            string Valsts = buttonValues.Item1;
            string pil = buttonValues.Item2;
            string adr = buttonValues.Item3;
            string rat = buttonValues.Item4;
            cen = buttonValues.Item5;

            R_Valsts.Text = "Valsts: " + Valsts;
            R_Pilseta.Text = "Pilsēta: " + pil;
            R_Adresse.Text = "Adresse: " + adr;
            R_Ratings.Text = "Ratings: " + rat;
            R_Cena.Text = "Cena: " + cen;

            Rezult_Grid.Visibility = Visibility.Hidden;
            Rezervet.Visibility = Visibility.Visible;


        }

        private void Atpakal_meklet(object sender, RoutedEventArgs e)
        {
            Rezult_Grid.Visibility = Visibility.Visible;
            Rezervet.Visibility = Visibility.Hidden;
        }

        private void Sakum_Datumu_maina(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = Sakum_datums.SelectedDate ?? DateTime.MinValue;
            Beigu_datums.DisplayDateStart = selectedDate.AddDays(1);

            Cenu_aprekins();
        }

        private void Beigu_Datumu_maina(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = Beigu_datums.SelectedDate ?? DateTime.MaxValue;
            Sakum_datums.DisplayDateEnd = selectedDate.AddDays(-1);

            Cenu_aprekins();
        }

        private void Cenu_aprekins()
        {
            if (Sakum_datums.SelectedDate.HasValue && Beigu_datums.SelectedDate.HasValue)
            {
                DateTime startDate = Sakum_datums.SelectedDate.Value;
                DateTime endDate = Beigu_datums.SelectedDate.Value;

                // Calculate the price based on the number of days
                TimeSpan duration = endDate - startDate;
                int numberOfDays = duration.Days;
                int pilna_cena = cen * numberOfDays;

                // Display the calculated price
                R_Cena.Text = "Cena: " + pilna_cena;
            }
        }

        private void Kartes_nummuru_ievade(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

            TextBox textBox = (TextBox)sender;

            // Get the current caret position
            int caretIndex = textBox.CaretIndex;

            // Remove any existing spaces from the text
            string text = textBox.Text.Replace(" ", "");

            // Insert a space every fourth character
            int spaceCount = text.Length / 4;
            for (int i = 1; i <= spaceCount; i++)
            {
                int insertPosition = i * 4 + (i - 1);
                text = text.Insert(insertPosition, " ");
            }

            // Update the TextBox text with the formatted string
            textBox.Text = text;

            // Set the caret position to the original index, adjusted for added spaces
            int adjustedCaretIndex = caretIndex + (caretIndex / 4);
            textBox.CaretIndex = adjustedCaretIndex;
        }

        private void Kartes_CVC_ievade(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Kartes_terminu_ievade(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

            TextBox textBox = (TextBox)sender;

            // Get the current caret position
            int caretIndex = textBox.CaretIndex;

            // Remove any existing spaces and slashes from the text
            string text = textBox.Text.Replace(" ", "").Replace("/", "");

            // Insert a space, slash, and another space after the second number
            if (text.Length >= 2)
            {
                text = text.Insert(2, " / ");
            }

            // Update the TextBox text with the formatted string
            textBox.Text = text;

            // Set the caret position to the original index, adjusted for added characters
            int adjustedCaretIndex = caretIndex + (caretIndex / 2) + 2;
            textBox.CaretIndex = adjustedCaretIndex;
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
