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
        List<string> operations = new List<string>();
        bool hasComma = false;
        bool digitsLocked = false;
        bool hasResult = false;
        bool isOperator = false;
        string tempValue = null;
        string operation = null;
        const double power = 2.0;
        double subTotal = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
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
            if(tempValue == null)
                equation.Text += txtDisplay.Text;
            
            operations.Add(txtDisplay.Text);

            if (hasResult == false && operations.Count > 2)
            {
                subTotal = Convert.ToDouble(operations.First());
                debugDisplay.Text = subTotal.ToString();
                operations.RemoveAt(0);

                foreach (var operand in operations)
                {
                    if (isNumeric(operand))
                    {
                        tempValue = operand;
                        debugDisplay.Text += operation;
                        debugDisplay.Text += tempValue;
                        Calculate();
                    }
                    else
                    {
                        operation = operand;
                    }
                }

                txtDisplay.Text = subTotal.ToString();
                hasResult = true;

            }
            else
            {
                equation.Text = txtDisplay.Text + operation + tempValue;
                Calculate();
                txtDisplay.Text = subTotal.ToString();
            }
            
            
        }

        private void btnPosNeg_Click(object sender, RoutedEventArgs e)
        {
            bool replace = false;

            if (txtDisplay.Text.EndsWith("."))
                replace = true;    

            txtDisplay.Text = (Convert.ToDouble(txtDisplay.Text) * -1.0).ToString();

            if (replace)
                txtDisplay.Text += ".";
            
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {
            int index = operations.Count;
            double tempDouble = 0;
            string tempOperation = null;
            string tempVariable = null;

            if (index > 1)
            {
                //Let's remove the last operation and operands from the list
                index--;
                tempOperation = operations[index];
                operations.RemoveAt(index);
                index--;
                tempVariable = operations[index];
                operations.RemoveAt(index);

                tempDouble = Convert.ToDouble(txtDisplay.Text) / 100 * Convert.ToDouble(tempVariable);
                equation.Text = equation.Text.Substring(0, (equation.Text.Length - tempVariable.Length - tempOperation.Length));
                tempValue = tempVariable + tempOperation + txtDisplay.Text + "%";
            }
            

            if (tempOperation == "+")
            {
                txtDisplay.Text = (Convert.ToDouble(txtDisplay.Text) + tempDouble).ToString();
            }
            else if (tempOperation == "-")
            {
                txtDisplay.Text = (Convert.ToDouble(txtDisplay.Text) - tempDouble).ToString();
            }
            else
            {
                txtDisplay.Text = (Convert.ToDouble(txtDisplay.Text) * 0.01).ToString();
            }

        }

        private void btn1x_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text != "0")
            {
                tempValue = "1/" + txtDisplay.Text;
                txtDisplay.Text = (1 / Convert.ToDouble(txtDisplay.Text)).ToString();
                digitsLocked = true;
            }
        }

        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text != "0")
            {
                tempValue = txtDisplay.Text + "^2";
                txtDisplay.Text = Math.Pow(Convert.ToDouble(txtDisplay.Text), power).ToString();
                digitsLocked = true;
            }
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text != "0")
            {
                tempValue = "sqrt(" + txtDisplay.Text + ")";
                txtDisplay.Text = (Math.Sqrt(Convert.ToDouble(txtDisplay.Text))).ToString();
                digitsLocked = true;
            }
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            isOperator = false;

            if (txtDisplay.Text == "0")
                txtDisplay.Text = null;

            if (digitsLocked != true)
            {
                Button button = sender as Button;
                txtDisplay.Text += button.Content.ToString();
            }
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            if (hasComma == false && digitsLocked == false)
            {
                Button button = sender as Button;
                txtDisplay.Text += button.Content.ToString();
                hasComma = true;
            }
        }

        private void btnArithmetic_Click(object sender, RoutedEventArgs e)
        {
            digitsLocked = false;

            if (isOperator == false)
            {
                operations.Add(txtDisplay.Text);
                if (tempValue != null)
                {
                    equation.Text += tempValue;
                    tempValue = null;
                }
                else if (hasResult)
                {
                    hasResult = false;
                    equation.Text = txtDisplay.Text;
                }
                else
                {
                    equation.Text += txtDisplay.Text;
                }

                Button button = sender as Button;
                operation = button.Content.ToString();

                if (equation.Text == "0")
                    equation.Text = null;

                equation.Text += operation;
                operations.Add(operation);

                operation = null;
                hasComma = false;
                digitsLocked = false;
                isOperator = true;
                txtDisplay.Text = "0";
            }
            
        }

        private void ClearAll()
        {
            equation.Text = null;
            debugDisplay.Text = null;
            txtDisplay.Text = "0";
            operations.Clear();
            hasComma = false;
            digitsLocked = false;
            tempValue = null;
            operation = null;
            isOperator = false;
        }

        public static bool isNumeric(string s)
        {
            return int.TryParse(s, out int n);
        }

        private void Calculate()
        {
            switch(operation)
            {
                case "+":
                    subTotal += Convert.ToDouble(tempValue);
                    break;
                case "-":
                    subTotal -= Convert.ToDouble(tempValue);
                    break;
                case "*":
                    subTotal *= Convert.ToDouble(tempValue);
                    break;
                case "/":
                    // What about divide by zero?
                    subTotal /= Convert.ToDouble(tempValue);
                    break;
            }

            //operation = tempValue = null;
        }
    }
}
