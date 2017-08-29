using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    public class Graph
    {
        public Graph(int[] nodes, List<Tuple<int, int>> adjacentnodes)
        {
            foreach (var node in nodes)
            {
                AddNode(node);
            }

            foreach (var adjacentnode in adjacentnodes)
            {
                AddAdjacentNode(adjacentnode);
            }
        }

        public Dictionary<int, HashSet<int>> AdjacencyList { get; } = new Dictionary<int, HashSet<int>>();

        public void AddNode(int node)
        {
            AdjacencyList[node] = new HashSet<int>();
        }

        public void AddAdjacentNode(Tuple<int, int> adjacentnode)
        {
            if (AdjacencyList.ContainsKey(adjacentnode.Item1) && AdjacencyList.ContainsKey(adjacentnode.Item2))
            {
                AdjacencyList[adjacentnode.Item1].Add(adjacentnode.Item2);
                AdjacencyList[adjacentnode.Item2].Add(adjacentnode.Item1);
            }
        }

        public List<int> ShortestPathFunction(Graph graph, int startnode, int endnode)
        {
            var previous = new Dictionary<int, int>();

            var queue = new Queue<int>();
            queue.Enqueue(startnode);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                foreach (var adjacentnode in graph.AdjacencyList[node])
                {
                    if (previous.ContainsKey(adjacentnode))
                        continue;

                    previous[adjacentnode] = node;
                    queue.Enqueue(adjacentnode);
                }
            }

            var path = new List<int>();
            var current = endnode;
            while (!current.Equals(startnode))
            {
                path.Add(current);
                current = previous[current];
            };

            path.Add(startnode);
            path.Reverse();

            return path;
        }
    }
}
