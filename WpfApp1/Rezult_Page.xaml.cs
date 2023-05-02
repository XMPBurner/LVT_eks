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
    /// Interaction logic for Rezult_Page.xaml
    /// </summary>
    public partial class Rezult_Page : Page
    {
        public Rezult_Page()
        {
            InitializeComponent();
        }

        private void create_test(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < 2; i++)
            {
                RowDefinition Row = new RowDefinition();
                MainGrid.RowDefinitions.Add(Row);
                MainGrid.RowDefinitions[i].Height = new GridLength(450);
            }

            for (int i = 0; i < 5; i++)
            {
                ColumnDefinition Col = new ColumnDefinition();
                MainGrid.ColumnDefinitions.Add(Col);
                MainGrid.ColumnDefinitions[i].Width = new GridLength(250);
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
    }
}
