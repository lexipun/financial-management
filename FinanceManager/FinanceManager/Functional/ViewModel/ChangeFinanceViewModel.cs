using FinanceManager.functional.Additional;
using FinanceManager.functional.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FinanceManager.functional.ViewModel
{
    class ChangeFinanceViewModel
    {
        public TreeList<string> Causes { get; set; }
        public string SelectedCause { get; set; }
        public string Amount { get; set; }
        private readonly string addCause;
        private TextBox newCause;
        public ChangeFinanceViewModel()
        {
            Causes = new TreeList<string>();
            newCause = new TextBox();

            addCause = "Add Cause";

            foreach (var cause in MainWindow.myFinance.ExpenseCauses)
            {
                Causes.Add(cause);
            }

            Causes.Add(addCause);
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            decimal result;

            if (SelectedCause.Equals(addCause))
            {
                if (!MainWindow.myFinance.AddExpenseCause(newCause.Text))
                {
                    MessageBox.Show("You cannot add cause");
                }
            }

            if (Decimal.TryParse(Amount, out result))
            {
                Act act = new Act() { amount = result, cause = SelectedCause };

                if (result < 0 || !MainWindow.myFinance.ChangeBudget(act))
                {
                    MessageBox.Show("Oops. You cannot add summ");
                }
            }
        }

    }
}
