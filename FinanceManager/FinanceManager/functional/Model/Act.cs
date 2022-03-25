using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FinanceManager.functional.Model
{
    [Serializable]
    public class Act
    {
        public DateTime LastChange { get; set; } = DateTime.Now;
        public string cause { get; set; }
        public decimal amount { get; set; }

    }
}
