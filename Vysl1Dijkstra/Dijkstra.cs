using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vysl1Dijkstra
{
    class Dijkstra
    {
        public Node Start { get; set; }
        public Node End { get; set; }

        public List<Node> GetShortestPathDijkstra()
        {
            var shortestPath = new List<Node>();
            
            DijkstraSearch();

            shortestPath.Add(End);
            BuildShortestPath(shortestPath, End);
            shortestPath.Reverse();

            return shortestPath;
        }

        private void BuildShortestPath(List<Node> list, Node node)
        {
            if (node.NearestNeighborToStart == null)
                return;

            list.Add(node.NearestNeighborToStart);

            BuildShortestPath(list, node.NearestNeighborToStart);
        }

        private void DijkstraSearch()
        {
            var prioQueue = new List<Node>();

            Start.MinKmDistanceToStart = 0;
            prioQueue.Add(Start);

            do
            {
                // sort priority queue
                prioQueue = prioQueue
                    .OrderBy(x => x.MinKmDistanceToStart)
                    .ToList();

                // take the node with the shortest path to start
                var node = prioQueue.First();
                prioQueue.Remove(node);

                foreach ((Node target, double kmDistance) in 
                    node.GetReachableConnections().OrderBy(x => x.kmDistance))
                {
                    var childNode = target;
                    if (childNode.WasVisited)
                        continue;

                    // targetDistance==inf OR targetDistance > (currentDistance+distToTarget) -> update, add to priority queue
                    if (childNode.MinKmDistanceToStart == null ||
                        node.MinKmDistanceToStart + kmDistance < childNode.MinKmDistanceToStart)
                    {
                        childNode.MinKmDistanceToStart = node.MinKmDistanceToStart + kmDistance;
                        childNode.NearestNeighborToStart = node;

                        if (!prioQueue.Contains(childNode))
                            prioQueue.Add(childNode);
                    }
                }

                node.WasVisited = true;

                if (node == End)
                    return;

            } while (prioQueue.Any());
        }
    }
}
