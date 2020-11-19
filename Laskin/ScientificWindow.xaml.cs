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
        string trigOperation = null;
        bool hasResult = false;
        bool hasCache = false;

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
            if(txtDisplay.Text.Length > 1)
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
                case "mod":
                    input %= cache;
                    break;
                case "^":
                    input = Math.Pow(input, cache);
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
            if (operation != null)
            {
                equationDisplay.Text = equationDisplay.Text + "ln(" + txtDisplay.Text + ")";
                cache = Math.Log(Convert.ToDouble(txtDisplay.Text));
                hasCache = true;
                txtDisplay.Text = Convert.ToString(cache);
                Calculate();
                operation = null;
            }
            else
            {
                input = Math.Log(Convert.ToDouble(txtDisplay.Text));
                equationDisplay.Text = "ln(" + txtDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            if (operation != null)
            {
                equationDisplay.Text = equationDisplay.Text + "log(" + txtDisplay.Text + ")";
                cache = Math.Log(Convert.ToDouble(txtDisplay.Text), 10);
                hasCache = true;
                txtDisplay.Text = Convert.ToString(cache);
                Calculate();
                operation = null;
            }
            else
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
                equationDisplay.Text = equationDisplay.Text + txtDisplay.Text + "!";
                txtDisplay.Text = Convert.ToString(factorial(Convert.ToInt32(txtDisplay.Text)));
            }
        }

        private void btnTrigonometry_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            trigOperation = button.Content.ToString();

            switch (trigOperation)
            {
                case "SIN":
                    cache = Math.Sin(Convert.ToDouble(txtDisplay.Text));
                    break;
                case "TAN":
                    cache = Math.Tan(Convert.ToDouble(txtDisplay.Text));
                    break;
                case "COS":
                    cache = Math.Cos(Convert.ToDouble(txtDisplay.Text));
                    break;
                case "ArcSIN":
                    cache = Math.Asin(Convert.ToDouble(txtDisplay.Text));
                    break;
                case "ArcTAN":
                    cache = Math.Atan(Convert.ToDouble(txtDisplay.Text));
                    break;
                case "ArcCOS":
                    cache = Math.Acos(Convert.ToDouble(txtDisplay.Text));
                    break;
            }

            if (operation != null)
            {
                equationDisplay.Text = equationDisplay.Text + trigOperation + "(" + txtDisplay.Text + ")";
                hasCache = true;
                txtDisplay.Text = Convert.ToString(cache);
                Calculate();
                operation = null;
            }
            else
            {
                input = cache;
                equationDisplay.Text = trigOperation + "(" + txtDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
            }

            /*if (operation != null)
            {
                

                equationDisplay.Text = trigOperation + "(" +  txtDisplay.Text + ")";
                txtDisplay.Text = Convert.ToString(input);
                trigOperation = null;
            }*/
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
