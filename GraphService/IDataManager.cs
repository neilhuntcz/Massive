using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GraphService
{
    // REST methods used when communicating with the data loader console app 

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

        [OperationContract]
        [WebInvoke(Method = "PUT",
                    UriTemplate = "UpdateNode",
                    BodyStyle = WebMessageBodyStyle.Bare,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        void UpdateNode(GraphNode node);
    }
}
