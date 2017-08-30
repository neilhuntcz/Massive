using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GraphService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DomainLogic" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DomainLogic.svc or DomainLogic.svc.cs at the Solution Explorer and start debugging.
    public class DomainLogic : IDomainLogic
    {
        public ShortestRoute GetShortestRoute(string StartNode, string EndNode)
        {
            ShortestRoute sr = new ShortestRoute();

            using (DataContext objContext = new DataContext())
            {
                var nodes = objContext.Nodes.Where(n => n.AdjacentNodes.Count > 0).Select(n => n.NodeID).ToArray();
                var adjacentnodes = new List<Tuple<int, int>>();

                foreach (var adj in objContext.AdjacentNodes)
                {
                    adjacentnodes.Add(Tuple.Create(adj.NodeID, adj.AdjacentNodeID));
                }

                var graph = new Graph(nodes, adjacentnodes);
                var shortestPath = graph.CalculateShortestPath(int.Parse(StartNode), int.Parse(EndNode));
                sr.ShortestRouteNodes = shortestPath;
            }

            return sr;
        }
    }
}
