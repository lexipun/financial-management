using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Functional.GlobalPatterns.Observable
{
    interface IPersonalObserver: IObservable<Type>
    {
        void PushUpdateDependenciedData(Type changedSource, IObserver<Type> source);
    }
}
