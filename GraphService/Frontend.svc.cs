using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataAccess;

namespace GraphService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Frontend" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Frontend.svc or Frontend.svc.cs at the Solution Explorer and start debugging.
    public class Frontend : IFrontend
    {
        public List<GraphNode> GetAllNodes()
        {
            List<GraphNode> nodes = new List<GraphNode>();

            using (DataContext db = new DataContext())
            {
                foreach (Node entitynode in db.Nodes.ToList())
                {
                    List<GraphAdjacentNode> adj = new List<GraphAdjacentNode>();

                    foreach (AdjacentNode a in entitynode.AdjacentNodes.ToList())
                    {
                        adj.Add(new GraphAdjacentNode { NodeID = a.NodeID, AdjacentNodeID = a.AdjacentNodeID });
                    }

                    GraphNode gnode = new GraphNode { NodeID = entitynode.NodeID, Label = entitynode.Label, AdjacentNodes = adj };

                    nodes.Add(gnode);
                }
            }

            return nodes;
        }
    }
}
