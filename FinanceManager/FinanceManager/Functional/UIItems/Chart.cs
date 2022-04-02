using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FinanceManager.Functional.UIItems
{
    enum Period
    {
        month = 1,
        quarter = 3,
        sixMonths = 6,
        year = 12,
        WholePeriod = int.MaxValue,
    }

    public enum TypeCharts
    {
        Line,
        Points,
        Rectungles,
    }

    class Chart:UIElement
    {
        public IPeriod Time { get; set; }
        public List<ISourceData> SourceData { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }


    }
}
