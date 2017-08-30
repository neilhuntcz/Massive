App configuration.

Database

I have used the Entity Framework code first approach to define the database. The database schema will be created in code if it does not exist, by default it will try to attach the database to the default SQLExpress instance if it is running otherwise it will use localdb and store the physical file in c:\users\{activeuser}. The connection string can be found in GraphService\Web.config. Alternatively the database can be created and hosted elsewhere by updating the connection string in the Web.config file of the servive and using the scripts below to create the database.

XML File Import

The import tool DataLoader will by default look for a directory named "inputdata" in the same directory as the executable. This behavior may be changed by modifying the InputXMLLocation setting in DataLoader\App.config

WCF Service Location

Both the data loader app and the web UI use a WCF REST service for interaction with the database and to perform various calculations. The location of this service can be specified using the ServiceURI setting in DataLoader\App.config and WebUI\Web.config

WebUI

To calculate the shortest route between 2 nodes simply click on the starting node, the ending node and then click on "Calculate Shortest Route". Clicking on more than 2 nodes in sequence will restart the node selection to allow for corrections.

/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.1601)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2016
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [master]
GO

/****** Object:  Database [MassiveGraphTest]    Script Date: 30/08/2017 11:47:28 ******/
CREATE DATABASE [MassiveGraphTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MassiveGraphTest', FILENAME = N'C:\Users\Neil\MassiveGraphTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MassiveGraphTest_log', FILENAME = N'C:\Users\Neil\MassiveGraphTest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [MassiveGraphTest] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MassiveGraphTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [MassiveGraphTest] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET ARITHABORT OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [MassiveGraphTest] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [MassiveGraphTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [MassiveGraphTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET  ENABLE_BROKER 
GO

ALTER DATABASE [MassiveGraphTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [MassiveGraphTest] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [MassiveGraphTest] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [MassiveGraphTest] SET  MULTI_USER 
GO

ALTER DATABASE [MassiveGraphTest] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [MassiveGraphTest] SET DB_CHAINING OFF 
GO

ALTER DATABASE [MassiveGraphTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [MassiveGraphTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [MassiveGraphTest] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [MassiveGraphTest] SET QUERY_STORE = OFF
GO

USE [MassiveGraphTest]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [MassiveGraphTest] SET  READ_WRITE 
GO


/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.1601)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2016
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [MassiveGraphTest]
GO

/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 30/08/2017 11:50:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.1601)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2016
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [MassiveGraphTest]
GO

/****** Object:  Table [dbo].[AdjacentNodes]    Script Date: 30/08/2017 11:50:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AdjacentNodes](
	[NodeID] [int] NOT NULL,
	[AdjacentNodeID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AdjacentNodes] PRIMARY KEY CLUSTERED 
(
	[NodeID] ASC,
	[AdjacentNodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdjacentNodes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AdjacentNodes_dbo.Nodes_NodeID] FOREIGN KEY([NodeID])
REFERENCES [dbo].[Nodes] ([NodeID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AdjacentNodes] CHECK CONSTRAINT [FK_dbo.AdjacentNodes_dbo.Nodes_NodeID]
GO


/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.1601)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2016
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [MassiveGraphTest]
GO

/****** Object:  Table [dbo].[Nodes]    Script Date: 30/08/2017 11:50:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Nodes](
	[NodeID] [int] NOT NULL,
	[Label] [nvarchar](max) NULL,
	[InputFilename] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Nodes] PRIMARY KEY CLUSTERED 
(
	[NodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


