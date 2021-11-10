using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.Interfaces
{
    public interface IAlgInitializator<T>
    {
        public T InitialState { get; set; }

        public T FinalState { get; set; }
    }
}
