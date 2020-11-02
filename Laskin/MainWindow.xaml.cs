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
        decimal number = 0;
        decimal subtotal = 0;
        string operation = null;
        string equation = null;
        string input = null;
        bool isComma = false;
        bool isAnswer = false;
        bool negativeNumber = false;
        public MainWindow()
        {
            InitializeComponent();
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

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (input != null)
            {
                calculate();
            }

            input = null;
            operation = null;
            isComma = false;

            txtDisplay.Text = subtotal.ToString();
            isAnswer = true;
            subtotal = 0;
        }

        private void btnPosNeg_Click(object sender, RoutedEventArgs e)
        {
            decimal tempValue = Convert.ToDecimal(input);
            tempValue *= (decimal)-1;
            input = tempValue.ToString();
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn1x_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text == "0")
                txtDisplay.Text = equation = null;

            if (isAnswer == true)
            {
                txtDisplay.Text = null;
                isAnswer = false;
                equation = subtotal.ToString();
            }

            Button button = sender as Button;
            input += button.Content.ToString();

            number = Convert.ToDecimal(input);

            txtDisplay.Text = equation + input; 
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            if (isComma == false)
            {
                isComma = true;

                Button button = sender as Button;
                input += button.Content.ToString();
                txtDisplay.Text += button.Content.ToString();
            }

        }

        private void btnArithmetic_Click(object sender, RoutedEventArgs e)
        {
            if (operation != null)
            {
                calculate();

            }

            if (isAnswer == true)
                subtotal = Convert.ToDecimal(txtDisplay.Text);

            if (subtotal == 0)
                subtotal = Convert.ToDecimal(input);
            else
                number = Convert.ToDecimal(input);
            
            input = null;
            isComma = false;


            Button button = sender as Button;
            operation = button.Content.ToString();
            txtDisplay.Text = txtDisplay.Text + " " + operation + " ";
        }

        private void calculate()
        {
            switch (operation)
            {
                case "+":
                    subtotal += number;
                    break;
                case "-":
                    subtotal *= number;
                    break;
                case "/":
                    subtotal /= number;
                    break;
                case "*":
                    subtotal *= number;
                    break;
            }

            operation = null;
        }
    }
}
