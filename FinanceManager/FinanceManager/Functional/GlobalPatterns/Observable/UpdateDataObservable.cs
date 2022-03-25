using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Functional.GlobalPatterns.Observable
{
    class UpdateDataObservable : IObservable
    {
        private List<IObserver<Dependencies>> _observers = new List<IObserver<Dependencies>>();

        public IDisposable Subscribe(IObserver<Dependencies> currentObserver)
        {
            if (!_observers.Contains(currentObserver))
            {
                _observers.Add(currentObserver);
            }

            return new Unsubscribe(_observers, currentObserver);
        }

        public void PushUpdateDependenciedData(Dependencies changedSource, IObserver<Dependencies> source)
        {
            try
            {
                foreach (IObserver<Dependencies> observer in _observers)
                {
                    observer.OnNext(changedSource);
                }
            }catch( Exception ex)
            {
                foreach (IObserver<Dependencies> observer in _observers)
                {
                    observer.OnError(ex);
                }
                return;
            }

            source.OnCompleted();
        }

        #region IncludeDisposeClass

        private class Unsubscribe : IDisposable
        {
            private List<IObserver<Dependencies>> _observers;
            private IObserver<Dependencies> _currentObserver;

            public Unsubscribe(List<IObserver<Dependencies>> observers, IObserver<Dependencies> currentObserver)
            {
                _observers = observers;
                _currentObserver = currentObserver;
            }


            public void Dispose()
            {
                if (_currentObserver != null && _observers.Contains(_currentObserver))
                {
                    _observers.Remove(_currentObserver);
                }
            }


        }

        #endregion
    }
}
