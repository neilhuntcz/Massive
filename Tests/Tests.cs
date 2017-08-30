using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net;
using DataAccess;
using GraphService;
using CommonLib;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        const string ServiceURI = "http://localhost:56481";
        const string InputXMLLocation = "C:\\Work\\Massive\\DataLoader\\bin\\Debug\\inputdata";

        [TestCase(TestName = "Test DataManager/GetAdd - Update - Delete")]
        public void TextDataManagerAddUpdateDelete()
        {
            IServiceCall service = new NodeServiceCall();

            GraphNode newnode = new GraphNode();
            newnode.Label = "TESTING";
            newnode.NodeID = 100;
            newnode.AdjacentNodes = new List<GraphAdjacentNode>();
            var result = service.Add(newnode, ServiceURI);
            Assert.That(HttpStatusCode.OK, Is.EqualTo(result.StatusCode));

            result = service.Update(newnode, ServiceURI);
            Assert.That(HttpStatusCode.OK, Is.EqualTo(result.StatusCode));

            result = service.Delete(100, ServiceURI);
            Assert.That(HttpStatusCode.OK, Is.EqualTo(result.StatusCode));
        }

        [TestCase(10, TestName = "Test DataManager/GetAll")]
        public void TextDataManagerGetOne(int expected)
        {
            IServiceCall service = new NodeServiceCall();
            List<GraphNode> g = (List<GraphNode>)service.GetAll(ServiceURI);
            Assert.That(expected, Is.EqualTo(g.Count));
        }

        [TestCase(1, 1, TestName = "Test DataManager/GetOne")]
        public void TextDataManagerGetOne(int NodeID, int expected)
        {
            IServiceCall service = new NodeServiceCall();
            GraphNode g = (GraphNode)service.GetOne(NodeID, ServiceURI);
            Assert.That(expected, Is.EqualTo(g.NodeID));
        }

        [TestCase(-1, null, TestName = "Test DataManager/GetOne Not Found")]
        public void TextDataManagerGetOneException(int NodeID, int? expected)
        {
            IServiceCall service = new NodeServiceCall();
            GraphNode g = (GraphNode)service.GetOne(NodeID, ServiceURI);
            Assert.That(expected, Is.EqualTo(g));
        }

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
            var shortestPath = graph.CalculateShortestPath(start, end);
            Assert.That(expected, Is.EqualTo(string.Join(", ", shortestPath)));
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
        [TestCase(InputXMLLocation + "\\apple.xml", TestName = "Test loading XML file into document object")]
        public void TestLoadXML(string input)
        {

            var xml = new System.Xml.XmlDocument();
            xml = new NodeXMLTool().LoadXML(input);
            Assert.That(new System.Xml.XmlDocument().GetType(), Is.EqualTo(xml.GetType()));
        }

        [TestCase(@"bad data", TestName = "Test loading invalid XML into document object")]
        public void TestLoadBadXML(string input)
        {
            var xml = new System.Xml.XmlDocument();
            xml = new NodeXMLTool().LoadXML(input);

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
	                            <adjacentNodes />
                            </node>", true, TestName = "Test valid XML with no adjacent nodes")]

        public void TestXMLStructure(string input, bool expected)
        {
            var xml = new System.Xml.XmlDocument();
            xml.LoadXml(input);
            bool IsValid = new NodeXMLTool().ValidateXML(xml);
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
            GraphNode NewNode = (GraphNode)new NodeXMLTool().CreateGraphNodeFromXML(xml);

            Assert.That(2, Is.EqualTo(NewNode.NodeID));
            Assert.That("Intel", Is.EqualTo(NewNode.Label));
            Assert.That(1, Is.EqualTo(NewNode.AdjacentNodes.Where(a => a.AdjacentNodeID == 1).Count()));
            Assert.That(1, Is.EqualTo(NewNode.AdjacentNodes.Where(a => a.AdjacentNodeID == 10).Count()));
        }
    }
}
