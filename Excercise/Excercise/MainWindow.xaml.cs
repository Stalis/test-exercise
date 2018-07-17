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

        private void StartSorting()
        {
            ClearFields();
            ProgressBarStart();
            try
            {
                Task<int[]>.Factory.StartNew(
                    arg => PairSorter.GetInstance().Sort((int[])arg),
                    ReadInput())
                    .ContinueWith(
                        task =>
                        {
                            logBox.Dispatcher.BeginInvoke(
                                new Action(() => logBox.Text = string.Join("\n", PairSorter.GetInstance().Log))
                                    );
                            logBox.Dispatcher.BeginInvoke(
                                new Action(
                                    () => resultBox.Text =
                                (from item in task.Result select item.ToString()).Aggregate((cur, next) => cur + next)
                            ));
                            progressBar.Dispatcher.BeginInvoke(new Action(ProgressBarStop));
                        });
            }
            catch (InvalidArrayException)
            {
                ProgressBarStop();
                MessageBox.Show("Неверный входной массив. Массив должен состоять из 6 уникальных чисел от 1 до 6");
            }
            catch (Exception ex)
            {
                ProgressBarStop();
                MessageBox.Show($"Неизвестная ошибка:\n{ex.Message}");
            }
        }

        #region Helpers
        private int[] ReadInput()
        {
            var input = (from item in inputBox.Text.ToCharArray()
                         where char.IsDigit(item)
                         select (int)char.GetNumericValue(item))
                         .Where(num => num > 0 && num <= 6)
                         .ToArray();

            if (input.Count() != 6 ) throw new InvalidArrayException();
            return input;
        }
        
        private void ProgressBarStart()
        {
            progressBar.Visibility = Visibility.Visible;
            progressBar.IsIndeterminate = true;
        }

        private void ProgressBarStop()
        {
            progressBar.Visibility = Visibility.Hidden;
            progressBar.IsIndeterminate = false;
        }

        private void ClearFields()
        {
            resultBox.Clear();
            logBox.Clear();
        }
        #endregion

        #region Handlers
        private void sortButton_Click(object sender, RoutedEventArgs e) =>
            StartSorting();

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                StartSorting();
            }
        }
        #endregion
    }

    class InvalidArrayException : Exception { }
}
