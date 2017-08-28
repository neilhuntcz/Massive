using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DataLoader;

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
    }
}
