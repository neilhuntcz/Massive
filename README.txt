App configuration.

Database

I have used the Entity Framework code first approach to define the database. The database schema will be created in code if it does not exist, by default it will try to attach the database to the default SQLExpress instance if it is running otherwise it will use localdb and store the physical file in c:\users\{activeuser}. The connection string can be found in GraphService\Web.config

XML File Import

The import tool DataLoader will by default look for a directory named "inputdata" in the same directory as the executable. This behavior may be changed by modifying the InputXMLLocation setting in DataLoader\App.config

WCF Service Location

Both the data loader app and the web UI use a WCF REST service for interaction with the database and to perform various calculations. The location of this service can be specified using the ServiceURI setting in DataLoader\App.config and WebUI\Web.config

WebUI

To calculate the shortest route between 2 nodes simply click on the starting node, the ending node and then click on "Calculate Shortest Route". Clicking on more than 2 nodes in sequence will restart the node selection sequence to allow for corrections.