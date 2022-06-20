using FinanceManager.functional.Additional;
using FinanceManager.Functional.CommonInterfaces;
using FinanceManager.Functional.GlobalPatterns.Observable;
using FinanceManager.Functional.GlobalPatterns.Observe;
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
    class SourceDataChart<T> : ISourceData, IObserver<Type>
        where T: IChartModel, new()
    {
        private Brush _brush;
        private TypeCharts _typeCharts;
        public SortedSet<T> SourceData { get; set; }

        public SourceDataChart()
        {
            SourceData = new SortedSet<T>();
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

            if (SourceData is null || SourceData.Count == 0)
            {
                return 0;
            }

            double result = SourceData.GetViewBetween(viewFrom, viewTo).Sum(element => element.Coordinate.Value);

            return result;
        }

        public void SetMark(Brush brush)
        {
            _brush = brush;

            UpdateDataObservable.Observe.PushUpdateDependenciedData(typeof(SourceDataChart<T>));
        }

        public void SetTypeOfChart(TypeCharts typeCharts)
        {
            _typeCharts = typeCharts;

            UpdateDataObservable.Observe.PushUpdateDependenciedData(typeof(SourceDataChart<T>));
        }

        public void OnCompleted()
        {
            // for logger
        }

        public void OnError(Exception error)
        {
            MessageBox.Show("Oops. Something happend i cannot update chart");
        }

        public void OnNext(Type value)
        {
            return;
        }
    }
}
