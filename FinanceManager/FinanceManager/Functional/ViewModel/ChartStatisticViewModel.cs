using FinanceManager.functional.Model;
using FinanceManager.Functional.BackgroudProcessing.ChartData;
using FinanceManager.Functional.Model;
using FinanceManager.Functional.UIItems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinanceManager.Functional.ViewModel
{
    internal class ChartStatisticViewModel
    {
        private Chart chart;
        public ChartStatisticViewModel()
        {
           
        }

        public UIElement GetChart()
        {
            GroupDataByPeriod dataByPeriod = new GroupDataByPeriod();

            dataByPeriod.SetPeriod(Period.month);
            dataByPeriod.SetDefaultFrequency();
            SourceDataChart<SpendModel> sourceData = new SourceDataChart<SpendModel>();

            sourceData.SetMark(Brushes.Red);
            sourceData.SetTypeOfChart(TypeCharts.Rectungles);
            FillData(sourceData.SourceData);

            chart = new Chart()
            {
                ActualDate = DateTime.Now,
                Height = 200,
                Width = 800,
                Time = dataByPeriod,
                SourceData = sourceData,
                DateFormat = "dd.MM",
            };

            chart.ActualDate = DateTime.Now;

            return chart.Build();
        }

        private void FillData(SortedSet<SpendModel> collection)
        {
            foreach(Act act in MainWindow.myFinance.StoryActs)
            {
                ChartCoordinate chartCoordinate = new ChartCoordinate()
                {
                    Position = act.LastChange,
                    Value = (double)act.amount,

                };
                SpendModel spendModel = new SpendModel()
                {
                    Coordinate = chartCoordinate,
                };

                collection.Add(spendModel);
            }
        }
    }
}
