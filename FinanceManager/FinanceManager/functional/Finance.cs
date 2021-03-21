using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.functional
{
    [Serializable]
    public class Finance
    {
        private Action<decimal> _signalChangeSumm;
        private decimal _budget;
        private List<string> _incomeCauses;
        private List<string> _expenseCauses;

        public decimal Budget { get { return _budget; } }
        public List<string> IncomeCauses { get { return _incomeCauses; } }
        public List<string> ExpenseCauses { get { return _expenseCauses; } }

        public bool ChangeBudget(decimal summ)
        {
            if (summ < 0 && _budget + summ < 0)
            {
                return false;
            }
            _budget += summ;
            _signalChangeSumm(_budget);
            return true;
        }
        public bool ChangeBudget(decimal summ, string cause)
        {
            if ((summ < 0 && _budget + summ <0) )
            {
                return false;
            }
            _budget += summ;
            _signalChangeSumm(_budget);
            return true;
        }

        public bool AddIncomeCause(string cause)
        {
            if (_incomeCauses.Contains(cause))
            {
                return false;
            }
            _incomeCauses.Add(cause);
            return true;
        }
        public bool AddExpenseCause(string cause)
        {
            if (_incomeCauses.Contains(cause))
            {
                return false;
            }
            _incomeCauses.Add(cause);
            return true;
        }

        public Finance(Action<decimal> signalChangeSumm) 
        {
            _signalChangeSumm = signalChangeSumm;
            _budget = 0;
            _incomeCauses = new List<string>();
            _expenseCauses = new List<string>();
        }
        public Finance(int budget) { _budget = budget; }
    }
}
