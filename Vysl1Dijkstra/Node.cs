using System;
using System.Collections.Generic;
using System.Text;

namespace Vysl1Dijkstra
{
    public class Node
    {
        /// <summary>
        /// Minimum distance to start. In Km, null==Inf
        /// </summary>
        public double? MinKmDistanceToStart { get; set; }

        /// <summary>
        /// Predecessor node
        /// </summary>
        public Node NearestNeighborToStart { get; set; }

        public HashSet<Transition> Transitions { get; set; } = new HashSet<Transition>();
        public string Name { get; set; }
        public bool WasVisited { get; set; } = false;

        /// <summary>
        /// Connection == reachable Node+distance to it
        /// </summary>
        /// <returns></returns>
        public HashSet<(Node target, double kmDistance)> 
            GetReachableConnections()
        {
            var result = new HashSet<(Node target, double kmDistance)>();

            foreach (var t in Transitions)
            {
                if (t.N1 == this)
                    result.Add((t.N2, t.Km));
                else if (!t.OneDirectional)
                    result.Add((t.N1, t.Km));
            }

            return result;
        }
    }
}
