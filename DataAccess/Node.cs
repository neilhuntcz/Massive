using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Node
    {
        public Node()
        {
            this.AdjacentNodes = new HashSet<AdjacentNode>();
        }

        public int NodeID { get; set; } = -1;
        public string Label { get; set; } = "No Label";

        // Used when resyncing, delete records with no matching file, add records with a filename
        // not already in the table
        public string InputFilename { get; set; } = "No file"; 

        public virtual ICollection<AdjacentNode> AdjacentNodes { get; set; }
    }
}
