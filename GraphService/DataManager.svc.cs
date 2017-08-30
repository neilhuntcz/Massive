using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataAccess;
using System.ServiceModel.Web;
using System.Net;

namespace GraphService
{
    public class DataManager : IDataManager
    {
        public void UpdateNode(GraphNode node)
        {
            using (DataContext db = new DataContext())
            {
                Node un = db.Nodes.Single(d => d.NodeID == node.NodeID);
                db.AdjacentNodes.RemoveRange(un.AdjacentNodes);

                foreach (GraphAdjacentNode a in node.AdjacentNodes)
                {
                    un.AdjacentNodes.Add(new AdjacentNode { NodeID = a.NodeID, AdjacentNodeID = a.AdjacentNodeID });
                }

                un.Label = node.Label;
                un.InputFilename = node.InputFilename;
                un.NodeID = node.NodeID;

                db.SaveChanges();
            }
        }

        public void AddNode(GraphNode node)
        {
            using (DataContext db = new DataContext())
            {
                var entitynode = new Node { NodeID = node.NodeID, Label = node.Label, InputFilename = node.InputFilename };

                foreach (GraphAdjacentNode a in node.AdjacentNodes)
                {
                    entitynode.AdjacentNodes.Add(new AdjacentNode { NodeID = a.NodeID, AdjacentNodeID = a.AdjacentNodeID });
                }

                db.Nodes.Add(entitynode);
                db.SaveChanges();
            }
        }

        public void DeleteNode(string NodeID)
        {
            int nid = Convert.ToInt32(NodeID);

            using (DataContext db = new DataContext())
            {
                db.Nodes.Remove(db.Nodes.Single(n => n.NodeID == nid));
                db.SaveChanges();
            }
        }

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

                    GraphNode gnode = new GraphNode { NodeID = entitynode.NodeID, Label = entitynode.Label, InputFilename = entitynode.InputFilename, AdjacentNodes = adj };

                    nodes.Add(gnode);
                }
            }

            return nodes;
        }

        // Return a single node based on the nodeID specified. Will fail if an invalid or nonexistant
        // node id is supplied
        public GraphNode GetNode(string NodeID)
        {
            using (DataContext db = new DataContext())
            {
                int nid = 0;

                try
                {
                    nid = Convert.ToInt32(NodeID);
                }
                catch (InvalidCastException ex)
                {
                    throw new WebFaultException(HttpStatusCode.BadRequest);
                }

                var entitynode = db.Nodes.Single(n => n.NodeID == nid);

                if (entitynode == null)
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }

                List<GraphAdjacentNode> adj = new List<GraphAdjacentNode>();

                foreach(AdjacentNode a in entitynode.AdjacentNodes)
                {
                    adj.Add(new GraphAdjacentNode { NodeID = a.NodeID, AdjacentNodeID = a.AdjacentNodeID });
                }

                GraphNode gnode = new GraphNode { NodeID = entitynode.NodeID, Label = entitynode.Label, InputFilename = entitynode.InputFilename, AdjacentNodes = adj };

                return gnode;
            }
        }
    }
}
