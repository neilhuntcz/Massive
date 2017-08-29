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
    public class FrontendNode
    {
        [DataMember]
        public int NodeID { get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public int PosX{ get; set; }
        [DataMember]
        public int PosY { get; set; }
        [DataMember]
        public ICollection<FrontendAdjacentNode> AdjacentNodes { get; set; }
    }

    [DataContract]
    public class FrontendAdjacentNode
    {
        [DataMember]
        public int NodeID { get; set; }
        [DataMember]
        public int AdjacentNodeID { get; set; }
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFrontend" in both code and config file together.
    [ServiceContract]
    public interface IFrontend
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                    UriTemplate = "/GetAllNodes",
                    BodyStyle = WebMessageBodyStyle.Bare,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        List<FrontendNode> GetAllNodes();
    }
}
