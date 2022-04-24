using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.functional.Additional
{
    class TreeList<T>: List<T>
        where T: struct
    {

        public new void Add(T item)
        {
            if(Count == 0)
            {
                base.Add(item);
                return;
            }


            int position = Count / 2;
            int minimal = 0;
            int maximal = Count - 1;

            while(maximal - minimal > 1)
            {
                if(item.ToString().CompareTo(this[position].ToString()) < 0)
                {
                    maximal = position;
                    position = (int)((maximal - minimal) / 1.67) + minimal;
                }
                else
                {
                    minimal = position;
                    position = (int)((maximal - minimal) / 1.67) + minimal;
                }
            }

            Insert(maximal, item);

        }


    }
}
