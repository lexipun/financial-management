using FinanceManager.functional.Localization;
using FinanceManager.functional.Model;
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
        TextBox newCause = new TextBox();
        public Expenses()
        {
            InitializeComponent();
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
            Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }
    }
}
