using FinanceManager.Functional.CommonInterfaces;
using FinanceManager.Functional.GlobalPatterns.Observable;
using System;
using System.Collections.Generic;
using System.Linq;
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

    class Chart :  IBuilder, IObserver<Type>
    {
        private const double axisWeight = 5;
        private const double textWeight = 60;
        private const int numberWithoutComma = 10_000;
        private const int displayingAfterComma = 2;
        private List<double> columnValues = new List<double>();

        public IPeriod Time { get; set; }
        public ISourceData SourceData { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsNeedRebuild { get; set; }
        public bool IsNeedUpdateBuild { get; set; }
        public string DateFormat { get; set; }
        public DateTime ActualDate { get; set; }

        public Chart()
        {
            ActualDate = DateTime.Now;
            
        }

        private void GenerateChartValues()
        {
            double value;
            DateTime temp;
            TimeSpan frequency = Time.GetFrequency();
            DateTime startpoint = ActualDate.AddMonths(-(int)Time.GetPeriod());

            while (startpoint < ActualDate)
            {
                temp = startpoint;
                startpoint = startpoint.AddDays(frequency.TotalDays);
                value = SourceData.GetValue(temp, startpoint);
                columnValues.Add(value);
            }
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
            DateTime startDay = ActualDate.AddMonths(-(int)period);
            double freqencyDistance = (ActualDate - startDay) / frequency;

            for (DateTime i = startDay; i < ActualDate; i += frequency)
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
                double moveFromLeft = (frequency / (freqencyDistance * (i - startDay))) * 1.5 * Width;

                if(moveFromLeft < 0)
                {
                    moveFromLeft = 0;
                }else if (moveFromLeft > Width)
                {
                    moveFromLeft = Width;
                }


                Canvas.SetLeft(point, moveFromLeft);

                result.Children.Add(point);
            }

            return result;
        }
        private UIElement GenerateYAxis()
        {
            const int limitPoints = 10;
            const int minimalPoints = 2;
            Canvas result = new Canvas();
            TextBlock textPoint;
            double actualHeight = Height - axisWeight - textWeight;
            double stepNumberPoints = 0;
            int points = minimalPoints;

            Line axis = new Line()
            {
                X1 = textWeight,
                X2 = textWeight + axisWeight,
                Y1 = 0,
                Y2 = Height,
            };

            result.Children.Add(axis);
            double maxValue;

            if(columnValues is null || columnValues.Count == 0)
            {
                maxValue = 2;
            }
            else
            {
                maxValue = columnValues.Max();
            }

            points = GetCountPoints(maxValue, minimalPoints, limitPoints);
            double distanceStep = actualHeight / points;
            stepNumberPoints = maxValue / points;

            for (int i = points; i > 0; --i)
            {
                double point = maxValue - stepNumberPoints * (points - i);

                if (maxValue > numberWithoutComma)
                {
                    point = Math.Ceiling(point / numberWithoutComma) * numberWithoutComma;
                }
                else
                {
                    point = Math.Round(point, displayingAfterComma);
                }

                textPoint = new TextBlock()
                {
                    Text = point.ToString(),
                };

                result.Children.Add(textPoint);

                Canvas.SetTop(textPoint, distanceStep * (points - i));
            }

            return result;
        }


        private int GetCountPoints(double maxValue, int minimalPoints, int limitPoints)
        {

            int result;

            if (CalculateCorrectPoint(numberWithoutComma, 0, out result))
            {
                return result;
            }

            if (CalculateCorrectPoint(1, displayingAfterComma, out result))
            {
                return result;
            }

            return minimalPoints;

            bool CalculateCorrectPoint(double minValue, int countAfterComma, out int result)
            {
                result = default;
                double tempValue;

                if (maxValue > minValue)
                {
                    tempValue = Math.Round(maxValue / limitPoints, countAfterComma);

                    if (tempValue % minValue != 0 && limitPoints > minimalPoints)
                    {
                        result = GetCountPoints(maxValue, minimalPoints, limitPoints - 1);
                        return true;
                    }

                    result = limitPoints;
                    return true;
                }

                return false;
            }
        }

        public UIElement Build()
        {

            Canvas board = new Canvas()
            {
                Width = Width,
                Height = Height,
            };

            board.Children.Add(GenerateYAxis());
            board.Children.Add(GenerateXAxis());
            GenerateChartValues();

            return board;

        }

        public void OnCompleted()
        {
            //for logger
        }

        public void OnError(Exception error)
        {
            IsNeedRebuild = true;
        }

        public void OnNext(Type value)
        {

            IsNeedUpdateBuild = true;

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
