using FinanceManager.functional.Model;
using System;
using System.Windows;
using System.Windows.Controls;


namespace FinanceManager
{
    /// <summary>
    /// Логика взаимодействия для Incomes.xaml
    /// </summary>
    public partial class Incomes : Window
    {
        private readonly string AddCause;
        TextBox newCause;
        public Incomes()
        {
            InitializeComponent();
            AddCause = "Add Cause";
            newCause = new TextBox();
            TextBlock txt;

            foreach (var cause in MainWindow.myFinance.IncomeCauses)
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
            decimal result = 0;
            TextBlock txt = (TextBlock)fromIncomes.SelectedItem;

            if (txt.Text.Equals(AddCause))
            {
                if (!MainWindow.myFinance.AddIncomeCause(newCause.Text))
                {
                    MessageBox.Show("You cannot add cause");
                }
            }

            if (Decimal.TryParse(Summ.Text, out result))
            {
                Act act = new Act() { amount = result, cause = txt.Text };

                if (result < 0 || !MainWindow.myFinance.ChangeBudget(act))
                {
                    MessageBox.Show("Oops. You cannot add summ");
                }
            }

            Close();
        }
    }
}
