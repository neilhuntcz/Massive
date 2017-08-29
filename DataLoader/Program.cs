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
using System.Runtime.Serialization.Json;
using System.Net.Http;

namespace DataLoader
{
    public class Program
    {
        public static void GetOne()
        {
            HttpClient proxy = new HttpClient();
            byte[] data = proxy.GetByteArrayAsync(string.Format("http://localhost:56481/DataManager.svc/GetNode/{0}", "9")).Result;
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(GraphNode));
            GraphNode node = obj.ReadObject(stream) as GraphNode;

            Console.WriteLine($"{node.NodeID} : {node.Label} : {node.InputFilename}");

            foreach (GraphAdjacentNode gnode in node.AdjacentNodes)
            {
                Console.WriteLine($"{gnode.NodeID} : {gnode.AdjacentNodeID}");
            }
        }

        public static void GetAll()
        {
            HttpClient proxy = new HttpClient();
            byte[] data = proxy.GetByteArrayAsync("http://localhost:56481/DataManager.svc/GetAllNodes").Result;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<GraphNode>));
            MemoryStream stream = new MemoryStream(data);
            var obj = (List<GraphNode>)ser.ReadObject(stream);

            foreach (GraphNode node in obj)
            {
                Console.WriteLine($"{node.NodeID} : {node.Label} : {node.InputFilename}");

                foreach (GraphAdjacentNode gnode in node.AdjacentNodes)
                {
                    Console.WriteLine($"{gnode.NodeID} : {gnode.AdjacentNodeID}");
                }
            }
        }

        public static void Delete()
        {
            HttpClient httpClient = new HttpClient();

            // add values to data for post
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("", ""));
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            // Post data
            var result = httpClient.DeleteAsync("http://localhost:56481/DataManager.svc/DeleteNode/1").Result;

            // Access content as stream which you can read into some string
            Console.WriteLine(result.Content);

            // Access the result status code
            Console.WriteLine(result.StatusCode);
        }

        public static void Add()
        {
            HttpClient httpClient = new HttpClient();
            GraphNode p = new GraphNode { NodeID = 11, Label = "TEST" };
            p.AdjacentNodes = new List<GraphAdjacentNode>();

            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(GraphNode));

            MemoryStream ms = new MemoryStream();
            obj.WriteObject(ms, p);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);

            string postBody = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            var result = httpClient.PostAsync("http://localhost:56481/DataManager.svc/AddNode", new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
        }

        private static async Task DisplayTextResult(HttpResponseMessage response)
        {
            string responJsonText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responJsonText);
        }

        static void Main(string[] args)
        {
            //GetOne();
            //Console.WriteLine();
            //GetAll();

            //Add();
            //Delete();
                       
            using (DataContext objContext = new DataContext())
            {
                var nodes = objContext.Nodes.Where(n => n.AdjacentNodes.Count > 0).Select(n => n.NodeID).ToArray();
                var adjacentnodes = new List<Tuple<int, int>>();
                   
                foreach(var adj in objContext.AdjacentNodes)
                {
                    adjacentnodes.Add(Tuple.Create(adj.NodeID, adj.AdjacentNodeID));
                }

                var graph = new Graph(nodes, adjacentnodes);
                var StartNode = 10;
                var EndNode = 5
                    ;
                var shortestPath = graph.ShortestPathFunction(graph, StartNode, EndNode);

                Console.WriteLine("shortest path to {0,2}: {1}", EndNode, string.Join(", ", shortestPath));

/*
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
                */
            }
            



            Console.ReadLine();
        }

        public static int TestNunitInstalled()
        {
            return 1;
        }
    }
}
