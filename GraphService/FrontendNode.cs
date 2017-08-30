using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GraphService
{
    // Classes used when communicating with the web UI

    [DataContract]
    public class FrontendNode
    {
        [DataMember]
        public int NodeID { get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public int PosX { get; set; }
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
}