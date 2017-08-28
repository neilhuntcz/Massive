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
                foreach (string f in Directory.GetFiles("../../inputdata", "*.xml"))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(f);

                    Node nn = new Node(xml);
                    objContext.Nodes.Add(nn);
                }

                objContext.SaveChanges();

                var a = objContext.Nodes.ToList();

                foreach (Node n in a)
                {
                    Console.WriteLine($"{n.InputFilename} : {n.NodeID} : {n.Label}");
                }
            }
        }

        public static int TestNunitInstalled()
        {
            return 1;
        }
    }
}
