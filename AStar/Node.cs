using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Node<T>
    {
        public Node(State<T> state) => State = state;

        public Node(State<T> state, Node<T> parent)
            : this(state)
        {
            Parent = parent;
            if (Parent != null)
                PathCost = Parent.PathCost + state.Cost;
        }

        public State<T> State { get; }

        public Node<T> Parent { get; }

        public int PathCost { get; }

        public bool IsRootNode => Parent == null;

        public IEnumerable<Node<T>> PathFromRoot()
        {
            var path = new Stack<Node<T>>();

            var node = this;
            while (!node.IsRootNode)
            {
                path.Push(node);
                node = node.Parent;
            }
            path.Push(node);

            return path;
        }

        public IEnumerable<T> PathFromRootStates()
        {
            return PathFromRoot().Select(n => n.State.Value);
        }
    }
}
