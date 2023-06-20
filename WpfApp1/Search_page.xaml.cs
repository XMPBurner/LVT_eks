using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public string Konta_epasts;
        public int cen;
        public int pilna_cena;
        public int User_ID;
        public int Izstaba_ID;
        public string Skaits;

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

            Konta_epasts = email;
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

            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);
            String Valsts = Valsts_poga.Content.ToString();
            Skaits = Skaits_poga.Content.ToString();

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

                    Search_Grid.Visibility = Visibility.Hidden;
                    Rezult_Grid.Visibility = Visibility.Visible;

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
                MessageBox.Show("Pēc nosacijumiem nevarējam atrast rezervācijas vietu!");
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
            rat = buttonValues.Item4;
            cen = buttonValues.Item5;

            R_Valsts.Text = "Valsts: " + Valsts;
            R_Pilseta.Text = "Pilsēta: " + pil;
            R_Adresse.Text = "Adresse: " + adr;
            R_Ratings.Text = "Ratings: " + rat;
            R_Skaits.Text = "Skaits: " + Skaits;
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
                DateTime startDate = Sakum_datums.SelectedDate.Value; // Iegūst sākuma datumu no izvēlētās vērtības
                DateTime endDate = Beigu_datums.SelectedDate.Value; // Iegūst beigu datumu no izvēlētās vērtības

                // Aprēķina cenu ar izvēlētajām dienām
                TimeSpan duration = endDate - startDate; // Aprēķina laiku starp beigu un sākuma datumu
                int numberOfDays = duration.Days; // Iegūst dienu skaitu no aprēķinātā laika
                pilna_cena = cen * numberOfDays; // Aprēķina kopējo cenu, reizinot dienu skaitu ar cenu vienai dienai

                // Parāda kopējo cenu
                R_Cena.Text = "Cena: " + pilna_cena; // Atjauno tekstu
            }
        }
        private void Numuru_ievade(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Kartes_numuru_ievade(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text); // Atļauj tikai ciparu ievadi, ignorējot citus simbolus

            TextBox textBox = (TextBox)sender;

            // Iegūst tagadējo caret pozīciju
            int caretIndex = textBox.CaretIndex;

            // Izņem citas eksistējošās atstarpes
            string text = textBox.Text.Replace(" ", "");

            // Ievieto atstarpi pēc katras ceturto numura
            int spaceCount = text.Length / 4;
            for (int i = 1; i <= spaceCount; i++)
            {
                int insertPosition = i * 4 + (i - 1);
                text = text.Insert(insertPosition, " ");
            }

            // Atjauno tekstu ar ievietotajām atstarpēm
            textBox.Text = text;

            // Novieto Caret uz sākotnējo indeksu, kas pielāgots pievienotajām atstarpēm
            int adjustedCaretIndex = caretIndex + (caretIndex / 4);
            textBox.CaretIndex = adjustedCaretIndex;
        }

        private void Kartes_CVC_ievade(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text); // Atļauj tikai ciparu ievadi, ignorējot citus simbolus
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
            int Kartes_termina_limits = 7;

            TextBox[] rezervetie_dati = { Rezevetaja_Vards, Rezevetaja_Uzvards, Rezevetaja_Numurs,
                                  Rezevetaja_Epasts, Kartes_numurs, Kartes_CVC, Kartes_datums };
            DatePicker[] rezervetie_datumi = { Sakum_datums, Beigu_datums };

            bool nav_ievadits = rezervetie_dati.Any(textBox => string.IsNullOrEmpty(textBox.Text))
                               || rezervetie_datumi.Any(datePicker => datePicker.SelectedDate == null);

            if (nav_ievadits)
            {
                MessageBox.Show("Nav ievadīti visi svarīgie dati!");
                return;
            }

            if (Rezevetaja_Numurs.Text.Length < Nummurs_limits ||
                Kartes_numurs.Text.Length < Kartes_nummura_limits ||
                Kartes_CVC.Text.Length < Kartes_CVC_limits ||
                Kartes_datums.Text.Length < Kartes_termina_limits)
            {
                MessageBox.Show("Kāds laukums nav līdz galam aizpildīts!");
                return;
            }

            string Vards = Rezevetaja_Vards.Text;
            string Uzvards = Rezevetaja_Uzvards.Text;
            string Nummurs = Rezevetaja_Numurs.Text;
            string Epasts = Rezevetaja_Epasts.Text;
            DateTime sakums = Sakum_datums.SelectedDate.Value;
            DateTime beigas = Beigu_datums.SelectedDate.Value;
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
                        MessageBox.Show("Nevar atrast kontu. nevaram izveidot rezervāciju!");
                        return;
                    }
                }

                MySqlCommand izstabaCommand = new MySqlCommand("SELECT Izstaba_ID FROM Izstaba WHERE Cena = @Cena AND Ratings = @Ratings", cnn);
                izstabaCommand.Parameters.AddWithValue("@Cena", cen);
                izstabaCommand.Parameters.AddWithValue("@Ratings", rat);

                int Izstaba_ID = Convert.ToInt32(izstabaCommand.ExecuteScalar());
                if (Izstaba_ID == 0)
                {
                    MessageBox.Show("Nevarēja atrast telpu. Nevaram izveidot rezervāciju!");
                    return;
                }

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO Rezervacija (Check_in, Checkout, Izmaksa, Lietotajs_ID, Izstaba_ID) " +
                                                      "VALUES (@Check_in, @Checkout, @Izmaksa, @Lietotajs_ID, @Izstaba_ID)", cnn);

                insertCommand.Parameters.Add("@Check_in", MySqlDbType.DateTime).Value = sakums;
                insertCommand.Parameters.Add("@Checkout", MySqlDbType.DateTime).Value = beigas;
                insertCommand.Parameters.AddWithValue("@Izmaksa", pilna_cena);
                insertCommand.Parameters.AddWithValue("@Lietotajs_ID", User_ID);
                insertCommand.Parameters.AddWithValue("@Izstaba_ID", Izstaba_ID);

                int ievietots = insertCommand.ExecuteNonQuery();

                if (ievietots > 0)
                {
                    MessageBox.Show("Jūsu rezervacija ir izveidota!");
                }
                else
                {
                    MessageBox.Show("Jūsu rezervaciju nevarēja izveidota!");
                }
            }
        }
    }
}
