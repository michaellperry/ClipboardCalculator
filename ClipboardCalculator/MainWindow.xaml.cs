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

namespace ClipboardCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICommand _reduceClipboardCommand;

        public MainWindow()
        {
            _reduceClipboardCommand = new RelayCommand(DoReduceClipboard);
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        public ICommand ReduceClipboard
        {
            get { return _reduceClipboardCommand; }
        }

        private void DoReduceClipboard()
        {
            if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
            {
                string text = Clipboard.GetText(TextDataFormat.UnicodeText);
                try
                {
                    string result = Evaluator.Evaluate(text);
                    Clipboard.SetText(result, TextDataFormat.UnicodeText);
                }
                catch (Exception x)
                {
                    Clipboard.SetText(x.Message, TextDataFormat.Text);
                }
            }
        }
    }
}
