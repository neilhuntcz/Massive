using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DataLoader;
using DataAccess;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        public int[] getNodes()
        {
            return new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        public List<Tuple<int,int>> getAdjacentNodes()
        {
             var list = new List<Tuple<int, int>>
             {
                Tuple.Create(1,2), Tuple.Create(1,3), Tuple.Create(2,1), Tuple.Create(2,5),
                Tuple.Create(2,7), Tuple.Create(2,9), Tuple.Create(2,10), Tuple.Create(3,5),
                Tuple.Create(5,2), Tuple.Create(5,5), Tuple.Create(5,7), Tuple.Create(5,8),
                Tuple.Create(6,7), Tuple.Create(6,10), Tuple.Create(7,6), Tuple.Create(8,9),
                Tuple.Create(9,10), Tuple.Create(10,9)
             };

            return list;
        }

        [TestCase (10, 5, "10, 2, 5", TestName = "Test shortest path algorithm")]
        public void TestShortestPath(int start, int end, string expected)
        {
            var graph = new Graph(getNodes(), getAdjacentNodes());
            var shortestPath = graph.ShortestPathFunction(graph, start, end);
            Assert.That(expected, Is.EqualTo(string.Join(", ", shortestPath)));
        }

        [Test]
        public void TestTest()
        {
            Assert.AreEqual(1, Program.TestNunitInstalled());
        }

        [TestCase(@"<?xml version='1.0' encoding='utf-8'?>
                            <node>
	                            <id>2</id>
	                            <label>Intel</label>
	                            <adjacentNodes>
		                            <id>1</id>
		                            <id>10</id>
	                            </adjacentNodes>
                            </node>", TestName = "Test loading XML into document object")]
        [TestCase(@"./inputdata/apple.xml", TestName = "Test loading XML file into document object")]
        public void TestLoadXML(string input)
        {
            var xml = new System.Xml.XmlDocument();
            xml = XMLTools.LoadXML(input);
            Assert.That(new System.Xml.XmlDocument().GetType(), Is.EqualTo(xml.GetType()));
        }

        [TestCase(@"bad data", TestName = "Test loading invalid XML into document object")]
        public void TestLoadBadXML(string input)
        {
            var xml = new System.Xml.XmlDocument();
            xml = XMLTools.LoadXML(input);

            Assert.That(null, Is.EqualTo(xml));
        }

        [TestCase (@"<?xml version='1.0' encoding='utf-8'?>
                            <node>
	                            <id>2</id>
	                            <label>Intel</label>
	                            <adjacentNodes>
		                            <id>1</id>
		                            <id>10</id>
	                            </adjacentNodes>
                            </node>", true, TestName = "Test valid XML")]
        [TestCase(@"<?xml version='1.0' encoding='utf-8'?>
                            <node>
	                            <label>Intel</label>
	                            <adjacentNodes>
		                            <id>1</id>
		                            <id>10</id>
	                            </adjacentNodes>
                            </node>", false, TestName = "Test invalid XML missing node id")]
        [TestCase(@"<?xml version='1.0' encoding='utf-8'?>
                            <node>
	                            <id>2</id>
	                            <adjacentNodes>
		                            <id>1</id>
		                            <id>10</id>
	                            </adjacentNodes>
                            </node>", false, TestName = "Test invalid XML missing label")]

        [TestCase(@"<?xml version='1.0' encoding='utf-8'?>
                            <node>
	                            <id>2</id>
                                <label>Intel</label>
	                            <adjacentNodes>
	                            </adjacentNodes>
                            </node>", false, TestName = "Test invalid XML missing adjacent nodes")]

        public void TestXMLStructure(string input, bool expected)
        {
            var xml = new System.Xml.XmlDocument();
            xml.LoadXml(input);
            bool IsValid = Node.ValidateXML(xml);
            Assert.That(expected, Is.EqualTo(IsValid));
        }

        [TestCase(@"<?xml version='1.0' encoding='utf-8'?>
                            <node>
	                            <id>2</id>
	                            <label>Intel</label>
	                            <adjacentNodes>
		                            <id>1</id>
		                            <id>10</id>
	                            </adjacentNodes>
                            </node>", true, TestName = "Test Load XML into Node object")]
        public void TestCreateNode(string input, bool expected)
        {
            var xml = new System.Xml.XmlDocument();
            xml.LoadXml(input);
            Node NewNode = new Node(xml);

            Assert.That(2, Is.EqualTo(NewNode.NodeID));
            Assert.That("Intel", Is.EqualTo(NewNode.Label));
            Assert.That(1, Is.EqualTo(NewNode.AdjacentNodes.Where(a => a.AdjacentNodeID == 1).Count()));
            Assert.That(1, Is.EqualTo(NewNode.AdjacentNodes.Where(a => a.AdjacentNodeID == 10).Count()));
        }
    }
}
