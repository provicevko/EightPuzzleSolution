using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AStar;
using AStar.HeuristicFunctions;
using BFS;
using EightPuzzle.Infrastructure.Algorithms;
using Algorithm = EightPuzzle.Enums.Algorithm;

namespace EightPuzzle.Services
{
    public class PuzzleSolverService : IPuzzleSolver
    {
        private readonly IPuzzleMovement _puzzleMovement;
        public PuzzleSolverService(IPuzzleMovement puzzleMovement)
        {
            _puzzleMovement = puzzleMovement;
        }
        private byte[] StartState { get; set; }
        private byte[] EndState { get; set; }

        public IEnumerable<byte[]> FindSolution(byte[] startState, byte[] endState, Algorithm algorithm)
        {
            if (!IsValidStates(startState, endState))
                throw new ArgumentException("StartState/EndState is not valid");

            StartState = startState;
            EndState = endState;

            return algorithm switch
            {
                Algorithm.Bfs => FindBfsSolution(),
                Algorithm.AStar => FindAStarSolution(),
                _ => throw new ArgumentException(nameof(algorithm))
            };
        }

        private IEnumerable<byte[]> FindBfsSolution()
        {
            IGraph<byte[]> graph = new Graph<byte[]>(new[]{StartState}, new List<Tuple<byte[], byte[]>>());
            var visited = Algorithms.Bfs(graph, StartState, IsFinalState, _puzzleMovement.ExpandMove);
            return GetPathToEndState(ref visited);
        }

        private IEnumerable<byte[]> FindAStarSolution()
        {
            AlgTask<byte[]> task = new(StartState, EndState);
            var result = Algorithms.AStarSearch(task, IsFinalState, _puzzleMovement.ExpandMove, new EightPuzzleHeuristic(EndState));
            return result;
        }

        private bool IsFinalState(byte[] currentState)
        {
            return !currentState.SequenceEqual(EndState);
        }

        private IEnumerable<byte[]> GetPathToEndState(ref IDictionary<byte[], byte[]> visited)
        {
            if (visited == null || visited.Count == 0)
                throw new ArgumentException(nameof(visited));

            if (!visited.ContainsKey(EndState))
                return null;

            var path = new List<byte[]>();

            var current = EndState;
            var startState = StartState;

            while (!current.SequenceEqual(startState))
            {
                path.Add(current);
                current = visited[current];
            }

            path.Add(startState.ToArray());
            path.Reverse();

            return path;
        }

        private bool IsValidStates(byte[] startState, byte[] endState)
        {
            return startState != null && endState != null
                                      && startState.Distinct().Count() == startState.Length
                                      && endState.Distinct().Count() == endState.Length
                                      && startState.Length == endState.Length
                                      && Math.Sqrt(startState.Length) % 1 == 0
                                      && Math.Sqrt(endState.Length) % 1 == 0
                                      && startState.Intersect(endState).Count() == startState.Length;
        }
    }
}
