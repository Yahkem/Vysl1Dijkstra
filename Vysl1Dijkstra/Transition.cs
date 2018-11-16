using System;
using System.Collections.Generic;
using System.Text;

namespace Vysl1Dijkstra
{ 
    public class Transition
    {
        private const double MILE_TO_KM = 1.609344;
        
        private Node n1;
        private Node n2;

        public bool WasUsed { get; set; } = false;
        public bool OneDirectional { get; set; } = false;

        public Node N1
        {
            get { return n1; }
            set
            {
                n1 = value;
                n1.Transitions.Add(this);
            }
        }
        public Node N2
        {
            get { return n2; }
            set
            {
                n2 = value;
                n2.Transitions.Add(this);
            }
        }
        
        public double Km { get; set; }

        public double Miles
        {
            get => Km / MILE_TO_KM;
            set => Km = value * MILE_TO_KM;
        }
    }
}
