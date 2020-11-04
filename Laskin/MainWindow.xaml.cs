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

namespace Laskin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double number = 0;
        double subTotal = 0;
        string operation = null;
        string tempNumber = null;
        bool hasComma = false;
        bool hasOperator = false;
        bool hasResult = false;
        bool isInvalid = false;
        double numberCopy = 0;
        string operationCopy = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            tempNumber = "0";
            number = 0;
            txtDisplay.Text = tempNumber;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text.Length < 2)
            {
                txtDisplay.Text = "0";
                number = 0;
            }
            else
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
                number = Convert.ToDouble(txtDisplay.Text);
            }
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            numberCopy = number;
            operationCopy = operation;

            if (hasOperator)
                calculate();
            else
            {
                number = numberCopy;
                operation = operationCopy;
                calculate();
            }

            txtDisplay.Text = subTotal.ToString();
            hasResult = true;
        }

        private void btnPosNeg_Click(object sender, RoutedEventArgs e)
        {
            number *= -1.0;
            txtDisplay.Text = number.ToString();
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btn1x_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text != "0")
            {
                number = (1 / Convert.ToDouble(txtDisplay.Text));
                txtDisplay.Text = tempNumber = number.ToString();
            }
            else
            {
                txtDisplay.Text = "Invalid operation";
                isInvalid = true;
            }
        }

        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            if (hasResult || isInvalid)
            {
                ClearAll();
            }
            Button button = sender as Button;
            tempNumber += button.Content.ToString();
            txtDisplay.Text = tempNumber;

            number = Convert.ToDouble(txtDisplay.Text);

            if (hasOperator == false)
            {
                subTotal = number;
                number = 0;
            }
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            if (hasComma == false)
            {
                tempNumber += ".";
                txtDisplay.Text = tempNumber;
                hasComma = true;
            }
        }

        private void btnArithmetic_Click(object sender, RoutedEventArgs e)
        {
            if (equation.Text == "0")
            {
                equation.Text = tempNumber;
            }
            
            if (hasResult)
            {
                equation.Text = subTotal.ToString();
                hasResult = false;
            }

            tempNumber = null;
            if (isInvalid == false)
            {
                Button button = sender as Button;
                operation = button.Content.ToString();
                equation.Text += operation;
                hasOperator = true;
                tempNumber = null;
            }
        }

        private void calculate()
        {
            switch(operation)
            {
                case "+":
                    subTotal += number;
                    break;
                case "-":
                    subTotal -= number;
                    break;
                case "*":
                    subTotal *= number;
                    break;
                case "/":
                    if (number == 0)
                    {
                        txtDisplay.Text = "Invalid operation";
                        isInvalid = true;
                    }
                    else
                        subTotal /= number;
                    break;
            }
            equation.Text += txtDisplay.Text;
            txtDisplay.Text = null;
            number = 0;
            operation = null;
            tempNumber = null;
        }

        private void ClearAll()
        {
            equation.Text = "0";
            txtDisplay.Text = "0";
            number = 0;
            subTotal = 0;
            operation = null;
            tempNumber = null;
            hasComma = false;
            hasOperator = false;
            hasResult = false;
            isInvalid = false;
        }
    }
}
