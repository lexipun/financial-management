using FinanceManager.functional;
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

namespace FinanceManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Finance myFinance;
        WriterAndReader saveData;
        public MainWindow()
        {
            saveData = new WriterAndReader();

            InitializeComponent();

            myFinance = new functional.Finance(SignalChangeSumm);

            saveData.UserDataLoader();
            myFinance.SetDataEventAboutChangeData(SignalChangeSumm);

            SignalChangeSumm(myFinance.Budget);
        }

        private void SignalChangeSumm(decimal count)
        {
            summ.Text = count.ToString();
        }

        private void income_Click(object sender, RoutedEventArgs e)
        {
            Incomes incomes = new Incomes();
            incomes.Show();
        }

        private void Expense_Click(object sender, RoutedEventArgs e)
        {
            Expenses expenses = new Expenses();
            expenses.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveData.UserDataSaver();
        }
    }
}
