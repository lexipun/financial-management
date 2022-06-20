using FinanceManager.Functional.BackgroudProcessing.ChartData;
using FinanceManager.Functional.UIItems.Interfaces.Chart;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Functional.Model
{
    internal class SpendModel : IChartModel
    {
        public ChartCoordinate Coordinate { get; set; }

        public int CompareTo(object obj)
        {
            SpendModel other = obj as SpendModel;
            TimeSpan difference = (Coordinate.Position - other.Coordinate.Position);
            if (difference.Days == 0)
            {
                return 0;
            }
            else if (difference.Days < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
