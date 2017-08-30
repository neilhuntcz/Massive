using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphService;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections;

namespace CommonLib
{
    public class NodeServiceCall : IServiceCall
    {
        public object GetOne<T>(T NodeID, string serviceURI)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                byte[] data = httpClient.GetByteArrayAsync(string.Format("{0}/DataManager.svc/GetNode/{1}", serviceURI, NodeID)).Result;
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(GraphNode));
                return (object)obj.ReadObject(stream);
            }
            catch
            {
                return default(object);
            }
        }

        public IList GetAll(string serviceURI)
        {
            HttpClient httpClient = new HttpClient();
            byte[] data = httpClient.GetByteArrayAsync(string.Format("{0}/DataManager.svc/GetAllNodes", serviceURI)).Result;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<GraphNode>));
            MemoryStream stream = new MemoryStream(data);
            return (IList)ser.ReadObject(stream);
        }

        public void Delete<T>(T NodeID, string serviceURI)
        {
            HttpClient httpClient = new HttpClient();
            var result = httpClient.DeleteAsync(string.Format("{0}/DataManager.svc/DeleteNode/{1}", serviceURI, NodeID)).Result;

            // Access content as stream which you can read into some string
            Console.WriteLine(result.Content);

            // Access the result status code
            Console.WriteLine(result.StatusCode);
        }

        public void Add<T>(T graphnode, string serviceURI)
        {
            HttpClient httpClient = new HttpClient();
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(GraphNode));
            MemoryStream ms = new MemoryStream();
            obj.WriteObject(ms, graphnode);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string postBody = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            var result = httpClient.PostAsync(string.Format("{0}/DataManager.svc/AddNode", serviceURI), new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
        }

        public void Update<T>(T graphnode, string serviceURI)
        {
            HttpClient httpClient = new HttpClient();
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(GraphNode));
            MemoryStream ms = new MemoryStream();
            obj.WriteObject(ms, graphnode);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string postBody = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            var result = httpClient.PutAsync(string.Format("{0}/DataManager.svc/UpdateNode", serviceURI), new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
        }
    }
}
