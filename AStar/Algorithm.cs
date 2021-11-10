using AStar.Infrastructure;
using AStar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public static class Algorithm
    {
        public static IEnumerable<byte[]> Search(IAlgInitializator<byte[]> algInitializator, Predicate<byte[]> expandCondition, Func<byte[], IEnumerable<byte[]>> expandedStates, IHeuristicCalculator<byte[]> heuristicCalculator)
        {
            if (expandCondition == null)
                throw new ArgumentNullException(nameof(expandCondition));

            if (expandedStates == null)
                throw new ArgumentNullException(nameof(expandedStates));

            ValidateAlgInitStates(algInitializator);

            var startState = algInitializator.InitialState;

            var visited = new HashSet<byte[]>(new ByteArrayStructuralComparer());
            var priorityQueue = new PriorityQueue<Node<byte[]>, double>();

            var root = new Node<byte[]>(new State<byte[]>(startState));

            priorityQueue.Enqueue(root, root.EvaluationFunction(heuristicCalculator));

            while (priorityQueue.Count > 0)
            {
                var node = priorityQueue.Dequeue();
                visited.Add(node.State.Value);

                if (!expandCondition.Invoke(node.State.Value))
                {
                    return node.PathFromRootStates();
                }

                var expanded = expandedStates.Invoke(node.State.Value);

                if (expanded != null)
                {
                    foreach (var childStateValue in expanded)
                    {
                        if (!visited.Contains(childStateValue))
                        {
                            var newChild = new Node<byte[]>(new State<byte[]>(childStateValue), node);
                            priorityQueue.Enqueue(newChild, newChild.EvaluationFunction(heuristicCalculator));
                        }
                    }
                }
            }

            return null;
        }

        private static double EvaluationFunction(this Node<byte[]> node, IHeuristicCalculator<byte[]> heuristicCalculator)
        {
            return node.PathCost + heuristicCalculator.Calculate(node.State.Value);
        }

        private static void ValidateAlgInitStates<T>(IAlgInitializator<T> algInitializator)
        {
            if (algInitializator == null)
                throw new ArgumentNullException(nameof(algInitializator));

            if (algInitializator.InitialState == null)
                throw new ArgumentNullException(nameof(algInitializator.InitialState));

            if (algInitializator.FinalState == null)
                throw new ArgumentNullException(nameof(algInitializator.FinalState));
        }
    }
}
