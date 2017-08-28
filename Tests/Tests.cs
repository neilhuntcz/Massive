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
        [Test]
        public void TestTest()
        {
            Assert.AreEqual(1, Program.TestNunitInstalled());
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
    }
}
