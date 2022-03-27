using FinanceManager.functional.Additional;
using FinanceManager.functional.Localization;
using FinanceManager.functional.Model;
using FinanceManager.Functional.GlobalPatterns.Observable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FinanceManager.functional.ViewModel
{
    class ChangeFinanceViewModel : IObserver<Dependencies>
    {
        public bool IsIncome {get;set;}
        public TreeList<string> Causes { get; set; }
        public string SelectedCause { get; set; }
        public string Amount { get; set; }
        private TextBox newCause;
        private IDisposable undescriber;
        public ChangeFinanceViewModel()
        {
            Causes = new TreeList<string>();
            newCause = new TextBox();

            foreach (string cause in MainWindow.myFinance.ExpenseCauses)
            {
                Causes.Add(cause);
            }

            Causes.Add(Translate.AddCause);
            undescriber = UpdateDataObservable.Observe.Subscribe(this);
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedCause.Equals(Translate.AddCause))
            {
                if (IsIncome)
                {
                    if (!MainWindow.myFinance.AddIncomeCause(newCause.Text))
                    {
                        MessageBox.Show(string.Format("this cause \"{0}\" alredy exist", newCause.Text));
                    }
                }
                else
                {
                    if (!MainWindow.myFinance.AddExpenseCause(newCause.Text))
                    {
                        MessageBox.Show(string.Format("this cause \"{0}\" alredy exist", newCause.Text));
                    }
                }
            }

            if (decimal.TryParse(Amount, out decimal spentMoney))
            {
                if (!IsIncome && spentMoney > 0)
                {
                    spentMoney = -spentMoney;
                }else if(IsIncome && spentMoney < 0)
                {
                    MessageBox.Show("You typed income letter then 0");
                }

                Act act = new Act() { amount = spentMoney, cause = SelectedCause };

                if (spentMoney < 0 || !MainWindow.myFinance.ChangeBudget(act))
                {
                    MessageBox.Show(string.Format("your budget is too small for this act \n you need {0}", -(MainWindow.myFinance.Budget + spentMoney)));
                }
            }
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(Dependencies value)
        {
            if (value.Contains(Translate.Type))
            {
                Causes.RemoveAt(Causes.Count - 1);
                Causes.Add(Translate.AddCause);
            }
        }

        ~ChangeFinanceViewModel()
        {
            undescriber.Dispose();
        }
    }
}
