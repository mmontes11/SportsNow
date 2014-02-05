/* 
 * SQL Server Script
 * 
 * This script can be directly executed to configure the test database from
 * PCs located at CECAFI Lab. The database and the corresponding users are 
 * already created in the sql server, so it will create the tables needed 
 * in the samples. 
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *      Configure within the CREATE DATABASE sql-sentence the path where 
 *      database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 
USE [master]
GO

/****** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'sportsnow')
DROP DATABASE [sportsnow]
GO

USE [master]
GO

/****** DataBase Creation ******/
								  
CREATE DATABASE [sportsnow] ON PRIMARY 
( NAME = 'sportsnow', FILENAME = 'C:\SourceCode\Database\sportsnow.mdf') 
LOG ON 
( NAME = 'sportsnow_log', FILENAME = 'C:\SourceCode\Database\sportsnow_log.ldf')
GO


/**** Delete User if already exists ******/

IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = 'user')
DROP LOGIN [user]
GO


/******   Create LoginUser    ******/
CREATE LOGIN [user] 
WITH   PASSWORD='password', 
	   DEFAULT_DATABASE=[sportsnow], 
	   DEFAULT_LANGUAGE=[Español], 
	   CHECK_EXPIRATION=OFF, 
	   CHECK_POLICY=OFF
GO


/******   Set user as database dbo  ******/
USE sportsnow
GO

SP_CHANGEDBOWNER 'user'
GO


/* 
 * Drop tables.                                                             
 * NOTE: before dropping a table (when re-executing the script), the tables 
 * having columns acting as foreign keys of the table to be dropped must be 
 * dropped first (otherwise, the corresponding checks on those tables could 
 * not be done).                                                            
 */

USE [sportsnow]
GO

/* ********** Drop Table UserProfile if already exists *********** */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[SportEvent]') AND type in ('U'))
DROP TABLE [SportEvent]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserGroup]') AND type in ('U'))
DROP TABLE [UserGroup]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Tag]') AND type in ('U'))
DROP TABLE [Tag]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfileUserGroup]') AND type in ('U'))
DROP TABLE [UserProfileUserGroup]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Recommendation]') AND type in ('U'))
DROP TABLE [Recommendation]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[RecommendationUserGroup]') AND type in ('U'))
DROP TABLE [RecommendationUserGroup]
GO


/* ******************* Category: Table Creation  ************** */

CREATE TABLE Category (
	id bigint IDENTITY(1,1) NOT NULL,
	name varchar(30) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (id),
)

/* ******************* SportEvent: Table Creation  ************** */

CREATE TABLE SportEvent (
	id bigint IDENTITY(1,1) NOT NULL,
	name varchar(30) NOT NULL,
	dateEvent date NOT NULL,
	place varchar(30) NOT NULL,
	review varchar(100) NOT NULL,
	category_id bigint,

	CONSTRAINT [PK_SportEvent] PRIMARY KEY (id),
	CONSTRAINT [FK_SportEvent_Category] FOREIGN KEY (category_id) REFERENCES Category (id)
)

/* ******************* UserProfile: Table Creation  ************** */

CREATE TABLE UserProfile (
	id bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,

	CONSTRAINT [PK_User] PRIMARY KEY (id),
	CONSTRAINT [UniqueKey_LoginName] UNIQUE (loginName)
)

/* ******************* UserGroup: Table Creation  ************** */

CREATE TABLE UserGroup (
	id bigint IDENTITY(1,1) NOT NULL,
	name varchar(20) NOT NULL,
	descrip varchar(100) NOT NULL,

	CONSTRAINT [PK_UserGroup] PRIMARY KEY (id),
)

/* ******************* Tag: Table Creation  ************** */
CREATE TABLE Tag (
	id bigint IDENTITY(1,1) NOT NULL,
	name varchar(20) NOT NULL,
	CONSTRAINT [PK_Tag] PRIMARY KEY (id),
	CONSTRAINT [UNIQUE_TagName] UNIQUE(name)
)

