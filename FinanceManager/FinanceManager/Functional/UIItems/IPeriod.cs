using System;

namespace FinanceManager.Functional.UIItems
{
    public interface IPeriod
    {
        TimeSpan GetFrequency();
        IPeriod GetPeriod();
    }
}