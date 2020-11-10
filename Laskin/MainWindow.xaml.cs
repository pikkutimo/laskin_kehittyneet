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
        double input = 0;
        double cache = 0;
        string operation = null;
        bool hasResult = false;
        bool hasPercent = false;

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

            hasPercent = false;
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
                txtDisplay.Text = "0";
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
            operation = null;
            equationDisplay.Text = null;
            hasResult = false;
            hasPercent = false;
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text.Length > 1)
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            else
                txtDisplay.Text = null;
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            equationDisplay.Text += operation;

            if (!hasResult)
            {
                cache = Convert.ToDouble(txtDisplay.Text);
                equationDisplay.Text = equationDisplay.Text + txtDisplay.Text;
            }
            else
                equationDisplay.Text = equationDisplay.Text + cache.ToString();

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
                    input /= cache;
                    break;
            }

            txtDisplay.Text = input.ToString();
            hasResult = true;
            hasPercent = false;
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {
            if (operation == null)
            {
                input = Convert.ToDouble(txtDisplay.Text) * 0.01;
                equationDisplay.Text = txtDisplay.Text + "%";
                txtDisplay.Text = input.ToString();
            }
            else if (!hasPercent)
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

                equationDisplay.Text = equationDisplay.Text + operation + txtDisplay.Text + "%";
                txtDisplay.Text = input.ToString();
                hasPercent = true;
                operation = null;
            }
        }

        private void btn1x_Click(object sender, RoutedEventArgs e)
        {
            if (equationDisplay.Text.Contains("^2") || equationDisplay.Text.Contains("1/") || equationDisplay.Text.Contains("sqrt"))
            {
                input = 1 / input;
                equationDisplay.Text = "1/(" + equationDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }
            else if (txtDisplay.Text != "0" && txtDisplay.Text != null)
            {
                input = 1 / Convert.ToDouble(txtDisplay.Text);
                equationDisplay.Text = "1/" + txtDisplay.Text;
                txtDisplay.Text = Convert.ToString(input);
            }
        }

        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
            if (equationDisplay.Text.Contains("^2") || equationDisplay.Text.Contains("1/") || equationDisplay.Text.Contains("sqrt"))
            {
                input = Math.Pow(input, 2.0);
                equationDisplay.Text = "(" + equationDisplay.Text + ")^2";
                txtDisplay.Text = Convert.ToString(input);
            }
            else if (txtDisplay.Text != "0" && txtDisplay.Text != null)
            {
                input = Math.Pow(Convert.ToDouble(txtDisplay.Text), 2.0);
                equationDisplay.Text = txtDisplay.Text + "^2";
                txtDisplay.Text = Convert.ToString(input);
            }
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (equationDisplay.Text.Contains("^2") || equationDisplay.Text.Contains("1/") || equationDisplay.Text.Contains("sqrt"))
            {
                input = Math.Sqrt(input);
                equationDisplay.Text = "sqrt(" + equationDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }
            else if (txtDisplay.Text != "0" && txtDisplay.Text != null)
            {
                input = Math.Sqrt(Convert.ToDouble(txtDisplay.Text));
                equationDisplay.Text = "sqrt(" + txtDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }
        }

        private void btnArithmetic_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (operation == null && txtDisplay.Text != null)
            {
                equationDisplay.Text = txtDisplay.Text;
                input = Convert.ToDouble(txtDisplay.Text);
                operation = button.Content.ToString();
                //equationDisplay.Text += operation;
            }
            else if (operation != null && hasResult)
            {
                equationDisplay.Text = input.ToString();
                hasResult = false;
                operation = button.Content.ToString();
                //equationDisplay.Text += operation;
            }
     
            txtDisplay.Text = "0";

        }

       
    }
}
