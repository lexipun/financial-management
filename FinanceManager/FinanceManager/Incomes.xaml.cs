using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinanceManager
{
    /// <summary>
    /// Логика взаимодействия для Incomes.xaml
    /// </summary>
    public partial class Incomes : Window
    {
        public Incomes()
        {
            InitializeComponent();
            TextBlock txt;
            int count = 0;

            foreach(var cause in MainWindow.myFinance.IncomeCauses)
            {
                txt = new TextBlock();
                txt.Text = cause;
                fromIncomes.Resources.Add(++count,txt);
            }

            txt = new TextBlock();
            txt.Text = "Add Cause";
            fromIncomes.Resources.Add(count,txt);

        }

        private void fromIncomes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
