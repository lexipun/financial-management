using FinanceManager.Functional.GlobalPatterns.Observable;
using FinanceManager.Functional.UIItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Functional.BackgroudProcessing.ChartData
{
    class GroupDataByPeriod : IPeriod, IObservable
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
        }

        public void SetDefaultFrequency()
        {
            if((int)_period != int.MaxValue)
            {
                _frequency = (DateTime.Now - DateTime.Now.AddMonths(-(int)_period)) / 6;
                return;
            }

            throw new NotImplementedException("Default Frequency was not implemented for whole period");
        }

        public void SetPeriod(Period period)
        {
            this._period = period;
        }

        public void PushUpdateDependenciedData(Dependencies changedSource, IObserver<Dependencies> source)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<Dependencies> observer)
        {
            throw new NotImplementedException();
        }
    }
}
