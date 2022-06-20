using FinanceManager.Functional.UIItems;
using FinanceManager.Functional.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinanceManager.Functional.View
{
    /// <summary>
    /// Interaction logic for ChartStatisticView.xaml
    /// </summary>
    public partial class ChartStatisticView : Window
    {
      
        public ChartStatisticView()
        {
            InitializeComponent();
            var viewModel = DataContext as ChartStatisticViewModel;

            this.View.Children.Add(viewModel.GetChart());
        }
    }
}
