using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GraphService
{
    // Classes used when returning the calculated shortest route between nodes

    [DataContract]
    public class ShortestRoute
    {
        [DataMember]
        public List<int> ShortestRouteNodes { get; set; }
    }
}