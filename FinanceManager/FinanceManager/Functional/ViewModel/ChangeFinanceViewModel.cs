using FinanceManager.functional.Additional;
using FinanceManager.functional.Localization;
using FinanceManager.functional.Model;
using FinanceManager.Functional.GlobalPatterns.Observable;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FinanceManager.functional.ViewModel
{
    class ChangeFinanceViewModel : Translate, IObserver<Dependencies>
    {
        private TextBox newCause;
        private IDisposable undescriber;
        public bool IsIncome {get;set;}
        public TreeList<string> Causes { get; set; }
        public string SelectedCause { get; set; }
        public string Amount { get; set; }
        public RelayCommand Save_Click_Command { get; set; }
        public ChangeFinanceViewModel()
        {
            Save_Click_Command = new RelayCommand(Save_Click);
            Causes = new TreeList<string>();
            newCause = new TextBox();

            foreach (string cause in MainWindow.myFinance.ExpenseCauses)
            {
                Causes.Add(cause);
            }

            Causes.Add(AddCause);
            undescriber = UpdateDataObservable.Observe.Subscribe(this);
        }


        private void Save_Click()
        {

            if (SelectedCause.Equals(AddCause))
            {
                if (IsIncome)
                {
                    if (!MainWindow.myFinance.AddIncomeCause(newCause.Text))
                    {
                        MessageBox.Show(string.Format(ExistCause, newCause.Text));
                    }
                }
                else
                {
                    if (!MainWindow.myFinance.AddExpenseCause(newCause.Text))
                    {
                        MessageBox.Show(string.Format(ExistCause, newCause.Text));
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
                    MessageBox.Show(IncomeTooSmall);
                }

                Act act = new Act() { amount = spentMoney, cause = SelectedCause };

                if (spentMoney < 0 || !MainWindow.myFinance.ChangeBudget(act))
                {
                    MessageBox.Show(string.Format(ExpenseTooBig, -(MainWindow.myFinance.Budget + spentMoney)));
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
            if (value.Contains(Type))
            {
                Causes.RemoveAt(Causes.Count - 1);
                Causes.Add(AddCause);
            }
        }

        ~ChangeFinanceViewModel()
        {
            undescriber.Dispose();
        }
    }
}
