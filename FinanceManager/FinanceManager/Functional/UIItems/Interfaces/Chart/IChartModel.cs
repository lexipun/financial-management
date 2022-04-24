using FinanceManager.Functional.BackgroudProcessing.ChartData;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Functional.UIItems.Interfaces.Chart
{
    interface IChartModel: IComparable
    {
        ChartCoordinate Coordinate { get; set; } 
    }
}
