using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.ServiceModel;
using GraphService;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Configuration;
using CommonLib;

namespace DataLoader
{
    public class Program
    {
        static void Main(string[] args)
        {                       
            XmlDocument xml = new XmlDocument();
            string[] xmlfiles = Directory.GetFiles(ConfigurationManager.AppSettings["InputXMLLocation"], "*.xml");
            string serviceuri = ConfigurationManager.AppSettings["ServiceURI"];
            IServiceCall service = new NodeServiceCall();
            XMLTool xmltool = new NodeXMLTool();

            List<GraphNode> a = (List<GraphNode>)service.GetAll(serviceuri);

            // First check for any removed XML files. If a file exists in the database but not the
            // filesytem, delete it from the database.
            foreach (GraphNode n in a)
            {
                if (xmlfiles.Where(d => Path.GetFileName(d) == n.InputFilename).Count() == 0)
                {
                    Console.WriteLine($"{n.InputFilename} no longer exists, removing from DB");
                    service.Delete(n.NodeID, serviceuri);
                }
            }
               
            // Now loop through the files to check for new/updated nodes
            foreach (string file in xmlfiles)
            {
                try
                {
                    xml = xmltool.LoadXML(file);
                }
                catch
                {
                    Console.WriteLine($"Unexpected error loading XML file {file}");
                    continue;
                }

                if (xml != null && xmltool.ValidateXML(xml))
                { 
                    GraphNode nn = (GraphNode)xmltool.CreateGraphNodeFromXML(xml);

                    if ((GraphNode)service.GetOne(nn.NodeID, serviceuri) == null)
                    {
                        service.Add(nn, serviceuri);
                        Console.WriteLine($"Added {nn.InputFilename} : {nn.NodeID} : {nn.Label}");
                    }
                    else
                    {
                        service.Update(nn, serviceuri);
                        Console.WriteLine($"Updated {nn.InputFilename} : {nn.NodeID} : {nn.Label}");
                    }
                }
                else
                {
                    Console.WriteLine($"{file} is not a path to XML file or a valid XML document");
                }
            }

            Console.WriteLine($"Press enter to close");
            Console.ReadLine();
        }
    }
}
