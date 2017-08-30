using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GraphService
{
    // REST methods used when communicating with the web UI 

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
