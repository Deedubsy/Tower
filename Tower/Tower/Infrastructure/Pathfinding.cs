using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower.Infrastructure
{
    public class PathFinder
    {
        public class Node
        {
            public Graph.Node node = null;
            public Node parent = null;
            public int degreesOfSeporation = 0;
            public float gScore = 0;
            public float hScore = 0;
            public float fScore = 0;

            public Node() { }
            public Node(Graph.Node n)
            {
                node = n;
            }
        }

        public enum Type
        {
            BREDTH_FIRST_SEARCH,
            DIJKSTRAS,
            ASTAR,
        }

        public List<Node> OpenList { get; private set; }
        public List<Node> ClosedList { get; private set; }

        public List<Graph.Node> Path { get; private set; }

        public delegate bool TEndConditionFunc(Graph.Node n);

        private Type m_pathFindType = Type.DIJKSTRAS;

        private TEndConditionFunc m_endConditionFunc = null;
        private Graph.Node m_endNode = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public PathFinder()
        {
            OpenList = new List<Node>();
            ClosedList = new List<Node>();
            Path = new List<Graph.Node>();
        }

        /// <summary>
        /// To begin searching for a path to a single node, you may call this function
        /// </summary>
        /// <param name="start">The node that represents the beginning of the path</param>
        /// <param name="end">The node that represents the end of the path</param>
        /// <param name="pathFindType">you can specify either AStar, Dijkstras or Bredth first search</param>
        public void BeginPathFinding(Graph.Node start, Graph.Node end, Type pathFindType)
        {
            Reset();

            OpenList.Add(new Node(start));
            m_endNode = end;
            m_pathFindType = pathFindType;

            m_endConditionFunc = delegate (Graph.Node node)
            {
                return m_endNode == node;
            };
        }

        /// <summary>
        /// To begin searching for a path to a single node, you may call this function
        /// </summary>
        /// <param name="start">The node that represents the beginning of the path</param>
        /// <param name="potentialEnd">A path will be found to one of the nodes in this list</param>
        /// <param name="pathFindType">you can specify either AStar, Dijkstras or Bredth first search.
        /// Note: If AStar is chosen and there is more than one item in the potentialEndList than Dijkstras will be used instead</param>
        public void BeginPathFinding(Graph.Node start, List<Graph.Node> potentialEnd, Type pathFindType)
        {
            Reset();
            OpenList.Add(new Node(start));

            m_pathFindType = pathFindType;
            if (m_pathFindType == Type.ASTAR)
                m_pathFindType = Type.DIJKSTRAS;

            m_endConditionFunc = delegate (Graph.Node node)
            {
                return potentialEnd.Contains(node);
            };
        }

        /// <summary>
        /// Clears the OpenList, ClosedList and Path
        /// </summary>
        public void Reset()
        {
            Path.Clear();
            OpenList.Clear();
            ClosedList.Clear();

            m_endConditionFunc = null;
            m_endNode = null;
        }

        /// <summary>
        /// returns true if the path finding process has not finished, usefull to wrap in a while loop
        /// </summary>
        /// <returns>returns true if the path finding process has not finished</returns>
        public bool RequiresUpdate()
        {
            if (IsPathFound())
                return false;

            if (OpenList.Count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Runs a single iteration of the Path finding algorithm
        /// Calling this function once every frame will cause a single node to be processed each frame until the path is found
        /// Calling this function inside a loop - eg: while( RequiresUpdate() ) Update(); 
        /// will cause the program to hault until the path is found.
        /// </summary>
        public void Update()
        {
            if (m_endConditionFunc == null)
                return;

            if (IsPathFound())
                return;

            if (OpenList.Count == 0)
                return;

            Node node = OpenList.Last();
            OpenList.RemoveAt(OpenList.Count - 1);

            ClosedList.Add(node);

            // if we found the end node...
            // the endConditionFunc should return true if this is a porential end node...
            if (m_endConditionFunc(node.node))
            {
                // calculate a path
                Node current = node;
                while (current != null)
                {
                    Path.Add(current.node);
                    current = current.parent;
                }
            }

            ProcessNode(node);

            // sort the open list based on which type of algorithm we are trying to preform.
            OpenList.Sort(delegate (Node a, Node b)
            {
                switch (m_pathFindType)
                {
                    case Type.BREDTH_FIRST_SEARCH:
                        if (a.degreesOfSeporation == b.degreesOfSeporation) return 0;
                        if (a.degreesOfSeporation > b.degreesOfSeporation) return -1;
                        if (a.degreesOfSeporation < b.degreesOfSeporation) return 1;
                        break;

                    case Type.DIJKSTRAS:
                        if (a.gScore == b.gScore) return 0;
                        if (a.gScore > b.gScore) return -1;
                        if (a.gScore < b.gScore) return 1;
                        break;

                    case Type.ASTAR:
                        if (a.fScore == b.fScore) return 0;
                        if (a.fScore > b.fScore) return -1;
                        if (a.fScore < b.fScore) return 1;
                        break;
                }

                throw new Exception("Unable to sort");
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the path is found, false otherwise</returns>
        public bool IsPathFound()
        {
            return Path.Count > 0;
        }

        private void ProcessNode(Node parent)
        {
            foreach (Graph.Edge edge in parent.node.Edges)
            {
                Graph.Node node = edge.Connection;

                // calculate the G, H and F score for the node
                //----------------------
                int dos = parent.degreesOfSeporation + 1;

                float h = 0;

                if (m_endNode != null)
                    h = (m_endNode.Position - node.Position).LengthSquared();

                float g = parent.gScore + edge.Cost;
                float f = g + h;
                //----------------------

                bool canAddNode = true;


                // is the node in the open list?
                foreach (Node n in OpenList)
                {
                    if (n.node == node)
                    {
                        // if its faster getting to this node from this parent, than update the node
                        if (g < n.gScore)
                        {
                            n.parent = parent;
                            n.gScore = g;
                            n.hScore = h;
                            n.fScore = f;
                            n.degreesOfSeporation = dos;
                        }
                        canAddNode = false;
                        break;
                    }
                }

                // is the node in the closed list?
                foreach (Node n in ClosedList)
                {
                    if (n.node == node)
                    {
                        // if its faster getting to this node from this parent, than update the node
                        if (g < n.gScore)
                        {
                            n.parent = parent;
                            n.gScore = g;
                            n.hScore = h;
                            n.fScore = f;
                            n.degreesOfSeporation = dos;
                        }
                        canAddNode = false;
                        break;
                    }
                }

                // if the node is not in the open or closed lists
                // than we can add the node.
                if (canAddNode)
                {
                    Node newNode = new Node(node);
                    newNode.parent = parent;
                    newNode.gScore = g;
                    newNode.hScore = h;
                    newNode.fScore = f;
                    newNode.degreesOfSeporation = dos;

                    OpenList.Add(newNode);
                }
            }
        }
    }

    public class Graph
    {
        public class Node
        {
            public uint ID { get; private set; }

            public Vector2 Position = new Vector2();
            public List<Edge> Edges = new List<Edge>();

            private static uint ms_nextID = 0;

            public Node()
            {
                ID = ms_nextID;
                ms_nextID += 1;
            }

            public Node(Vector2 position)
            {
                Position = position;

                ID = ms_nextID;
                ms_nextID += 1;
            }
        }

        public class Edge
        {
            public Node Connection = null;
            public float Cost = 0;

            public Edge() { }

            public Edge(Node connection, float cost)
            {
                Connection = connection;
                Cost = cost;
            }
        }

        public List<Node> Nodes { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public Graph()
        {
            Nodes = new List<Node>();
        }

        /// <summary>
        /// Adds a node to the graph
        /// </summary>
        /// <param name="position">represents the position to add the node</param>
        /// <returns>the node added to the graph if successfull</returns>
        public Node AddNode(Vector2 position)
        {
            Nodes.Add(new Node(position));
            return Nodes.Last();
        }

        /// <summary>
        /// Adds an edge between nodes
        /// </summary>
        /// <param name="node1">an edge will be created FROM this node, connecting to node2</param>
        /// <param name="node2">an edge will be created TO this node, connecting from node1</param>
        /// <param name="cost">the cost to traverse from node1 to node2. required for path finding</param>
        public void AddConnection(Node node1, Node node2, float cost)
        {
            if (node1 == null || node2 == null)
                return;

            node1.Edges.Add(new Edge(node2, cost));
        }

        /// <summary>
        /// searches through the collection of nodes to find all nodes within a radious
        /// </summary>
        /// <param name="pos">represents the mid point, nodes around this point within the radious will be found</param>
        /// <param name="range">represents the radious, nodes within this radious will be found</param>
        /// <returns>A list of nodes around the the given position</returns>
        public List<Node> FindNodesInRange(Vector2 pos, float range)
        {
            List<Node> nodeList = new List<Node>();
            range = 40;
            for (int i = 0; i < Nodes.Count; i++)
            {
                float length = (Nodes[i].Position - pos).Length();
                if ((Nodes[i].Position - pos).Length() < range)
                    nodeList.Add(Nodes[i]);
            }

            return nodeList;
        }

        /// <summary>
        /// Helper function, Gets the node a matching unique ID value.
        /// </summary>
        /// <param name="ID">the id of the node to get</param>
        /// <returns>the node matching the same id value, null otherwise</returns>
        public Node GetNode(int ID)
        {
            foreach (Node n in Nodes)
            {
                if (n.ID == ID)
                    return n;
            }

            return null;
        }
    }
}
