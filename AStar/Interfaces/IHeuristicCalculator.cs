using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.Interfaces
{
    public interface IHeuristicCalculator<T>
    {
        double Calculate(T state);
    }
}
