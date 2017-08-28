using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.IO;
using System.Xml;

namespace DataLoader
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (DataContext objContext = new DataContext())
            {
                foreach(string f in Directory.GetFiles("../../inputdata", "*.xml"))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(f);
                    Node nn = new Node { InputFilename = Path.GetFileName(f) };

                    foreach (XmlNode n in xml.DocumentElement)
                    {
                        if (n.Name == "id")
                        {
                            nn.NodeID = int.Parse(n.InnerText);
                        }
                        else if (n.Name == "label")
                        {
                            nn.Label = n.InnerText;
                        }
                        else if (n.Name == "adjacentNodes")
                        {
                            foreach (XmlNode a in n.ChildNodes)
                            {
                                if (a.Name == "id")
                                {
                                    nn.AdjacentNodes.Add(new AdjacentNode { NodeID = int.Parse(n.InnerText), AdjacentNodeID = int.Parse(a.InnerText) });
                                }

                                //Console.Write($"{a.Name} : {a.InnerText} ");
                            }
                        }
                    }

                    Console.WriteLine($"{nn.InputFilename} : {nn.NodeID} : {nn.Label}");
                    Console.WriteLine("");

                    objContext.Nodes.Add(nn);          
                }

                /*
                var a = objContext.Nodes.ToList();

                foreach (Node n in a)
                {
                    Console.WriteLine($"{n.NodeID} : {n.Label}");
                }
                */


                objContext.SaveChanges();
            }

            //Console.ReadLine();
        }

        public static int TestNunitInstalled()
        {
            return 1;
        }
    }
}
