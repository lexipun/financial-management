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
        private readonly TextBox newCause = new TextBox();
        public Incomes()
        {
            InitializeComponent();
            AddCause = "Add Cause";
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
            else if(table.Children.Contains(newCause))
            {
                Grid.SetRow(comboBox, Grid.GetRow(comboBox) + 1);
                table.Children.Remove(newCause);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
