using System;
using System.Windows.Media;

namespace FinanceManager.Functional.UIItems
{
    internal interface ISourceData
    {
        double GetValue(DateTime from, DateTime To);
        Brush GetMark();
        TypeCharts GetTypeOfChart();
    }
}