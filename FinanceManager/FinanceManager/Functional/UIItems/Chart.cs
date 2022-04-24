﻿using FinanceManager.Functional.CommonInterfaces;
using FinanceManager.Functional.GlobalPatterns.Observable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

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

    class Chart:UIElement, IBuilder, IObserver<Dependencies>
    {
        private const double axisWeight = 5;
        private const double textWeight = 60;
        public IPeriod Time { get; set; }
        public ISourceData SourceData { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsNeedRebuild { get ; set ; }
        public bool IsNeedUpdateBuild { get; set; }
        public string DateFormat { get; set; }
        public DateTime ActualDate { get; set; }

        public Chart()
        {
            ActualDate = DateTime.Now;
        }


        private UIElement GenerateXAxis()
        {
            const double distanceOfAxisFromBot = 40;

            TextBlock point;
            Canvas result = new Canvas();

            Canvas.SetLeft(result, 0);
            Canvas.SetTop(result, Height - distanceOfAxisFromBot);

            Line xAxis = new Line()
            {
                X1 = 0,
                X2 = Width,
                Y1 = 0,
                Y2 = axisWeight,
            };

            result.Children.Add(xAxis);

            Period period = Time.GetPeriod();
            TimeSpan frequency = Time.GetFrequency();
            DateTime startDay = ActualDate.AddDays(-(int)period);
            double freqencyDistance = (ActualDate - startDay) / frequency;

            for(DateTime i = startDay; i < ActualDate; i += frequency)
            {
                point = new TextBlock();

                if (string.IsNullOrEmpty(DateFormat))
                {
                    point.Text = i.ToString();
                }
                else
                {
                    point.Text = i.ToString(DateFormat);
                }

                Canvas.SetTop(point, axisWeight);
                Canvas.SetLeft(point, freqencyDistance * (i - startDay) / frequency);

                result.Children.Add(point);
            }

            return result;
        }
        private UIElement GenerateYAxis()
        {
            Canvas result = new Canvas();

            Line axis = new Line()
            {
                X1 = textWeight,
                X2 = textWeight + axisWeight,
                Y1 = 0,
                Y2 = Height,
            };

            result.Children.Add(axis);



        }

        public UIElement Build()
        {
            throw new NotImplementedException();

            Canvas board = new Canvas();    

            


        }

        public void OnCompleted()
        {
            //for logger
        }

        public void OnError(Exception error)
        {
            IsNeedRebuild = true;
        }

        public void OnNext(Dependencies value)
        {
            foreach(Type type in value)
            {
                if(type is IPeriod || type is ISourceData)
                {
                    IsNeedUpdateBuild = true;
                }
            }
        }

        public void Rebuild()
        {
            throw new NotImplementedException();
        }

        public void UpdateBuild()
        {
            throw new NotImplementedException();
        }
    }
}
