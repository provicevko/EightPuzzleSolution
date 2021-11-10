using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFS.Infrastructure;

namespace BFS
{
    public static class Algorithm
    {
        public static Dictionary<byte[], byte[]> StoppedExpandedBfs(IGraph<byte[]> graph, byte[] start, Predicate<byte[]> expandCondition, Func<byte[], IEnumerable<byte[]>> expandedStates)
        {
            if (expandCondition == null)
                throw new ArgumentNullException(nameof(expandCondition));

            if (expandedStates == null)
                throw new ArgumentNullException(nameof(expandedStates));

            var visited = new Dictionary<byte[], byte[]>(new ByteArrayStructuralComparer());

            if (!graph.AdjacencyList.ContainsKey(start))
                return visited;

            var queue = new Queue<byte[]>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();

                if (expandCondition.Invoke(vertex))
                {
                    var expanded = expandedStates.Invoke(vertex);

                    if (expanded != null)
                        AddStatesToGraph(graph, vertex, expanded);
                }
                else
                    return visited;

                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (visited.ContainsKey(neighbor))
                        continue;

                    visited[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            return visited;
        }
        private static void AddStatesToGraph<T>(IGraph<T> graph, T node, IEnumerable<T> states)
        {
            foreach (var state in states)
            {
                graph.AddVertex(state);
                graph.AddEdge(Tuple.Create(node, state));
            }
        }
    }
}

