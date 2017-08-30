using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using GraphService;

namespace CommonLib
{
    public class NodeXMLTool : XMLTool
    {
        public override bool ValidateXML(XmlDocument nodeFromXMLFile)
        {
            bool isValid = true;

            if (nodeFromXMLFile.SelectNodes("./node/id").Count != 1)
            {
                isValid = false;
            }

            if (nodeFromXMLFile.SelectNodes("./node/label").Count != 1)
            {
                isValid = false;
            }

            if (nodeFromXMLFile.SelectNodes("./node/adjacentNodes").Count == 0)
            {
                isValid = false;
            }

            return isValid;

        }

        // Create a new node from the passed in XML document that was read from disk
        public override object CreateGraphNodeFromXML(XmlDocument nodeFromXMLFile)
        {
            GraphNode gn = new GraphNode();

            gn.InputFilename = System.IO.Path.GetFileName(nodeFromXMLFile.BaseURI);
            gn.AdjacentNodes = new List<GraphAdjacentNode>();

            foreach (XmlNode xmlNode in nodeFromXMLFile.DocumentElement)
            {
                if (xmlNode.Name == "id")
                {
                    gn.NodeID = int.Parse(xmlNode.InnerText);
                }
                else if (xmlNode.Name == "label")
                {
                    gn.Label = xmlNode.InnerText;
                }
                else if (xmlNode.Name == "adjacentNodes")
                {
                    foreach (XmlNode xmlAdjacentNodes in xmlNode.ChildNodes)
                    {
                        if (xmlAdjacentNodes.Name == "id")
                        {
                            gn.AdjacentNodes.Add(new GraphAdjacentNode { NodeID = gn.NodeID, AdjacentNodeID = int.Parse(xmlAdjacentNodes.InnerText) });
                        }
                    }
                }
            }

            return gn;
        }
    }
}
