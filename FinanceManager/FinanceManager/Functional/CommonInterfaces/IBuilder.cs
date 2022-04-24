using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FinanceManager.Functional.CommonInterfaces
{
    interface IBuilder
    {
        bool IsNeedRebuild { get; set; }
        bool IsNeedUpdateBuild { get; set; }
        UIElement Build();
        void UipdateBuild();
        void Rebuild();
    }
}
