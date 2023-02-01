namespace AdventOfCode07
{
    public class Node
    {
        public string? Name { get; set; }
        public int Size { get; set; }
        public NodeType NodeType { get; set; }
        public Node? Parent { get; set; }
        public List<Node> Children { get; set; }

        public Node(string name, NodeType nodeType, Node? parent = null)
        {
            Name = name;
            NodeType = nodeType;
            Parent = parent;
            Children = new List<Node>();
        }

        public int GetSize()
        {
            var childrenSize = Children != null ? Children.Sum(x => x.GetSize()) : 0;
            return Size + childrenSize;
        }
    }
}
