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
    /// <summary>
    /// Interaction logic for Rezult_Page.xaml
    /// </summary>
    public partial class Rezult_Page : Page
    {
        public string Valsts { get; set; }
        public string Skaits { get; set; }
        public bool WIFI { get; set; }
        public bool AC { get; set; }

        string connstring = @"server=localhost;userid=root;password=;database=Porikis;port=3306";
        public Rezult_Page(string Valsts,string Skaits,bool WIFI,bool AC)
        {
            InitializeComponent();
        }

        private void create_test(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < 5; i++)
            {
                RowDefinition Row = new RowDefinition();
                MainGrid.RowDefinitions.Add(Row);
                MainGrid.RowDefinitions[i].Height = new GridLength(300);
            }

            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition Col = new ColumnDefinition();
                MainGrid.ColumnDefinitions.Add(Col);
                MainGrid.ColumnDefinitions[i].Width = new GridLength(480);
            }

            MainGrid.HorizontalAlignment = HorizontalAlignment.Center;

            int row = 0;
            int col = 0;
            int maxCols = 2;
            
            for (int i = 0; i < 10; i++)
            {

                Button testpoga = new Button();
                testpoga.Content = "test poga " + (i+1);
                testpoga.Width = 450;
                testpoga.Height = 250;

                MainGrid.Children.Add(testpoga);

                Grid.SetRow(testpoga, row);
                Grid.SetColumn(testpoga, col);

                col++;
                if(col >= maxCols)
                {
                    col = 0;
                    row++;
                }
            }
        }


        //        <Button HorizontalAlignment = "Left" VerticalAlignment="Top" Width="450" Height="250" Margin="507,25,0,0">
        //    <StackPanel Height = "251" >
        //        < Grid Width="450" Height="250">
        //            <TextBlock HorizontalAlignment = "Right" Width="104" Margin="0,62,135,165">Valsts:"Nosaukums"</TextBlock>
        //            <Image Source = "/Images/Room1.jpg" Stretch="Uniform"  Margin="0,0,244,3" />
        //            <TextBlock HorizontalAlignment = "Right" Width="104" Margin="0,85,135,142"><Run Text = "Piseta" />< Run Text=":&quot;Nosaukums&quot;"/></TextBlock>
        //            <TextBlock HorizontalAlignment = "Right" Width="115" Margin="0,108,124,119"><Run Text = "Adresse" />< Run Text=":&quot;Nosaukums&quot;"/></TextBlock>
        //            <TextBlock HorizontalAlignment = "Right" Width="115" Margin="0,136,124,91"><Run Text = "Rating" />< Run Text="s: 5/5"/></TextBlock>
        //            <TextBlock HorizontalAlignment = "Right" Width="104" Margin="0,164,135,63"><Run Text = "Cena" />< Run Text=":"/><Run Text = "40$" /></ TextBlock >


        //                  < Image Source="/Images/Wifi.png" Margin="211,192,209,28" Width="30" Height="30"/>
        //            <Image Source = "/Images/AC.png" Margin="246,192,174,28" Width="30" Height="30"/>

        //        </Grid>
        //    </StackPanel>
        //</Button>




        //cnn.Open();

        //    MySqlCommand command = new MySqlCommand("SELECT * FROM iztaba WHERE Valsts=@Valsts and Skaits=@Skaits and Wifi=@WIFI and AC=@AC", cnn);
        //command.Parameters.AddWithValue("@Valsts", Valsts);
        //    command.Parameters.AddWithValue("@Skaits", Skaits);
        //    command.Parameters.AddWithValue("@WIFI", WIFI);
        //    command.Parameters.AddWithValue("@AC", AC);

    }
}
