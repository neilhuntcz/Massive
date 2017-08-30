using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GraphService
{
    // Classes used when cominicating with the data loader console app

    [DataContract]
    public class GraphNode
    {
        [DataMember]
        public int NodeID { get; set; }
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
}