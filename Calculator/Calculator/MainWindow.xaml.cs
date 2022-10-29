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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        decimal lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            buttonAC.Click += ButtonAC_Click;
            buttonNegative.Click += ButtonNegative_Click;
            buttonPercentage.Click += ButtonPercentage_Click;
            buttonEquals.Click += ButtonEquals_Click;
        }

        private void ButtonEquals_Click(object sender, RoutedEventArgs e)
        {
            decimal newNumber;

            if (decimal.TryParse(labelResult.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                       result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multliply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Divsion:
                        result = SimpleMath.Divide(lastNumber,newNumber);
                        break;
                }
                labelResult.Content = result.ToString();
            }
        }

        private void ButtonPercentage_Click(object sender, RoutedEventArgs e)
        {
            decimal tempNumber;

            if (decimal.TryParse(labelResult.Content.ToString(), out tempNumber))
            {
                tempNumber = (tempNumber / 100);
                if (lastNumber != 0)
                    tempNumber *= lastNumber;
                labelResult.Content = tempNumber.ToString();
            }
        }

        private void ButtonNegative_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(labelResult.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                labelResult.Content = lastNumber.ToString();
            }
        }

        private void ButtonAC_Click(object sender, RoutedEventArgs e)
        {
            labelResult.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(labelResult.Content.ToString(), out lastNumber))
            {
                labelResult.Content = "0";
            }

            if (sender == buttonMultiplication)
                selectedOperator = SelectedOperator.Multiplication;
            if (sender == buttonDivision)
                selectedOperator = SelectedOperator.Divsion;
            if(sender == buttonAddition)
                selectedOperator = SelectedOperator.Addition;
            if (sender == buttonSubtracttion)
                selectedOperator = SelectedOperator.Subtraction;
        }

        private void buttonSeparator_Click(object sender, RoutedEventArgs e)
        {   
            if (labelResult.Content.ToString().Contains(","))
            {
                // Do nothing
            }
            else
            {
                labelResult.Content = $"{labelResult.Content},";
            }
        }

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = $"{selectedValue}";
            }
            else
            {
                labelResult.Content = $"{labelResult.Content}{selectedValue}";
            }

        }
    }
    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Divsion
    }

    public class SimpleMath
    {
        public static decimal Add(decimal n1, decimal n2)
        {
            return n1 + n2;
        }
        public static decimal Subtract(decimal n1, decimal n2)
        {
            return n1 - n2;
        }
        public static decimal Multliply(decimal n1, decimal n2)
        {
            return n1 * n2;
        }
        public static decimal Divide(decimal n1, decimal n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return n1 / n2;
        }
    }
}
