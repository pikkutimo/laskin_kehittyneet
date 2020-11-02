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
        double numberOne = 0;
        double numberTwo = 0;
        double result = 0;
        string operation = null;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10);
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10);
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10) + 1;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10) + 1;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10) + 2;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10) + 2;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10) + 3;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10) + 3;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10) + 4;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10) + 4;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10) + 5;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10) + 5;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10) + 6;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10) + 6;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10) + 7;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10) + 7;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10) + 8;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10) + 8;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = (numberOne * 10) + 9;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = (numberTwo * 10) + 9;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            operation = "+";
            txtDisplay.Text = "0";
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            operation = "-";
            txtDisplay.Text = "0";
        }

        private void btnTimes_Click(object sender, RoutedEventArgs e)
        {
            operation = "*";
            txtDisplay.Text = "0";
        }

        private void btnDivide_Click(object sender, RoutedEventArgs e)
        {
            operation = "/";
            txtDisplay.Text = "0";
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            switch (operation)
            {
                case "+":
                    result = numberOne + numberTwo;
                    break;
                case "-":
                    result = numberOne - numberTwo;
                    break;
                case "*":
                    result = numberOne * numberTwo;
                    break;
                case "/":
                    result = numberOne / numberTwo;
                    break;
            }

            txtDisplay.Text = (result).ToString();
            operation = "";
            numberOne = result;
            numberTwo = 0;
            result = 0;
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPosNeg_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = -1.0 * numberOne;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = -1.0 * numberTwo;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn1x_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne *= numberOne;
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo *= numberTwo;
                txtDisplay.Text = numberTwo.ToString();
            }
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(operation))
            {
                numberOne = Math.Sqrt(numberOne);
                txtDisplay.Text = numberOne.ToString();
            }
            else
            {
                numberTwo = Math.Sqrt(numberTwo);
                txtDisplay.Text = numberTwo.ToString();
            }
        }
    }
}
