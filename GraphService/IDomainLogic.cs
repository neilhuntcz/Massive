using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GraphService
{
    // REST methods used when calculating the shortest route between 2 nodes

    [ServiceContract]
    public interface IDomainLogic
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                    UriTemplate = "/GetShortestRoute?StartNode={StartNode}&EndNode={EndNode}",
                    BodyStyle = WebMessageBodyStyle.Bare,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        ShortestRoute GetShortestRoute(string StartNode, string EndNode);
    }
}
