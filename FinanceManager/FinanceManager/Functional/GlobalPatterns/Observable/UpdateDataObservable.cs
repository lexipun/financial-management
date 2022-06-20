using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FinanceManager.Functional.GlobalPatterns.Observe
{
    class UpdateDataObservable : IObservable<Type>
    {
        public static UpdateDataObservable Observe { get; private set; }
        protected Dictionary<Type, List<IObserver<Type>>> subscribes = new Dictionary<Type, List<IObserver<Type>>>();
        

        static UpdateDataObservable()
        {
            Observe = new UpdateDataObservable();
        }
        private UpdateDataObservable() { }

        public ISubscribeData Subscribe(IObserver<Type> currentObserver)
        {
            return new SubscribeData(subscribes, currentObserver);
        }

        public void PushUpdateDependenciedData(Type changedSource, IObserver<Type> source = null)
        {
            try
            {
                foreach (IObserver<Type> observer in subscribes[changedSource])
                {
                    observer.OnNext(changedSource);
                }

            }
            catch (Exception ex)
            {
                source?.OnError(ex);
                return;
            }

            source?.OnCompleted();
        }

        public void PushExceptionDependenciedData(Type changedSource, Exception exception)
        {
            try
            {
                foreach (IObserver<Type> observer in subscribes[changedSource])
                {
                    observer.OnError(exception);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // log it
            }
        }

        IDisposable IObservable<Type>.Subscribe(IObserver<Type> observer)
        {
            return Subscribe(observer);
        }


        #region IncludeDisposeClass

        private class SubscribeData : ISubscribeData
        {
            private IObserver<Type> _currentObserver;
            protected Dictionary<Type, List<IObserver<Type>>> subscribes;

            public SubscribeData(Dictionary<Type, List<IObserver<Type>>> subscribes, IObserver<Type> currentObserver)
            {
                this.subscribes = subscribes;
                _currentObserver = currentObserver;
            }

            public void AddSubscribeTo(Type type)
            {
                if (!subscribes.ContainsKey(type))
                {
                    subscribes.Add(type, new List<IObserver<Type>>());
                }

                subscribes[type].Add(_currentObserver);
            }

            public void Dispose()
            {
                if (_currentObserver is null)
                {
                    return;
                }

                foreach (var item in subscribes)
                {
                    if (item.Value.Contains(_currentObserver))
                    {
                        item.Value.Remove(_currentObserver);
                    }
                }
            }


        }

        #endregion
    }
}
