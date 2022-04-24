using FinanceManager.Functional.CommonInterfaces;
using FinanceManager.Functional.GlobalPatterns.Observable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FinanceManager.Functional.UIItems
{
    public enum Period
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

    class Chart:UIElement, IBuilder, IObservable
    {
        public IPeriod Time { get; set; }
        public ISourceData SourceData { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsNeedRebuild { get ; set ; }
        public bool IsNeedUpdateBuild { get; set; }

        public UIElement Build()
        {
            throw new NotImplementedException();
        }

        public void PushUpdateDependenciedData(Dependencies changedSource, IObserver<Dependencies> source)
        {
            throw new NotImplementedException();
        }

        public void Rebuild()
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<Dependencies> observer)
        {
            throw new NotImplementedException();
        }

        public void UipdateBuild()
        {
            throw new NotImplementedException();
        }
    }
}
