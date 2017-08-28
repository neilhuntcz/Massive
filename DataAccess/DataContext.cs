using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        // For simplicity the database will be automatically created if it does not exist.
        // The location will be on the {localdb} instance if available or a mdf in c:\users\{currentuser}
        public DataContext(): base("MassiveGraphTest")   
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
        }

        // Create a composite primary key for the AdjacentNode table
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<AdjacentNode>().HasKey(table => new {
                table.NodeID,
                table.AdjacentNodeID
            });
        }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<AdjacentNode> AdjacentNodes { get; set; }
    }
}
