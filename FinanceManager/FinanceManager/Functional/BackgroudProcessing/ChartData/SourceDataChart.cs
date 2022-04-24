using FinanceManager.functional.Additional;
using FinanceManager.Functional.CommonInterfaces;
using FinanceManager.Functional.GlobalPatterns.Observable;
using FinanceManager.Functional.UIItems;
using FinanceManager.Functional.UIItems.Interfaces.Chart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace FinanceManager.Functional.BackgroudProcessing.ChartData
{
    class SourceDataChart<T> : ISourceData, IObserver<Dependencies>
        where T: IChartModel, new()
    {
        private Brush _brush;
        private TypeCharts _typeCharts;
        public SortedSet<T> SourceData { get; set; }

        public SourceDataChart()
        {

        }

        public Brush GetMark()
        {
            return _brush;
        }

        public TypeCharts GetTypeOfChart()
        {
            return _typeCharts;
        }

        public double GetValue(DateTime from, DateTime to)
        {
            T viewFrom = new T()
            {
                Coordinate = new ChartCoordinate() { Position = from, },
            };

            T viewTo = new T()
            {
                Coordinate = new ChartCoordinate() { Position = to, },
            };

            double result = SourceData.GetViewBetween(viewFrom, viewTo).Sum(element => element.Coordinate.Value);

            return result;
        }

        public void SetMark(Brush brush)
        {
            _brush = brush;

            UpdateDataObservable.Observe.PushUpdateDependenciedData(new Dependencies(typeof(SourceDataChart<T>)));
        }

        public void SetTypeOfChart(TypeCharts typeCharts)
        {
            _typeCharts = typeCharts;

            UpdateDataObservable.Observe.PushUpdateDependenciedData(new Dependencies(typeof(SourceDataChart<T>)));
        }

        public void OnCompleted()
        {
            // for logger
        }

        public void OnError(Exception error)
        {
            MessageBox.Show("Oops. Something happend i cannot update chart");
        }

        public void OnNext(Dependencies value)
        {
            return;
        }
    }
}
