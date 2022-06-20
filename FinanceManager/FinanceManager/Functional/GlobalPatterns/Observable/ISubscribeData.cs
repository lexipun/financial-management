using System;

namespace FinanceManager.Functional.GlobalPatterns.Observe
{
    internal interface ISubscribeData : IDisposable
    {
        void AddSubscribeTo(Type type);
    }
}