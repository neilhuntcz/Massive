using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GraphService
{
    [DataContract]
    public class GraphNode
    {
        [DataMember]
        public int NodeID{ get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public string InputFilename { get; set; }
        [DataMember]
        public ICollection<GraphAdjacentNode> AdjacentNodes { get; set; }
    }

    [DataContract]
    public class GraphAdjacentNode
    {
        [DataMember]
        public int NodeID { get; set; }
        [DataMember]
        public int AdjacentNodeID { get; set; }
    }

    [ServiceContract]
    public interface IDataManager
    {
        [OperationContract]
        [WebInvoke( Method = "GET", 
                    UriTemplate = "/GetNode/{NodeID}", 
                    BodyStyle = WebMessageBodyStyle.Bare, 
                    RequestFormat = WebMessageFormat.Json, 
                    ResponseFormat = WebMessageFormat.Json)]
        GraphNode GetNode(string NodeID);

        [OperationContract]
        [WebInvoke( Method = "GET", 
                    UriTemplate = "/GetAllNodes", 
                    BodyStyle = WebMessageBodyStyle.Bare, 
                    RequestFormat = WebMessageFormat.Json, 
                    ResponseFormat = WebMessageFormat.Json)]
        List<GraphNode> GetAllNodes();

        [OperationContract]
        [WebInvoke( Method = "DELETE",
                    UriTemplate = "/DeleteNode/{NodeID}",
                    BodyStyle = WebMessageBodyStyle.Bare, 
                    RequestFormat = WebMessageFormat.Json, 
                    ResponseFormat = WebMessageFormat.Json)]
        void DeleteNode(string NodeID);

        [OperationContract]
        [WebInvoke(Method = "POST",
                    UriTemplate = "AddNode",
                    BodyStyle = WebMessageBodyStyle.Bare,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        void AddNode(GraphNode node);
    }
}
