using System.Collections.Generic;

namespace Core
{
    public class Node<S>
    {
        private S initialState;

        public Node(S initialState, object parent)
        {
            this.initialState = initialState;
            Parent = parent;
        }

        public object Parent { get; internal set; }

        public S State { get { return initialState; } }
    }

    public class Node
    {
        private string _name;
        private Dictionary<Node, double> _neighbours;
        private bool _seen;
        private Node _parent;

        public Node(string name, Dictionary<Node, double> neighbours, Node parent, bool seen = false)
        {
            _name = name;
            _neighbours = neighbours;
            _parent = parent;
        }

        public string Name { get { return _name; } }

        public Dictionary<Node, double> Neighbours { get { return _neighbours; } set { _neighbours = value; } }

        public bool Seen { get { return _seen; } set { _seen = value; } }

        public Node Parent { get { return _parent; } set { _parent = value; } }
    }
}