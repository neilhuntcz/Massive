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
        public DataContext(): base("DataContext")   
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }
        
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            // Disable identity on the NodeID as it will be provided in the XML file.
            builder.Entity<Node>().Property(e => e.NodeID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            // Create a composite primary key for the AdjacentNode table
            builder.Entity<AdjacentNode>().HasKey(table => new {
                table.NodeID,
                table.AdjacentNodeID
            });
        }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<AdjacentNode> AdjacentNodes { get; set; }
    }
}
