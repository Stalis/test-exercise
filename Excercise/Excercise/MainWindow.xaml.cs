using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Excercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] m_InputSeparators = new char[] { ',' };

        public MainWindow()
        {
            InitializeComponent();

        }

        private void sortButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.Print("Start sorting");
            try
            {
                Task<int[]>.Factory.StartNew((arg) =>
                    PairSorter.GetInstance().Sort((int[])arg),
                    ReadInput()
                    );
            } catch(InvalidArraySizeException)
            {
                MessageBox.Show("Неверный размер массива. Массив должен состоять из 6 чисел");
            }
            logBox.Text = string.Join("\n", PairSorter.GetInstance().Log);
            Debug.Print("Stop sorting");
        }

        private int[] ReadInput()
        {
            var input = (from item in inputBox.Text.Split(m_InputSeparators)
             where int.TryParse(item, out _)
             select int.Parse(item)).ToArray();

            if (input.Count() != 6) throw new InvalidArraySizeException();
            return input;
        }
    }

    class InvalidArraySizeException : Exception
    {

    }
}
