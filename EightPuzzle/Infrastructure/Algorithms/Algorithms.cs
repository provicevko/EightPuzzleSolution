using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AStar.Interfaces;
using BFS;

namespace EightPuzzle.Infrastructure.Algorithms
{
    public class Algorithms
    {
        public static IDictionary<byte[], byte[]> Bfs(IGraph<byte[]> graph, byte[] start, Predicate<byte[]> expandCondition, Func<byte[], IEnumerable<byte[]>> expandedStates) =>
            BFS.Algorithm.StoppedExpandedBfs(graph, start, expandCondition, expandedStates);
        public static IEnumerable<byte[]> AStarSearch(IAlgInitializator<byte[]> algInitializator, Predicate<byte[]> expandCondition, Func<byte[], IEnumerable<byte[]>> expandedStates, IHeuristicCalculator<byte[]> heuristicCalculator) =>
            AStar.Algorithm.Search(algInitializator, expandCondition, expandedStates, heuristicCalculator);
    }
}
