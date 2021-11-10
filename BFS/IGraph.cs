using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    public interface IGraph<T>
    {
        Dictionary<T, HashSet<T>> AdjacencyList { get; }
        void AddVertex(T vertex);
        void AddEdge(Tuple<T, T> edge);
    }
}
