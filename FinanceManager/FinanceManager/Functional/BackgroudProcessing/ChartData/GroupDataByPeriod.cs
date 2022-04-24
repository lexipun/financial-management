using FinanceManager.Functional.GlobalPatterns.Observable;
using FinanceManager.Functional.UIItems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FinanceManager.Functional.BackgroudProcessing.ChartData
{
    class GroupDataByPeriod : IPeriod, IObserver<Dependencies>
    {
        private TimeSpan _frequency;
        private Period _period;


        public TimeSpan GetFrequency()
        {
            return _frequency;
        }

        public Period GetPeriod()
        {
            return _period;
        }

        public void SetFrequency(TimeSpan frequency)
        {
            _frequency = frequency;

            UpdateDataObservable.Observe.PushUpdateDependenciedData(new Dependencies(typeof(GroupDataByPeriod)), this);
        }

        public void SetDefaultFrequency()
        {
            if((int)_period != int.MaxValue)
            {
                _frequency = (DateTime.Now - DateTime.Now.AddMonths(-(int)_period)) / 6;

                UpdateDataObservable.Observe.PushUpdateDependenciedData(new Dependencies(typeof(GroupDataByPeriod)), this);
                return;
            }

            throw new NotImplementedException("Default Frequency was not implemented for whole period");
        }

        public void SetPeriod(Period period)
        {
            this._period = period;

            UpdateDataObservable.Observe.PushUpdateDependenciedData(new Dependencies(typeof(GroupDataByPeriod)), this);
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
