using FinanceManager.functional.Localization;
using FinanceManager.functional.Model;
using FinanceManager.functional.ViewModel;
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
        private readonly TextBox newCause = new TextBox();
        public Incomes()
        {
            InitializeComponent();
            DataContext = new ChangeFinanceViewModel(true);
        }

        private void fromIncomes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string cause = comboBox.SelectedItem as string;

            if (cause.Equals(Translate.AddCause))
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }
    }
}
