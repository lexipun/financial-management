using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Functional.GlobalPatterns.Observe
{
    class Dependencies: IEnumerable, IEnumerable<Type>
    {
        private List<Type> _types;
        public Type this[int index]
        {
            get
            {
                if (index < 0 || index >= _types.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                return _types[index];
            }
        }

        public Dependencies(IEnumerable<Type> types)
        {
            _types = new List<Type>(types);
        }

        public Dependencies(params Type[] types)
        {
            _types = new List<Type>();

            foreach (Type type in types)
            {
                _types.Add(type);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _types.GetEnumerator();
        }

        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            return _types.GetEnumerator();
        }
    }
}
