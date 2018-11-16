using System;
using System.Linq;
using System.Collections.Generic;

namespace Vysl1Dijkstra
{
    class Program
    {
        static string FormatDouble(double? d) => d.GetValueOrDefault().ToString("0.00");

        static void Main(string[] args)
        {
            //a) Prague - Ruzyne International Airport -> Indira Gandhi International Airport
            //b) Bremen Airport -> Sunan International Airport
            //c) John F Kennedy International Airport->Cairo International Airport
            //d) Dublin Airport -> Domodedovo Airport
            //e) Naha Airport -> Evreux Airport

            var flights = new (string startName, string endName)[]
            {
                ("PRG", "DEL"),
                ("BRE", "FNJ"),
                ("JFK", "CAI"),
                ("DUB", "DME"),
                ("OKA", "EVX")
            };

            foreach (var (startName, endName) in flights)
            {
                var graph = GenerateGraph(startName, endName);

                Console.WriteLine($"Finding shortest flight path from {startName} to {endName}:");

                var dijkstra = new Dijkstra
                {
                    Start = graph.startingNode,
                    End = graph.endingNode
                };

                var list = dijkstra.GetShortestPathDijkstra();

                Console.WriteLine("  Path found, total flight distance: " +
                    $"{FormatDouble(dijkstra.End.MinKmDistanceToStart)}km");

                double currentDistToStart = 0;
                foreach (var item in list)
                {
                    if (item.NearestNeighborToStart != null)
                    {
                        double minKmToStart = item.MinKmDistanceToStart.GetValueOrDefault();
                        double distFromPrevious = minKmToStart - currentDistToStart;
                        currentDistToStart = minKmToStart;

                        Console.WriteLine($"    [{item.NearestNeighborToStart.Name}]" +
                            $"--({FormatDouble(distFromPrevious).PadLeft(8)}km )-->[{item.Name}]");
                    }
                }
            }

            Console.ReadKey();
        }

        private static (HashSet<Node> nodes, Node startingNode, Node endingNode) 
            GenerateGraph(string startingNodeName, string endingNodeName)
        {
            (HashSet<Node> nodes, Node startingNode, Node endingNode) 
                result = (null, null,null);

            Node
                prg = new Node { Name = "PRG" },
                lhr = new Node { Name = "LHR" },
                bre = new Node { Name = "BRE" },
                rsw = new Node { Name = "RSW" },
                jfk = new Node { Name = "JFK" },
                waw = new Node { Name = "WAW" },
                bfi = new Node { Name = "BFI" },
                dub = new Node { Name = "DUB" },
                oka = new Node { Name = "OKA" },
                pek = new Node { Name = "PEK" },
                dme = new Node { Name = "DME" },
                hnd = new Node { Name = "HND" },
                ckg = new Node { Name = "CKG" },
                fnj = new Node { Name = "FNJ" },
                awp = new Node { Name = "AWP" },
                kef = new Node { Name = "KEF" },
                del = new Node { Name = "DEL" },
                evx = new Node { Name = "EVX" },
                pak = new Node { Name = "PAK" },
                cai = new Node { Name = "CAI" };

            Transition
                t1 = new Transition { Km = 1044.93, N1 = prg, N2 = lhr },
                t2 = new Transition { Km = 500.63, N1 = prg, N2 = bre, OneDirectional = true },
                t3 = new Transition { Km = 8184.96, N1 = prg, N2 = rsw },
                t4 = new Transition { Km = 827.53, N1 = bre, N2 = waw },
                t5 = new Transition { Miles = 1076.52, N1 = rsw, N2 = jfk },
                t6 = new Transition { Miles = 5293.15, N1 = rsw, N2 = waw },
                t7 = new Transition { Miles = 2414.55, N1 = jfk, N2 = bfi, OneDirectional = true },
                t8 = new Transition { Km = 8396.55, N1 = waw, N2 = bfi, OneDirectional = true },
                t9 = new Transition { Km = 1166.6, N1 = waw, N2 = dme },
                t10 = new Transition { Miles = 4790.84, N1 = bfi, N2 = hnd },
                t11 = new Transition { Km = 7502.07, N1 = dme, N2 = hnd },
                t12 = new Transition { Km = 12489.43, N1 = awp, N2 = dme, OneDirectional = true },
                t13 = new Transition { Km = 6237.99, N1 = hnd, N2 = awp },
                t14 = new Transition { Km = 2565.34, N1 = dme, N2 = evx },
                t15 = new Transition { Km = 1553.67, N1 = hnd, N2 = oka },
                t16 = new Transition { Km = 15043.41, N1 = awp, N2 = evx },
                t17 = new Transition { Km = 14966.36, N1 = kef, N2 = awp, OneDirectional = true },
                t18 = new Transition { Km = 6503.89, N1 = awp, N2 = ckg },
                t19 = new Transition { Km = 8493.96, N1 = awp, N2 = del },
                t20 = new Transition { Km = 5835.67, N1 = evx, N2 = pak },
                t21 = new Transition { Km = 6662.77, N1 = evx, N2 = del },
                t22 = new Transition { Km = 3127.3, N1 = pak, N2 = cai },
                t23 = new Transition { Km = 4403.07, N1 = cai, N2 = del, OneDirectional = true },
                t24 = new Transition { Km = 5300.37, N1 = kef, N2 = cai, OneDirectional = true },
                t25 = new Transition { Km = 7631.91, N1 = kef, N2 = del },
                t26 = new Transition { Km = 1499.44, N1 = kef, N2 = dub },
                t27 = new Transition { Km = 2032.71, N1 = ckg, N2 = fnj, OneDirectional = true },
                t28 = new Transition { Km = 783.06, N1 = fnj, N2 = pek, OneDirectional = true },
                t29 = new Transition { Km = 1464.84, N1 = pek, N2 = ckg, OneDirectional = true },
                t30 = new Transition { Km = 1851.41, N1 = oka, N2 = pek },
                t31 = new Transition { Km = 10981.67, N1 = jfk, N2 = pek },
                t32 = new Transition { Km = 8156.63, N1 = lhr, N2 = pek, OneDirectional = true },
                t33 = new Transition { Miles = 279.24, N1 = dub, N2 = lhr, OneDirectional = true };
            
            result.nodes = new HashSet<Node>()
            {
                prg, lhr, bre, rsw, jfk, waw, bfi, dub, oka, pek,
                dme, hnd, ckg, fnj, awp, kef, del, evx, pak, cai
            };

            result.startingNode = result.nodes
                .Where(n => n.Name.ToLower() == startingNodeName.ToLower())
                .FirstOrDefault();

            result.endingNode = result.nodes
                .Where(n => n.Name.ToLower() == endingNodeName.ToLower())
                .FirstOrDefault();

            return result;
        }
    }
}
