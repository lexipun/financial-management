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
    /// Логика взаимодействия для Expenses.xaml
    /// </summary>
    public partial class Expenses : Window
    {
        private readonly string AddCause;
        TextBox newCause;
        public Expenses()
        {
            InitializeComponent();
            AddCause = "Add Cause";
            newCause = new TextBox();
            TextBlock txt;

            foreach (var cause in MainWindow.myFinance.ExpenseCauses)
            {
                txt = new TextBlock();
                txt.Text = cause;
                fromIncomes.Items.Add(txt);
            }

            txt = new TextBlock();
            txt.Text = AddCause;
            fromIncomes.Items.Add(txt);
        }

        private void fromIncomes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock txt = (TextBlock)comboBox.SelectedItem;
            if (txt.Text.Equals(AddCause))
            {


                Grid.SetRow(comboBox, Grid.GetRow(comboBox) - 1);


                Grid.SetColumn(newCause, Grid.GetColumn(comboBox));
                Grid.SetRow(newCause, Grid.GetRow(comboBox) + 1);
                table.Children.Add(newCause);

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            decimal rezult = 0;
            TextBlock txt = (TextBlock)fromIncomes.SelectedItem;
            if (txt.Text.Equals(AddCause))
            {
                if (!MainWindow.myFinance.AddExpenseCause(newCause.Text))
                {
                    MessageBox.Show("You cannot add cause");
                }
            }
            if (Decimal.TryParse(Summ.Text, out rezult))
            {
                if (rezult < 0 || !MainWindow.myFinance.ChangeBudget(rezult))
                {
                    MessageBox.Show("Oops. You cannot add summ");
                }
            }

            Close();
        }

    }
}
