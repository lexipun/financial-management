using FinanceManager.Functional.CommonInterfaces;
using FinanceManager.Functional.GlobalPatterns.Observable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        Rectangles,
    }

    class Chart : IBuilder, IObserver<Type>
    {
        private const int limitPoints = 10;
        private const int minimalPoints = 2;
        private const double axisWeight = 5;
        private const double textWeight = 30;
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
                X2 = Width + 30,
                Y1 = 0,
                Y2 = 0,
                Stroke = Brushes.Black,
            };
            
            result.Children.Add(xAxis);

            Period period = Time.GetPeriod();
            TimeSpan frequency = Time.GetFrequency();
            DateTime startDay = ActualDate.AddMonths(-(int)period);
            double freqencyDistance = (ActualDate - startDay) / frequency;
            int index = 0;
            for (DateTime i = startDay; (i - ActualDate).Days < 0; i += frequency)
            {
                point = new TextBlock();

                if (string.IsNullOrEmpty(DateFormat))
                {
                    point.Text = i.ToString();
                }
                else
                {
                    point.Text = string.Concat(i.ToString(DateFormat), "-", (i + frequency).ToString(DateFormat));
                }

                Canvas.SetTop(point, axisWeight);
                double distance = (index / (freqencyDistance));
                double moveFromLeft = distance * Width + Width/freqencyDistance * 0.5 + 40 - 50;

                if (moveFromLeft < 0)
                {
                    moveFromLeft = 0;
                }
                else if (moveFromLeft > Width)
                {
                    moveFromLeft = Width;
                }


                Canvas.SetLeft(point, moveFromLeft);

                result.Children.Add(point);
                ++index;
            }

            return result;
        }
        double maxValue = 2;
        private UIElement GenerateYAxis()
        {
            
            Canvas result = new Canvas();
            TextBlock textPoint;
            double actualHeight = Height - axisWeight - textWeight;
            double stepNumberPoints = 0;
            int points = minimalPoints;

            Line axis = new Line()
            {
                X1 = textWeight,
                X2 = textWeight,
                Y1 = -30,
                Y2 = Height - 20,
                Stroke = Brushes.Black,
            };

            result.Children.Add(axis);
            

            if (columnValues is null || columnValues.Count == 0)
            {
                maxValue = 2;
            }
            else
            {
                maxValue = columnValues.Max();
            }

            points = GetCountPoints(maxValue, minimalPoints, limitPoints);
            double distanceStep = (Height - 40) / points;
            stepNumberPoints = maxValue / points;

            for (int i = points; i >= 0; --i)
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

                Canvas.SetTop(textPoint, distanceStep * (points - i) - 20);
            }

            return result;
        }

        private Rectangle GetRectangle(double height, double width, double startPoint,object context)
        {
            Rectangle result = new Rectangle()
            {
                Height = height,
                Width = width,
                Fill = Brushes.LightBlue,
                 ToolTip = context,
            };
            
            Canvas.SetTop(result, Height - height - 40);
            Canvas.SetLeft(result, startPoint + 40);

            return result;
        }
        private UIElement GenerateContent()
        {
            var result = new Canvas();

            TypeCharts type = SourceData.GetTypeOfChart();
            Period period = Time.GetPeriod();
            TimeSpan frequency = Time.GetFrequency();
            DateTime startDay = ActualDate.AddMonths(-(int)period);
            double freqencyDistance = frequency/ (ActualDate - startDay) * Width ;
            int index = 0;
            for (DateTime i = startDay; (i - ActualDate).Days < 0; i += frequency)
            {
                double value = SourceData.GetValue(i, i + frequency);
                double dataHeight = (Height - 20) / maxValue * value;
                double startPoint = freqencyDistance * index + freqencyDistance * 0.5; 


                switch (type)
                {
                    case TypeCharts.Rectangles:
                        {
                            double rectangleWidth = freqencyDistance * 0.9;
                            startPoint -= freqencyDistance * 0.5;
                            result.Children.Add(GetRectangle(dataHeight, rectangleWidth, startPoint, value));
                        }
                        break;
                    case TypeCharts.Points:
                        {

                        }
                        break;
                }

                ++index;
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
                Height = Height - maxValue / GetCountPoints(maxValue, minimalPoints, limitPoints),
            };

            GenerateChartValues();
            board.Children.Add(GenerateYAxis());
            board.Children.Add(GenerateXAxis());
            board.Children.Add(GenerateContent());

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
