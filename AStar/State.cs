using AStar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class State<T> : IStateValuable
    {
        public State(T value)
        {
            Value = value;
        }
        public virtual int Cost => 1;
        public T Value { get; private set; }
    }
}
