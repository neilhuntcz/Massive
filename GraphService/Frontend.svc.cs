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
        public List<FrontendNode> GetAllNodes()
        {
            List<FrontendNode> nodes = new List<FrontendNode>();
            Random r = new Random(DateTime.Now.Millisecond);

            using (DataContext db = new DataContext())
            {
                int posx = 250;
                int posy = 200;
                int number = db.Nodes.ToList().Count();

                int columns = (int)Math.Sqrt(number);
                int rows = (int)Math.Ceiling(number / (float)columns);

                int curcol = 1;
                int currow = 1;

                foreach (Node entitynode in db.Nodes.ToList())
                {
                    if (r.Next() % 2 == 0)
                    {
                        posx = curcol * 250 + 33;
                        posy = currow * 200 + 20;
                    }
                    else
                    {
                        posx = (curcol * 250) - 33;
                        posy = (currow * 200) - 20;
                    }

                    List<FrontendAdjacentNode> adj = new List<FrontendAdjacentNode>();

                    foreach (AdjacentNode a in entitynode.AdjacentNodes.ToList())
                    {
                        // For display purposes we are not interested in nodes that have a 2 way
                        // relationship. Filter so only one relationship is returned.
                        if (entitynode.NodeID < a.AdjacentNodeID)
                        {
                            if (db.AdjacentNodes.Where(n => n.NodeID == a.AdjacentNodeID && n.AdjacentNodeID == entitynode.NodeID).Count() >= 1)
                            {
                                continue;
                            }
                        }

                        adj.Add(new FrontendAdjacentNode { NodeID = a.NodeID, AdjacentNodeID = a.AdjacentNodeID });
                    }

                    FrontendNode gnode = new FrontendNode
                    {
                        NodeID = entitynode.NodeID,
                        Label = entitynode.Label,
                        PosX = posx,
                        PosY = posy,
                        AdjacentNodes = adj
                    };

                    nodes.Add(gnode);

                    if (curcol > columns)
                    {
                        posx = 250;
                        currow++;
                        curcol = 1;
                    }
                    else
                    {
                        curcol++;
                    }
                }
            }

            return nodes;
        }
    }
}
