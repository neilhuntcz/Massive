using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataAccess
{
    public class Node
    {
        public int NodeID { get; set; }
        public string Label { get; set; } = "No Label";
        // Used when resyncing, delete records with no matching file, add records with a filename
        // not already in the table
        public string InputFilename { get; set; } = "No file";
        public virtual ICollection<AdjacentNode> AdjacentNodes { get; set; }

        public Node()
        {
            this.AdjacentNodes = new HashSet<AdjacentNode>();
        }

        public static bool ValidateXML(XmlDocument nodeFromXMLFile)
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

            if (nodeFromXMLFile.SelectNodes("./node/adjacentNodes/id").Count == 0)
            {
                isValid = false;
            }

            return isValid;

        }

        // Create a new node from the passed in XML document that was read from disk
        public Node(XmlDocument nodeFromXMLFile) : this()
        {
            InputFilename = System.IO.Path.GetFileName(nodeFromXMLFile.BaseURI);

            foreach (XmlNode xmlNode in nodeFromXMLFile.DocumentElement)
            {
                if (xmlNode.Name == "id")
                {
                    NodeID = int.Parse(xmlNode.InnerText);
                }
                else if (xmlNode.Name == "label")
                {
                    Label = xmlNode.InnerText;
                }
                else if (xmlNode.Name == "adjacentNodes")
                {
                    foreach (XmlNode xmlAdjacentNodes in xmlNode.ChildNodes)
                    {
                        if (xmlAdjacentNodes.Name == "id")
                        {
                            AdjacentNodes.Add(new AdjacentNode { NodeID = int.Parse(xmlNode.InnerText), AdjacentNodeID = int.Parse(xmlAdjacentNodes.InnerText) });
                        }
                    }
                }
            }
        }
    }
}
