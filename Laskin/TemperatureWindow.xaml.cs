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

namespace Laskin
{
    /// <summary>
    /// Interaction logic for TemperatureWindow.xaml
    /// </summary>
    public partial class TemperatureWindow : Window
    {
        double input = 0;
        public TemperatureWindow()
        {
            InitializeComponent();
        }

        private void MenuStandardClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void MenuScientificClick(object sender, RoutedEventArgs e)
        {
            ScientificWindow science = new ScientificWindow();
            science.Show();
            Close();
        }

        private void MenuExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuTemperatureClick(object sender, RoutedEventArgs e)
        {

        }

        private void btnToFahrenheit_Click(object sender, RoutedEventArgs e)
        {
            if (InFahrenheit.Text != null)
                InFahrenheit.Text = null;
            input = Convert.ToDouble(InCelcius.Text);
            InFahrenheit.Text = (Math.Round((input * 9 / 5 + 32), 2)).ToString();
        }

        private void btnToCelcius_Click(object sender, RoutedEventArgs e)
        {
            if (InCelcius.Text != null)
                InCelcius.Text = null;
            input = Convert.ToDouble(InFahrenheit.Text);
            InCelcius.Text = Math.Round(((input - 32) * 5 / 9), 2).ToString();
        }
    }
}
