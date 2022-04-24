using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Functional.GlobalPatterns.Observable
{
    interface IPersonalObserver: IObservable<Dependencies>
    {
        void PushUpdateDependenciedData(Dependencies changedSource, IObserver<Dependencies> source);
    }
}
