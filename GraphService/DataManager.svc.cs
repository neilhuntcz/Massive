using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataAccess;
using Newtonsoft.Json;

namespace GraphService
{
    public class DataManager : IDataManager
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

        public GraphNode GetNode(string NodeID)
        {
            using (DataContext db = new DataContext())
            {
                int nid = Convert.ToInt32(NodeID);
                var entitynode = db.Nodes.Single(n => n.NodeID == nid);
                List<GraphAdjacentNode> adj = new List<GraphAdjacentNode>();

                foreach(AdjacentNode a in entitynode.AdjacentNodes)
                {
                    adj.Add(new GraphAdjacentNode { NodeID = a.NodeID, AdjacentNodeID = a.AdjacentNodeID });
                }

                GraphNode gnode = new GraphNode { NodeID = entitynode.NodeID, Label = entitynode.Label, AdjacentNodes = adj };

                //return JsonConvert.SerializeObject(node);
                return gnode;
            }
        }
    }
}
