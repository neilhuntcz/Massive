using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace DataLoader
{
    // This will be moved when refactored
    public static class XMLTools
    {
        public static XmlDocument LoadXML(string input)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                // try to load the input string directly
                xml.LoadXml(input);
            }
            catch (XmlException ex) { }

            if (!xml.HasChildNodes)
            {
                try
                {
                    // not loaded yet, try to load from file
                    xml.Load(input);
                }
                catch (FileNotFoundException ex) { }
            }

            // return the document if loaded correctly
            if (xml.HasChildNodes)
            {
                return xml;
            }
            else
            {
                return null;
            }
        }
    }
}
