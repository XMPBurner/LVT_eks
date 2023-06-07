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

        private void DB_lietotaji(object sender, RoutedEventArgs e)
        {
            Datu_tabula = Dati_lietotajs();
            Data_Table.ItemsSource = Datu_tabula.DefaultView;
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
        }

        private DataTable DB_rezervacija()
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);

            cnn.Open();

            MySqlCommand command = new MySqlCommand("SELECT Rezervacija_ID, DATE_FORMAT(Check_in, '%Y-%m-%d %H:%i:%s') AS `Sākums datums`, " +
                                                    "DATE_FORMAT(Checkout, '%Y-%m-%d %H:%i:%s') AS `Beigu datums`, " +
                                                    "Izmaksa, Lietotajs_ID, Izstaba_ID " +
                                                    "FROM rezervacija", cnn);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable Datu_tabula = new DataTable();

            adapter.Fill(Datu_tabula);

            return Datu_tabula;
        }

        private void Delete_row(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            DataRowView rowView = (DataRowView)deleteButton.DataContext;
            DataRow row = rowView.Row;

            string Vards = row["Vards"].ToString();
            string Uzvards = row["Uzvards"].ToString();
            string Email = row["Email"].ToString();
            string Nummurs = row["Nummurs"].ToString();

            MySqlConnection cnn;
            cnn = new MySqlConnection(connstring);

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

            command.ExecuteNonQuery();

            Data_Table.Items.Refresh();
            cnn.Close();
        }
    }
}
