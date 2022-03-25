using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Functional.GlobalPatterns.Observable
{
    interface IObservable: IObservable<Dependencies>
    {
        void PushUpdateDependenciedData(Dependencies changedSource, IObserver<Dependencies> source);
    }
}
