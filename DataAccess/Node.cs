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
        public Node()
        {
            this.AdjacentNodes = new HashSet<AdjacentNode>();
        }

        /*
         <?xml version="1.0" encoding="utf-8"?>
        <node>
	        <id>2</id>
	        <label>Intel</label>
	        <adjacentNodes>
		        <id>1</id>
		        <id>10</id>
		        <id>5</id>
		        <id>9</id>
		        <id>7</id>
	        </adjacentNodes>
        </node> */

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

        public int NodeID { get; set; }
        public string Label { get; set; } = "No Label";

        // Used when resyncing, delete records with no matching file, add records with a filename
        // not already in the table
        public string InputFilename { get; set; } = "No file"; 

        public virtual ICollection<AdjacentNode> AdjacentNodes { get; set; }
    }
}
