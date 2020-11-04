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
        List<string> operations = new List<string>();
        bool hasComma = false;
        bool digitsLocked = false;
        string tempValue = null;
        string operation = null;
        double power = 2.0;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
           
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
            tempValue = txtDisplay.Text + "^2";
            txtDisplay.Text =  Math.Pow(Convert.ToDouble(txtDisplay.Text), power).ToString();
            digitsLocked = true;
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            tempValue = "sqrt(" + txtDisplay.Text + ")";
            txtDisplay.Text = (Math.Sqrt(Convert.ToDouble(txtDisplay.Text))).ToString();
            digitsLocked = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
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

            if (tempValue != null)
            {
                operations.Add(tempValue);
                equation.Text += tempValue;
                tempValue = null;
            }
            else
            {
                operations.Add(txtDisplay.Text);
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
            txtDisplay.Text = "0";
        }

        private void calculate()
        {
           
        }

        private void ClearAll()
        {
           
        }
    }
}
