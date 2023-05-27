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
        public string rat;
        public int cen;
        public int pilna_cena;
        public int User_ID;
        public int Izstaba_ID;

        string connstring = @"server=localhost;userid=root;password=;database=Porikis;port=3306";
        public Search_page(string email, string Vards, string Uzvards)
        {
            InitializeComponent();
            Search_Grid.Visibility = Visibility.Visible;
            Rezult_Grid.Visibility = Visibility.Hidden;
            Rezerve.Visibility = Visibility.Hidden;

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
            int cen = buttonValues.Item5;

            R_Valsts.Text = "Valsts: " + Valsts;
            R_Pilseta.Text = "Pilsēta: " + pil;
            R_Adresse.Text = "Adresse: " + adr;
            R_Ratings.Text = "Ratings: " + rat;
            R_Cena.Text = "Cena: " + cen;

            Rezult_Grid.Visibility = Visibility.Hidden;
            Rezerve.Visibility = Visibility.Visible;

        }

        private void Atpakal_meklet(object sender, RoutedEventArgs e)
        {
            Rezult_Grid.Visibility = Visibility.Visible;
            Rezerve.Visibility = Visibility.Hidden;
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

                // Aprēķina Cenu ar izvēlētām dienān
                TimeSpan duration = endDate - startDate;
                int numberOfDays = duration.Days;
                int pilna_cena = cen * numberOfDays;

                // Parāda kopējo cenu
                R_Cena.Text = "Cena: " + pilna_cena;
            }
        }
        private void Nummuru_ievade(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Kartes_nummuru_ievade(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

            TextBox textBox = (TextBox)sender;

            // Iegūst tagadējo caret pozīciju
            int caretIndex = textBox.CaretIndex;

            // Izņem citas eksistējošās atstarpes
            string text = textBox.Text.Replace(" ", "");

            // Ievieto atstarpi pēc katru ceturto nummuru
            int spaceCount = text.Length / 4;
            for (int i = 1; i <= spaceCount; i++)
            {
                int insertPosition = i * 4 + (i - 1);
                text = text.Insert(insertPosition, " ");
            }

            // Atjauno tekstu
            textBox.Text = text;

            // Novieto Caret uz sākotnējo indeksu, kas pielāgots pievienotajām atstarpēm
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

            // Iegūst tagadējo caret pozīciju
            int caretIndex = textBox.CaretIndex;

            // Noņem citas eksistējošas atstarpes un slīpsvītras no teksta
            string text = textBox.Text.Replace(" ", "").Replace("/", "");

            // Ievieto " / " pēc ortā uzrakstītā nummura
            if (text.Length >= 2)
            {
                text = text.Insert(2, " / ");
            }

            // Atjaunina tekstu
            textBox.Text = text;

            // Novieto Caret uz sākotnējo indeksu, kas pielāgots pievienotajām atstarpēm
            int adjustedCaretIndex = caretIndex + (caretIndex / 2) + 2;
            textBox.CaretIndex = adjustedCaretIndex;
        }

        private void Rezervet(object sender, RoutedEventArgs e)
        {
            int Nummurs_limits = 8;
            int Kartes_nummura_limits = 19;
            int Kartes_CVC_limits = 3;
            int Kartes_termina_limits = 3;

            TextBox[] rezervetie_dati = { Rezevetaja_Vards, Rezevetaja_Uzvards, Rezevetaja_Nummurs,
                                  Rezevetaja_Epasts, Kartes_nummurs, Kartes_CVC, Kartes_datums };
            DatePicker[] rezervetie_datumi = { Sakum_datums, Beigu_datums };

            bool nav_ievadits = rezervetie_dati.Any(textBox => string.IsNullOrEmpty(textBox.Text))
                               || rezervetie_datumi.Any(datePicker => datePicker.SelectedDate == null);

            if (nav_ievadits)
            {
                MessageBox.Show("Nav ievadīti visi svarīgie dati!");
                return;
            }

            if (Rezevetaja_Nummurs.Text.Length < Nummurs_limits ||
                Kartes_nummurs.Text.Length < Kartes_nummura_limits ||
                Kartes_CVC.Text.Length < Kartes_CVC_limits ||
                Kartes_datums.Text.Length < Kartes_termina_limits)
            {
                MessageBox.Show("Kāds laukums nav līdz galam aizpildīts!");
                return;
            }

            string Vards = Rezevetaja_Vards.Text;
            string Uzvards = Rezevetaja_Uzvards.Text;
            string Nummurs = Rezevetaja_Nummurs.Text;
            string Epasts = Rezevetaja_Epasts.Text;
            string Ratings = Rezevetaja_Epasts.Text;
            string Konta_epasts = AccEmail;
            int Cena = pilna_cena;
            DateTime sakums = Sakum_datums.SelectedDate.Value;
            DateTime beigas = Sakum_datums.SelectedDate.Value;
            long Sakum_rezervacijas_datums = new DateTimeOffset(sakums).ToUnixTimeSeconds();
            long Beig_rezervacijas_datums = new DateTimeOffset(beigas).ToUnixTimeSeconds();

            int User_ID = 0;

            using (MySqlConnection cnn = new MySqlConnection(connstring))
            {
                cnn.Open();

                MySqlCommand userCommand = new MySqlCommand("SELECT Lietotajs_ID FROM lietotajs WHERE Email = @Email", cnn);
                userCommand.Parameters.AddWithValue("@Email", Konta_epasts);

                using (MySqlDataReader userReader = userCommand.ExecuteReader())
                {
                    if (userReader.Read())
                    {
                        User_ID = Convert.ToInt32(userReader["Lietotajs_ID"]);
                    }
                    else
                    {
                        MessageBox.Show("Invalid user email. Cannot create reservation.");
                        return;
                    }
                }

                MySqlCommand izstabaCommand = new MySqlCommand("SELECT Izstaba_ID FROM Izstaba WHERE Cena = @Cena AND Ratings = @Ratings AND AC = @AC AND Wifi = @Wifi", cnn);
                izstabaCommand.Parameters.AddWithValue("@Cena", Cena);
                izstabaCommand.Parameters.AddWithValue("@Ratings", rat);
                izstabaCommand.Parameters.AddWithValue("@AC", Ac);
                izstabaCommand.Parameters.AddWithValue("@Wifi", Wifi);

                int Izstaba_ID = Convert.ToInt32(izstabaCommand.ExecuteScalar());
                if (Izstaba_ID == 0)
                {
                    MessageBox.Show("No matching room found. Cannot create reservation.");
                    return;
                }

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO Rezervacija (Check_in, Checkout, Izmaksa, Lietotajs_ID, Izstaba_ID) " +
                                                              "VALUES (@Check_in, @Checkout, @Izmaksa, @Lietotajs_ID, @Izstaba_ID)", cnn);

                insertCommand.Parameters.AddWithValue("@Check_in", Sakum_rezervacijas_datums);
                insertCommand.Parameters.AddWithValue("@Checkout", Beig_rezervacijas_datums);
                insertCommand.Parameters.AddWithValue("@Izmaksa", Cena);
                insertCommand.Parameters.AddWithValue("@Lietotajs_ID", User_ID);
                insertCommand.Parameters.AddWithValue("@Izstaba_ID", Izstaba_ID);

                int ievietots = insertCommand.ExecuteNonQuery();

                if (ievietots > 0)
                {
                    MessageBox.Show("Jūsu rezervacija ir izveidota!");
                }
                else
                {
                    MessageBox.Show("Jūsu rezervacija Netika izveidota!");
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
