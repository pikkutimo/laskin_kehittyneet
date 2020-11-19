using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Laskin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * To review:
         * - operations with multiple power, sqrt etc.
         */
        double input = 0;
        double cache = 0;
        string operation = null;
        bool hasResult = false;
        bool hasCache = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (txtDisplay.Text == "0")
                txtDisplay.Text = button.Content.ToString();
            else if (hasResult)
            {
                // If after pressing the equals sign and getting the result, the user start inputting new number
                // => Clear everything
                equationDisplay.Text = null;
                input = 0;
                operation = null;
                hasResult = false;
                txtDisplay.Text = button.Content.ToString();
            }
            else
                txtDisplay.Text += button.Content.ToString();
        }

        private void btnPosNeg_Click(object sender, RoutedEventArgs e)
        {
            if (hasResult)
            {
                txtDisplay.Text = (Convert.ToDouble(input) * -1.0).ToString();
                equationDisplay.Text = null;
                input = 0;
                operation = null;
                hasResult = false;
            }
            txtDisplay.Text = (Convert.ToDouble(txtDisplay.Text) * -1.0).ToString();
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            if (hasResult)
            {
                equationDisplay.Text = null;
                input = 0;
                operation = null;
                hasResult = false;
            }

            if (txtDisplay.Text == "0")
                txtDisplay.Text = "0.";
            else if (txtDisplay.Text.Contains("."))
                txtDisplay.Text = txtDisplay.Text;
            else
                txtDisplay.Text += ".";
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear everything
            txtDisplay.Text = "0";
            input = 0;
            cache = 0;
            operation = null;
            equationDisplay.Text = null;
            hasResult = false;
            hasCache = false;
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text.Length > 1)
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            else
                txtDisplay.Text = "0";
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text != "0")
            {
                //3.
                // You can't crash the program by erasing txtDisplay.Text
                Calculate();
                hasResult = true;
                txtDisplay.Text = input.ToString();
            }
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {
            if (operation == null)
            {
                input = Convert.ToDouble(txtDisplay.Text) * 0.01;
                equationDisplay.Text = txtDisplay.Text + "%";
                txtDisplay.Text = input.ToString();
            }
            else
            {
                switch (operation)
                {
                    case "+":
                        input += Convert.ToDouble(txtDisplay.Text) / 100 * input;
                        break;
                    case "-":
                        input -= Convert.ToDouble(txtDisplay.Text) / 100 * input;
                        break;
                    case "*":
                        input *= Convert.ToDouble(txtDisplay.Text) / 100;
                        break;
                    case "/":
                        input /= Convert.ToDouble(txtDisplay.Text) / 100;
                        break;
                }

                equationDisplay.Text = equationDisplay.Text + txtDisplay.Text + "%";
                txtDisplay.Text = input.ToString();
                hasResult = true;
                operation = null;
            }
        }

        private void Calculate()
        {
            if (input == 0)
                equationDisplay.Text = null;

            if (!hasCache)
            {
                cache = Convert.ToDouble(txtDisplay.Text);
                equationDisplay.Text += txtDisplay.Text;
            }

            switch (operation)
            {
                case "+":
                    input += cache;
                    break;
                case "-":
                    input -= cache;
                    break;
                case "*":
                    input *= cache;
                    break;
                case "/":
                    if (cache != 0)
                        input /= cache;
                    break;
            }
        }

        private void btn1x_Click(object sender, RoutedEventArgs e)
        {
            if (operation != null)
            {
                equationDisplay.Text = equationDisplay.Text + "1/" + txtDisplay.Text;
                cache = 1 / Convert.ToDouble(txtDisplay.Text);
                hasCache = true;
                Calculate();
                txtDisplay.Text = Convert.ToString(cache);
                operation = null;
            }
            else
            {
                input = 1 / Convert.ToDouble(txtDisplay.Text);
                equationDisplay.Text = equationDisplay.Text + "1/" + txtDisplay.Text;
                txtDisplay.Text = Convert.ToString(input);
            }
        }

        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
            if (operation != null)
            {
                equationDisplay.Text = equationDisplay.Text + txtDisplay.Text + "^2";
                cache = Math.Pow(Convert.ToDouble(txtDisplay.Text), 2.0);
                txtDisplay.Text = Convert.ToString(cache);
                hasCache = true;
                Calculate();
                operation = null;
            }
            else
            {
                input = Math.Pow(Convert.ToDouble(txtDisplay.Text), 2.0);
                equationDisplay.Text = txtDisplay.Text + "^2";
                txtDisplay.Text = Convert.ToString(input);
            }
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (operation != null)
            {
                equationDisplay.Text = equationDisplay.Text + "sqrt(" + txtDisplay.Text + ")";
                cache = Math.Sqrt(Convert.ToDouble(txtDisplay.Text));
                hasCache = true;
                txtDisplay.Text = Convert.ToString(cache);
                Calculate();
                operation = null;
            }
            else
            {
                input = Math.Sqrt(Convert.ToDouble(txtDisplay.Text));
                equationDisplay.Text = "sqrt(" + txtDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }
        }

        private void btnArithmetic_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (operation == null)
            {
                equationDisplay.Text = txtDisplay.Text;
                input = Convert.ToDouble(txtDisplay.Text);
            }
            else if (hasResult)
            {
                equationDisplay.Text = input.ToString();
                hasResult = false;
            }
            else
            {
                //If there is already input & operation and the user presses arithmetic operator again
                // => calculate the existing operation as input
                Calculate();
            }

            operation = button.Content.ToString();
            equationDisplay.Text += operation;
            txtDisplay.Text = "0";

        }

        private void MenuScientificClick(object sender, RoutedEventArgs e)
        {
            ScientificWindow science = new ScientificWindow();
            science.Show();
            Close();
        }

        private void MenuStandardClick(object sender, RoutedEventArgs e)
        {

        }

        private void MenuTemperatureClick(object sender, RoutedEventArgs e)
        {
            TemperatureWindow temperature = new TemperatureWindow();
            temperature.Show();
            Close();
        }

        private void MenuExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
