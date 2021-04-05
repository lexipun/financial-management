using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.functional
{
    [Serializable]
    public class Binder
    {
        protected Dictionary<string, decimal> _historyOfIncomesAndExpenses = new Dictionary<string, decimal>();
    }
}
