using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await CSharpScript.EvaluateAsync("1+1", ScriptOptions.Default.WithImports("System.Math"));
        }
        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(expressionTextBox.Text))  {
                int index = expressionTextBox.Text.LastIndexOfAny(new char[] { '+', '-', '/', '*' });
                expressionTextBox.Text = expressionTextBox.Text.Substring(0, index + 1);
            }
        }
        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            expressionTextBox.Clear();
            resultTextBox.Clear();
        }
        private void RemoveLastSymbButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(expressionTextBox.Text))
            {
                expressionTextBox.Text = expressionTextBox.Text.Substring(0, expressionTextBox.Text.Length - 1);
            }
        }

        private void NumpadButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            expressionTextBox.Text += button.Content;
        }

        private async void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(expressionTextBox.Text))
            {
                var result = await CSharpScript.EvaluateAsync(expressionTextBox.Text, ScriptOptions.Default.WithImports("System.Math"));
                resultTextBox.Text = result.ToString();
            }
        }

    }
}