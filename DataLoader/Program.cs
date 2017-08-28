using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.IO;
using System.Xml;
using System.ServiceModel;
using GraphService;
using System.Net;
using Newtonsoft.Json;

namespace DataLoader
{
    public class Program
    {
        static void Main(string[] args)
        {

            WebClient proxy = new WebClient();
            string serviceURL = "http://localhost:56481/DataManager.svc/GetNode/10";
            string data = proxy.DownloadString(serviceURL);

            var n = JsonConvert.DeserializeObject<Node>(data);
      
            string x = "";


            /*
            using (DataContext objContext = new DataContext())
            {
                XmlDocument xml = new XmlDocument();

                string[] xmlfiles = Directory.GetFiles(".\\inputdata", "*.xml");


                // move to sync deletions
                var a = objContext.Nodes.ToList();

                foreach (Node n in a)
                {
                    if (xmlfiles.Where(d => Path.GetFileName(d) == n.InputFilename).Count() == 0)
                    {
                        Console.WriteLine($"{n.InputFilename} no longer exists, removing from DB");
                        objContext.Nodes.Remove(n);
                    }
                }

                foreach (string file in xmlfiles)
                {
                    try
                    {
                        xml = XMLTools.LoadXML(file);
                    }
                    catch
                    {
                        Console.WriteLine($"Unexpected error loading XML file {file}");
                        continue;
                    }

                    if (xml != null)
                    { 
                        Node nn = new Node(xml);

                        // refactor to one call to single or default
                        if (objContext.Nodes.Where(d => d.InputFilename == nn.InputFilename).Count() == 0)
                        {                        
                            objContext.Nodes.Add(nn);
                            Console.WriteLine($"Added {nn.InputFilename} : {nn.NodeID} : {nn.Label}");
                        }
                        else
                        {
                            Node un = objContext.Nodes.Single(d => d.InputFilename == nn.InputFilename);
                            objContext.AdjacentNodes.RemoveRange(un.AdjacentNodes);
                            un.AdjacentNodes = nn.AdjacentNodes;
                            un.Label = nn.Label;
                            un.NodeID = nn.NodeID;

                            Console.WriteLine($"Updated {un.InputFilename} : {un.NodeID} : {un.Label}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{file} is not a path to XML file or a valid XML document");
                    }
                }

                objContext.SaveChanges();
            }
            */
        }

        public static int TestNunitInstalled()
        {
            return 1;
        }
    }
}
