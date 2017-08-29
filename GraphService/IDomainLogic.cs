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
    public class ShortestRoute
    {
        [DataMember]
        public List<int> ShortestRouteNodes { get; set; }
    }

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