/* ******************* Comment: Table Creation  ************** */

CREATE TABLE Comment (
	id bigint IDENTITY(1,1) NOT NULL,
	value varchar(100) NOT NULL,
	dateComment date NOT NULL,
	eventId bigint NOT NULL,
	userId bigint NOT NULL,

	CONSTRAINT [PK_Comment] PRIMARY KEY (id),
	CONSTRAINT [FK_Comment_SportEvent] FOREIGN KEY (eventId) REFERENCES SportEvent (id),
	CONSTRAINT [FK_Comment_UserProfile] FOREIGN KEY (userId) REFERENCES UserProfile (id)
)

/* ******************* CommentTag: Table Creation  ************** */

CREATE TABLE CommentTag (
	tagId bigint NOT NULL,
	commentId bigint NOT NULL,

	CONSTRAINT [PK_CommentTag] PRIMARY KEY (tagId, commentId),
	CONSTRAINT [FK_CommentTag_Tag] FOREIGN KEY (tagId) REFERENCES Tag (id),
	CONSTRAINT [FK_CommentTag_Comment] FOREIGN KEY (commentId) REFERENCES Comment (id)
)


/* ******************* UserProfileUserGroup: Table Creation  ************** */

CREATE TABLE UserProfileUserGroup (
	userProfileId bigint NOT NULL,
	userGroupId bigint NOT NULL,
	CONSTRAINT [PK_UserProfileUserGroup] PRIMARY KEY (userProfileId, userGroupId),
	CONSTRAINT [FK_UserProfileUserGroup_UserProfile] FOREIGN KEY (userProfileId) REFERENCES UserProfile(id),
	CONSTRAINT [FK_UserProfileUserGroup_UserGroup] FOREIGN KEY (userGroupId) REFERENCES UserGroup(id)
)

/* ******************* Recommendation: Table Creation  ************** */

CREATE TABLE Recommendation (
	id bigint IDENTITY(1,1) NOT NULL,
	dateRecommendation date NOT NULL,
	why varchar(255),
	sportEventId bigint NOT NULL,
	userProfileId bigint NOT NULL,

	CONSTRAINT [PK_Recommendation] PRIMARY KEY (id),
	CONSTRAINT [FK_Recommendation_SportEvent] FOREIGN KEY (sportEventId) REFERENCES SportEvent (id),
	CONSTRAINT [FK_Recommendation_UserProfile] FOREIGN KEY (userProfileId) REFERENCES UserProfile (id)
)


/* ******************* RecommendationUserGroup: Table Creation  ************** */
CREATE TABLE RecommendationUserGroup (
	recommendationId bigint NOT NULL,
	userGroupId bigint NOT NULL,

	CONSTRAINT [PK_RecommendationUserGroup] PRIMARY KEY(recommendationId,userGroupId),
	CONSTRAINT [FK_RecommendationUserGroup_Recommendation] FOREIGN KEY(recommendationId) REFERENCES Recommendation(id),
	CONSTRAINT [FK_RecommendationUserGroup_UserGroup] FOREIGN KEY(userGroupId) REFERENCES UserGroup(id)
);

GO

USE [sportsnow]
GO


/* ******************* Indexes Creation  ************** */

CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByUserName]
ON [UserProfile] ([loginName] ASC)


CREATE NONCLUSTERED INDEX [IX_SportEventIndexByName]
ON [SportEvent] ([name] ASC)

GO


INSERT INTO Category VALUES('Matches');
INSERT INTO Category VALUES('Races');

INSERT INTO SportEvent VALUES('Barça-Madrid',CAST('01-01-2012' AS DATETIME),'CampNou','Spanish Derby',1);
INSERT INTO SportEvent VALUES('Catalunya Grand Prix',CAST('01-01-2012' AS DATETIME),'Circuit de Catalunya','Spanish Grand Prix',2);

