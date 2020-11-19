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
    /// Interaction logic for ScientificWindow.xaml
    /// </summary>
    public partial class ScientificWindow : Window
    {
        double input = 0;
        double cache = 0;
        string operation = null;
        string temp = null;
        string trigOperation = null;
        bool hasResult = false;
        bool hasPercent = false;
        bool hasPerX = false;

        public ScientificWindow()
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
            if (txtDisplay.Text.Length > 0)
            {
                //3.
                // You can't crash the program by erasing txtDisplay.Text
                Calculate();
                hasResult = true;
                hasPercent = false;
                hasPerX = false;
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

        private void Calculate()
        {
            if (equationDisplay.Text.Contains("%"))
                //2.
                //If %-sign present do not add the to anywhere 
                equationDisplay.Text += operation;
            else if (equationDisplay.Text.Contains("^2"))
            {
                cache = Math.Pow(Convert.ToDouble(txtDisplay.Text), 2.0);
            }
            else if (equationDisplay.Text.Contains("sqrt"))
            {
                cache = Math.Sqrt(Convert.ToDouble(txtDisplay.Text));
            }
            else if (hasPerX)
            {
                equationDisplay.Text = temp;
                cache = 1 / Convert.ToDouble(txtDisplay.Text);
                temp = "1/" + txtDisplay.Text;
                equationDisplay.Text = equationDisplay.Text + operation + temp;
                hasPerX = false;
            }
            else if (!hasResult)
            {
                equationDisplay.Text += operation;
                cache = Convert.ToDouble(txtDisplay.Text);
                equationDisplay.Text += txtDisplay.Text;
            }
            else
                equationDisplay.Text = equationDisplay.Text + operation + cache.ToString();

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
                case "mod":
                    input %= cache;
                    break;
                case "^":
                    input = Math.Pow(input, cache);
                    break;
            }

            txtDisplay.Text = input.ToString();
        }

        private void btn1x_Click(object sender, RoutedEventArgs e)
        {
            if (operation != null)
            {
                //cache = Math.Pow(Convert.ToDouble(txtDisplay.Text), 2.0);
                equationDisplay.Text = temp + operation + "1/" + txtDisplay.Text;
                Calculate();
                operation = null;
                hasPerX = false;
            }
            /*else if (equationDisplay.Text.Contains("^2") || equationDisplay.Text.Contains("1/") || equationDisplay.Text.Contains("sqrt"))
            {
                input = 1 / input;
                equationDisplay.Text = "1/(" + equationDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }*/
            else if (txtDisplay.Text != "0" && txtDisplay.Text != null)
            {
                input = 1 / Convert.ToDouble(txtDisplay.Text);
                equationDisplay.Text = "1/" + txtDisplay.Text;
                txtDisplay.Text = Convert.ToString(input);
                hasPerX = true;
            }
        }

        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
            if (operation != null)
            {
                temp = equationDisplay.Text;
                //cache = Math.Pow(Convert.ToDouble(txtDisplay.Text), 2.0);
                equationDisplay.Text = temp + operation + "(" + txtDisplay.Text + ")^2";
                Calculate();
                operation = null;
            }
            /*else if (equationDisplay.Text.Contains("^2") || equationDisplay.Text.Contains("1/") || equationDisplay.Text.Contains("sqrt"))
            {
                input = Math.Pow(input, 2.0);
                equationDisplay.Text = "(" + equationDisplay.Text + ")^2";
                txtDisplay.Text = Convert.ToString(input);     
            }*/
            else if (txtDisplay.Text != "0" && txtDisplay.Text != null)
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
                temp = equationDisplay.Text;
                equationDisplay.Text = temp + operation + "sqrt(" + txtDisplay.Text + ")";
                Calculate();
                operation = null;
            }
            else if (equationDisplay.Text.Contains("^2") || equationDisplay.Text.Contains("1/") || equationDisplay.Text.Contains("sqrt"))
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

            if (equationDisplay.Text.Contains("1/") || equationDisplay.Text.Contains("sqrt"))
            {
                temp = equationDisplay.Text;
            }
            else if (operation == null && txtDisplay.Text != null)
            {
                equationDisplay.Text = txtDisplay.Text;
                input = Convert.ToDouble(txtDisplay.Text);
                //operation = button.Content.ToString();
                //equationDisplay.Text += operation;
            }
            else if (operation != null && hasResult)
            {
                equationDisplay.Text = input.ToString();
                hasResult = false;
                //operation = button.Content.ToString();
                //equationDisplay.Text += operation;
            }
            else
            {
                //If there is already input & operation and the user presses arithmetic operator again
                // => calculate the existing operation as input
                Calculate();
                //operation = button.Content.ToString();
            }

            operation = button.Content.ToString();
            txtDisplay.Text = "0";

        }

        private void MenuScientificClick(object sender, RoutedEventArgs e)
        {

        }

        private void MenuStandardClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
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

        private void btnLn_Click(object sender, RoutedEventArgs e)
        {
            if (equationDisplay.Text.Contains("^2") || equationDisplay.Text.Contains("1/") || equationDisplay.Text.Contains("sqrt") || equationDisplay.Text.Contains("log"))
            {
                input = Math.Log(input);
                equationDisplay.Text = "log(" + equationDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }
            else if (txtDisplay.Text != "0" && txtDisplay.Text != null)
            {
                input = Math.Log(Convert.ToDouble(txtDisplay.Text));
                equationDisplay.Text = "log(" + txtDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            if (equationDisplay.Text.Contains("^2") || equationDisplay.Text.Contains("1/") || equationDisplay.Text.Contains("sqrt") || equationDisplay.Text.Contains("log"))
            {
                input = Math.Log(input, 10);
                equationDisplay.Text = "log(" + equationDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }
            else if (txtDisplay.Text != "0" && txtDisplay.Text != null)
            {
                input = Math.Log(Convert.ToDouble(txtDisplay.Text), 10);
                equationDisplay.Text = "log(" + txtDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }
        }

       

        private void btnPi_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text == null || txtDisplay.Text == "0")
                txtDisplay.Text = Math.PI.ToString();
        }

        private void btnFactorial_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text.Contains(".") || txtDisplay.Text.Contains("-") || txtDisplay.Text == "0")
            {

            }
            else 
            {
                txtDisplay.Text = Convert.ToString(factorial(Convert.ToInt32(txtDisplay.Text)));
            }
        }

        private void btnTrigonometry_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            trigOperation = button.Content.ToString();

            if (equationDisplay.Text == "")
            {
                input = Convert.ToDouble(txtDisplay.Text);

                switch (trigOperation)
                {
                    case "SIN":
                        input = Math.Sin(input);
                        break;
                    case "TAN":
                        input = Math.Tan(input);
                        break;
                    case "COS":
                        input = Math.Cos(input);
                        break;
                    case "ArcSIN":
                        input = Math.Asin(input);
                        break;
                    case "ArcTAN":
                        input = Math.Atan(input);
                        break;
                    case "ArcCOS":
                        input = Math.Acos(input);
                        break;
                }

                equationDisplay.Text = trigOperation + "(" +  txtDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
                trigOperation = null;
            }
        }

        long factorial(int number)
        {
            if (number == 1)
                return 1;
            else
                return number * factorial(number - 1);
        }
    }
}
