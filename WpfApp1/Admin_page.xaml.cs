using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;

namespace WpfApp1
{
    public partial class Admin_page : Page
    {
        public Admin_page()
        {
            InitializeComponent();
        }

        string connstring = @"server=localhost;userid=root;password=;database=Porikis;port=3306";

        DataTable Datu_tabula;
        public int delete = 0;
        public int update = 0;

        private void DB_lietotaji(object sender, RoutedEventArgs e)
        {
            Datu_tabula = Dati_lietotajs();
            Data_Table.ItemsSource = Datu_tabula.DefaultView;
            delete = 1;
        }

        private DataTable Dati_lietotajs()
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);

            cnn.Open();

            MySqlCommand command = new MySqlCommand("SELECT Vards, Uzvards, Email, Nummurs FROM lietotajs", cnn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable Datu_tabula = new DataTable();

            adapter.Fill(Datu_tabula);

            return Datu_tabula;
        }

        private void DB_rezervacija(object sender, RoutedEventArgs e)
        {
            Datu_tabula = DB_rezervacija();
            Data_Table.ItemsSource = Datu_tabula.DefaultView;
            delete = 2;
        }

        private DataTable DB_rezervacija()
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);

            cnn.Open();

            MySqlCommand command = new MySqlCommand("SELECT Rezervacija_ID, DATE_FORMAT(Check_in, '%Y-%m-%d') AS `Sākums datums`, " +
                                                    "DATE_FORMAT(Checkout, '%Y-%m-%d') AS `Beigu datums`, " +
                                                    "Izmaksa, Lietotajs_ID, Izstaba_ID " +
                                                    "FROM rezervacija", cnn);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable Datu_tabula = new DataTable();

            adapter.Fill(Datu_tabula);

            return Datu_tabula;
        }

        private void Delete_row(object sender, RoutedEventArgs e)
        {
            if(delete == 1)
            {
                Delete_row_user(sender, e);
            }else if (delete == 2)
            {
                Delete_row_rezerve(sender, e);
            }
        }

        private void Delete_row_user(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Vai vēlaties dzest datus no tabulas lietotāji?",
                                "Dzēst datus!",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                // Atrod kuru rindu vēlamies dzēst
                Button deleteButton = (Button)sender;
                DataRowView rowView = (DataRowView)deleteButton.DataContext;
                DataRow row = rowView.Row;

                // Iegūst datus no rindas, kuru vēlamies dzēst
                string Vards = row["Vards"].ToString();
                string Uzvards = row["Uzvards"].ToString();
                string Email = row["Email"].ToString();
                string Nummurs = row["Nummurs"].ToString();


                MySqlConnection cnn;
                cnn = new MySqlConnection(connstring); // Izveido jaunu savienojumu ar datu bāzi, izmantojot savienojuma virkni

                cnn.Open();

                MySqlCommand command = new MySqlCommand("DELETE FROM lietotajs " +
                                                        "WHERE Vards = @Vards " +
                                                        "and Uzvards = @Uzvards " +
                                                        "and Email = @Email " +
                                                        "and Nummurs = @Nummurs", cnn);

                command.Parameters.AddWithValue("@Vards", Vards);
                command.Parameters.AddWithValue("@Uzvards", Uzvards);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Nummurs", Nummurs);

                command.ExecuteNonQuery(); // Izpilda SQL vaicājumu, lai dzēstu ierakstu

                DB_lietotaji(sender, e); // Atjauno datu tabulu, lai parādītu atjaunināto stāvokli
                cnn.Close();
            }
        }

        private void Delete_row_rezerve(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Vai vēlaties dzest datus no tabulas rezervācija?",
                    "Dzēst datus!",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Atrod kuru rindu vēlamies dzēst
                Button deleteButton = (Button)sender;
                DataRowView rowView = (DataRowView)deleteButton.DataContext;
                DataRow row = rowView.Row;

                // Iegūst datus no rindas, kuru vēlamies dzēst
                string Check_in = (string)row["Sākums datums"];
                string Checkout = (string)row["Beigu datums"];
                double Izmaksa = (double)row["Izmaksa"];
                string Lietotajs_ID = row["Lietotajs_ID"].ToString();
                string Izstaba_ID = row["Izstaba_ID"].ToString();

                MySqlConnection cnn;
                cnn = new MySqlConnection(connstring); // Izveido jaunu savienojumu ar datu bāzi, izmantojot savienojuma virkni

                cnn.Open();

                MySqlCommand command = new MySqlCommand("DELETE FROM rezervacija " +
                                                        "WHERE Check_in = @Check_in " +
                                                        "and Checkout = @Checkout " +
                                                        "and Izmaksa = @Izmaksa " +
                                                        "and Lietotajs_ID = @Lietotajs_ID " +
                                                        "and Izstaba_ID = @Izstaba_ID", cnn);

                command.Parameters.Add("@Check_in", MySqlDbType.DateTime).Value = Check_in;
                command.Parameters.Add("@Checkout", MySqlDbType.DateTime).Value = Checkout;
                command.Parameters.AddWithValue("@Izmaksa", Izmaksa);
                command.Parameters.AddWithValue("@Lietotajs_ID", Lietotajs_ID);
                command.Parameters.AddWithValue("@Izstaba_ID", Izstaba_ID);

                command.ExecuteNonQuery(); // Izpilda SQL vaicājumu, lai dzēstu ierakstu

                DB_rezervacija(sender, e); // Atjauno datu tabulu, lai parādītu atjaunināto stāvokli
                cnn.Close();
            }
        }
    }
}
