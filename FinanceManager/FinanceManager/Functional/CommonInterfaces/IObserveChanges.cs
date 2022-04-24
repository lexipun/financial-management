using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Functional.CommonInterfaces
{
    interface IObserveChanges
    {
        DateTime LastChange { get; set; }
    }
}
