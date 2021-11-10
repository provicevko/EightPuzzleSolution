using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    public class Graph<T>: IGraph<T>
    {
        public Graph() => AdjacencyList = new();
        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges) : this(vertices, edges, null)
        {
        }

        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges, IEqualityComparer<T> comparer)
        {
            AdjacencyList = comparer == null ? new() : new(comparer);

            foreach (var vertex in vertices)
                AddVertex(vertex);

            foreach (var edge in edges)
                AddEdge(edge);
        }

        public Dictionary<T, HashSet<T>> AdjacencyList { get; }

        public void AddVertex(T vertex)
        {
            AdjacencyList[vertex] = new HashSet<T>();
        }

        public void AddEdge(Tuple<T, T> edge)
        {
            if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
                AdjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }
}
