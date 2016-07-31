
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/30/2016 23:13:54
-- Generated from EDMX file: D:\Co-op Project\OASystem\OA.Model\OA.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OA_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ActionInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentActionInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentActionInfo] DROP CONSTRAINT [FK_DepartmentActionInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentActionInfo_Department]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentActionInfo] DROP CONSTRAINT [FK_DepartmentActionInfo_Department];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoDepartment_Department]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoDepartment] DROP CONSTRAINT [FK_UserInfoDepartment_Department];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoDepartment_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoDepartment] DROP CONSTRAINT [FK_UserInfoDepartment_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_WF_InstanceWF_StepInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WF_StepInfo] DROP CONSTRAINT [FK_WF_InstanceWF_StepInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_WF_TempWF_Instance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WF_Instance] DROP CONSTRAINT [FK_WF_TempWF_Instance];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[book]', 'U') IS NOT NULL
    DROP TABLE [dbo].[book];
GO
IF OBJECT_ID(N'[dbo].[Department]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Department];
GO
IF OBJECT_ID(N'[dbo].[DepartmentActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentActionInfo];
GO
IF OBJECT_ID(N'[dbo].[KeyWordsRank]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KeyWordsRank];
GO
IF OBJECT_ID(N'[dbo].[R_UserInfo_ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfoActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfoActionInfo];
GO
IF OBJECT_ID(N'[dbo].[SearchDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SearchDetails];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfoDepartment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoDepartment];
GO
IF OBJECT_ID(N'[dbo].[UserInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoRoleInfo];
GO
IF OBJECT_ID(N'[dbo].[WF_Instance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Instance];
GO
IF OBJECT_ID(N'[dbo].[WF_StepInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_StepInfo];
GO
IF OBJECT_ID(N'[dbo].[WF_Temp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Temp];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ActionInfoes'
CREATE TABLE [dbo].[ActionInfoes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SubTime] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [ModifiedOn] nvarchar(max)  NOT NULL,
    [Remark] nvarchar(256)  NULL,
    [Url] nvarchar(512)  NOT NULL,
    [HttpMethod] nvarchar(32)  NOT NULL,
    [ActionMethodName] nvarchar(32)  NULL,
    [ControllerName] nvarchar(128)  NULL,
    [ActionInfoName] nvarchar(32)  NOT NULL,
    [Sort] nvarchar(max)  NULL,
    [ActionTypeEnum] smallint  NOT NULL,
    [MenuIcon] varchar(512)  NULL,
    [IconWidth] int  NOT NULL,
    [IconHeight] int  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DepName] nvarchar(32)  NOT NULL,
    [ParentId] int  NOT NULL,
    [TreePath] nvarchar(128)  NOT NULL,
    [Level] int  NOT NULL,
    [IsLeaf] bit  NOT NULL
);
GO

-- Creating table 'R_UserInfo_ActionInfo'
CREATE TABLE [dbo].[R_UserInfo_ActionInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserInfoID] int  NOT NULL,
    [ActionInfoID] int  NOT NULL,
    [IsPass] bit  NOT NULL
);
GO

-- Creating table 'RoleInfoes'
CREATE TABLE [dbo].[RoleInfoes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(32)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [Remark] nvarchar(256)  NULL,
    [ModifiedOn] datetime  NOT NULL,
    [Sort] nvarchar(max)  NULL
);
GO

-- Creating table 'UserInfoes'
CREATE TABLE [dbo].[UserInfoes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UName] nvarchar(32)  NULL,
    [UPwd] nvarchar(16)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [ModifiedOn] datetime  NOT NULL,
    [Remark] nvarchar(256)  NULL,
    [Sort] nvarchar(max)  NULL
);
GO

-- Creating table 'books'
CREATE TABLE [dbo].[books] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NULL,
    [Author] nvarchar(50)  NULL,
    [PublisherId] int  NULL,
    [PublishedDate] nvarchar(50)  NULL,
    [ISBN] nvarchar(50)  NULL,
    [WordsCount] int  NULL,
    [UnitPrice] decimal(19,4)  NULL,
    [ContentDescription] nvarchar(max)  NULL,
    [AuthorDescription] nvarchar(max)  NULL,
    [EditorComment] nvarchar(max)  NULL,
    [TOC] nvarchar(max)  NULL,
    [CategoryId] int  NULL,
    [Clicks] int  NULL
);
GO

-- Creating table 'KeyWordsRanks'
CREATE TABLE [dbo].[KeyWordsRanks] (
    [id] uniqueidentifier  NOT NULL,
    [KeyWords] nvarchar(50)  NULL,
    [SearchCount] int  NULL
);
GO

-- Creating table 'SearchDetails'
CREATE TABLE [dbo].[SearchDetails] (
    [id] uniqueidentifier  NOT NULL,
    [KeyWords] nvarchar(50)  NULL,
    [SearchDateTime] datetime  NULL
);
GO

-- Creating table 'WF_Instance'
CREATE TABLE [dbo].[WF_Instance] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InstanceName] nvarchar(max)  NULL,
    [SubTime] datetime  NOT NULL,
    [StartedBy] int  NOT NULL,
    [Level] smallint  NOT NULL,
    [SubForm] nvarchar(max)  NULL,
    [Status] smallint  NOT NULL,
    [Result] smallint  NOT NULL,
    [WF_TempID] int  NOT NULL,
    [ApplicationId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'WF_StepInfo'
CREATE TABLE [dbo].[WF_StepInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SetpName] nvarchar(32)  NOT NULL,
    [IsProcessed] bit  NOT NULL,
    [IsStartStep] bit  NOT NULL,
    [IsEndStep] bit  NOT NULL,
    [Title] nvarchar(32)  NULL,
    [Comment] nvarchar(max)  NULL,
    [StepResult] smallint  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ProcessTime] datetime  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [ProcessBy] int  NOT NULL,
    [ParentStepID] int  NOT NULL,
    [ChildStepID] int  NOT NULL,
    [WF_InstanceID] int  NOT NULL
);
GO

-- Creating table 'WF_Temp'
CREATE TABLE [dbo].[WF_Temp] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TempName] nvarchar(32)  NULL,
    [SubTime] datetime  NOT NULL,
    [ModfiedOn] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(256)  NULL,
    [TempDescription] nvarchar(max)  NULL,
    [TempForm] nvarchar(max)  NOT NULL,
    [TempStatus] smallint  NOT NULL,
    [SubBy] int  NOT NULL
);
GO

-- Creating table 'DepartmentActionInfo'
CREATE TABLE [dbo].[DepartmentActionInfo] (
    [ActionInfoes_ID] int  NOT NULL,
    [Departments_ID] int  NOT NULL
);
GO

-- Creating table 'RoleInfoActionInfo'
CREATE TABLE [dbo].[RoleInfoActionInfo] (
    [ActionInfoes_ID] int  NOT NULL,
    [RoleInfoes_ID] int  NOT NULL
);
GO

-- Creating table 'UserInfoDepartment'
CREATE TABLE [dbo].[UserInfoDepartment] (
    [Departments_ID] int  NOT NULL,
    [UserInfoes_ID] int  NOT NULL
);
GO

-- Creating table 'UserInfoRoleInfo'
CREATE TABLE [dbo].[UserInfoRoleInfo] (
    [RoleInfoes_ID] int  NOT NULL,
    [UserInfoes_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'ActionInfoes'
ALTER TABLE [dbo].[ActionInfoes]
ADD CONSTRAINT [PK_ActionInfoes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [PK_R_UserInfo_ActionInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'RoleInfoes'
ALTER TABLE [dbo].[RoleInfoes]
ADD CONSTRAINT [PK_RoleInfoes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserInfoes'
ALTER TABLE [dbo].[UserInfoes]
ADD CONSTRAINT [PK_UserInfoes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'books'
ALTER TABLE [dbo].[books]
ADD CONSTRAINT [PK_books]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'KeyWordsRanks'
ALTER TABLE [dbo].[KeyWordsRanks]
ADD CONSTRAINT [PK_KeyWordsRanks]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'SearchDetails'
ALTER TABLE [dbo].[SearchDetails]
ADD CONSTRAINT [PK_SearchDetails]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [ID] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [PK_WF_Instance]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WF_StepInfo'
ALTER TABLE [dbo].[WF_StepInfo]
ADD CONSTRAINT [PK_WF_StepInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WF_Temp'
ALTER TABLE [dbo].[WF_Temp]
ADD CONSTRAINT [PK_WF_Temp]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ActionInfoes_ID], [Departments_ID] in table 'DepartmentActionInfo'
ALTER TABLE [dbo].[DepartmentActionInfo]
ADD CONSTRAINT [PK_DepartmentActionInfo]
    PRIMARY KEY CLUSTERED ([ActionInfoes_ID], [Departments_ID] ASC);
GO

-- Creating primary key on [ActionInfoes_ID], [RoleInfoes_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [PK_RoleInfoActionInfo]
    PRIMARY KEY CLUSTERED ([ActionInfoes_ID], [RoleInfoes_ID] ASC);
GO

-- Creating primary key on [Departments_ID], [UserInfoes_ID] in table 'UserInfoDepartment'
ALTER TABLE [dbo].[UserInfoDepartment]
ADD CONSTRAINT [PK_UserInfoDepartment]
    PRIMARY KEY CLUSTERED ([Departments_ID], [UserInfoes_ID] ASC);
GO

-- Creating primary key on [RoleInfoes_ID], [UserInfoes_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [PK_UserInfoRoleInfo]
    PRIMARY KEY CLUSTERED ([RoleInfoes_ID], [UserInfoes_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ActionInfoID] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([ActionInfoID])
    REFERENCES [dbo].[ActionInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_ActionInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([ActionInfoID]);
GO

-- Creating foreign key on [UserInfoID] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_UserInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [ActionInfoes_ID] in table 'DepartmentActionInfo'
ALTER TABLE [dbo].[DepartmentActionInfo]
ADD CONSTRAINT [FK_DepartmentActionInfo_ActionInfo]
    FOREIGN KEY ([ActionInfoes_ID])
    REFERENCES [dbo].[ActionInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Departments_ID] in table 'DepartmentActionInfo'
ALTER TABLE [dbo].[DepartmentActionInfo]
ADD CONSTRAINT [FK_DepartmentActionInfo_Department]
    FOREIGN KEY ([Departments_ID])
    REFERENCES [dbo].[Departments]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentActionInfo_Department'
CREATE INDEX [IX_FK_DepartmentActionInfo_Department]
ON [dbo].[DepartmentActionInfo]
    ([Departments_ID]);
GO

-- Creating foreign key on [ActionInfoes_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo]
    FOREIGN KEY ([ActionInfoes_ID])
    REFERENCES [dbo].[ActionInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleInfoes_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo]
    FOREIGN KEY ([RoleInfoes_ID])
    REFERENCES [dbo].[RoleInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleInfoActionInfo_RoleInfo'
CREATE INDEX [IX_FK_RoleInfoActionInfo_RoleInfo]
ON [dbo].[RoleInfoActionInfo]
    ([RoleInfoes_ID]);
GO

-- Creating foreign key on [Departments_ID] in table 'UserInfoDepartment'
ALTER TABLE [dbo].[UserInfoDepartment]
ADD CONSTRAINT [FK_UserInfoDepartment_Department]
    FOREIGN KEY ([Departments_ID])
    REFERENCES [dbo].[Departments]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserInfoes_ID] in table 'UserInfoDepartment'
ALTER TABLE [dbo].[UserInfoDepartment]
ADD CONSTRAINT [FK_UserInfoDepartment_UserInfo]
    FOREIGN KEY ([UserInfoes_ID])
    REFERENCES [dbo].[UserInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoDepartment_UserInfo'
CREATE INDEX [IX_FK_UserInfoDepartment_UserInfo]
ON [dbo].[UserInfoDepartment]
    ([UserInfoes_ID]);
GO

-- Creating foreign key on [RoleInfoes_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo]
    FOREIGN KEY ([RoleInfoes_ID])
    REFERENCES [dbo].[RoleInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserInfoes_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_UserInfo]
    FOREIGN KEY ([UserInfoes_ID])
    REFERENCES [dbo].[UserInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoRoleInfo_UserInfo'
CREATE INDEX [IX_FK_UserInfoRoleInfo_UserInfo]
ON [dbo].[UserInfoRoleInfo]
    ([UserInfoes_ID]);
GO

-- Creating foreign key on [WF_TempID] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [FK_WF_TempWF_Instance]
    FOREIGN KEY ([WF_TempID])
    REFERENCES [dbo].[WF_Temp]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_TempWF_Instance'
CREATE INDEX [IX_FK_WF_TempWF_Instance]
ON [dbo].[WF_Instance]
    ([WF_TempID]);
GO

-- Creating foreign key on [WF_InstanceID] in table 'WF_StepInfo'
ALTER TABLE [dbo].[WF_StepInfo]
ADD CONSTRAINT [FK_WF_InstanceWF_StepInfo]
    FOREIGN KEY ([WF_InstanceID])
    REFERENCES [dbo].[WF_Instance]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_InstanceWF_StepInfo'
CREATE INDEX [IX_FK_WF_InstanceWF_StepInfo]
ON [dbo].[WF_StepInfo]
    ([WF_InstanceID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------



insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('ASP.Net: The Complete Reference', 'Matthew MacDonald', 'February 1st 2002', '0072195134', 'Get the comprehensive low-down on all seven built-in .NET Framework namespaces--plus plenty of other useful information for developers, including relevant topics like security, Web services, database development, application deployment, and more.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Pro ASP.NET MVC 5', 'Adam Freeman', 'December 19th 2013', '', 'The ASP.NET MVC 5 Framework is the latest evolution of Microsoft’s ASP.NET web platform. It provides a high-productivity programming model that promotes cleaner code architecture, test-driven development, and powerful extensibility, combined with all the benefits of ASP.NET.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Programming ASP.Net', 'Jesse Liberty, Dan Hurwitz', 'November 2nd 2005', '059600916X', 'O''Reilly has once again updated its bestselling tutorial on ASP.NET, the world''s leading web development tool from Microsoft. In Programming ASP.NET, Third Edition, authors Jesse Liberty and Dan Hurwitz give you the lowdown on the technology''s latest version, ASP.NET 2.0, as well as Visual Studio 2005.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Professional ASP.NET MVC 4', ' Jon Galloway', 'October 2nd 2012', '111834846X', 'An outstanding author team presents the ultimate Wrox guide to ASP.NET MVC 4 Microsoft insiders join giants of the software development community to offer this in-depth guide to ASP.NET MVC, an essential web development technology. Experienced .NET and ASP.NET developers will find all the important information they need to build dynamic, data-driven websites with ASP.NET and the newest release of Microsoft''s Model-View-Controller technology. Featuring step-by-step guidance and lots of code samples, this guide gets you started and moves all the way to advanced topics, using plenty of examples. Designed to give experienced .NET and ASP.NET programmers everything needed to work with the newest version of MVC technology Expert author team includes Microsoft ASP.NET MVC insiders as well as leaders of the programming community Covers controllers, views, models, forms and HTML helpers, data annotation and validation, membership, authorization, security, and routing Includes essential topics such as Ajax and jQuery, NuGet, dependency injection, unit testing, extending MVC, and Razor Includes additional real-world coverage requested by readers of the previous edition as well as a new case study example chapter');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Professional ASP.Net Design Patterns', 'Scott Millett',  'September 16th 2010', '047095289X', 'Design patterns are time-tested solutions to recurring problems, letting the designer build programs on solutions that have already proved effective Provides developers with more than a dozen ASP.NET examples showing standard design patterns and how using them helpsbuild a richer understanding of ASP.NET architecture, as well as better ASP.NET applications Builds a solid understanding of ASP.NET architecture that can be used over and over again in many projects Covers ASP.NET code to implement many standard patterns including Model-View-Controller (MVC), ETL, Master-Master Snapshot, Master-Slave-Snapshot, Facade, Singleton, Factory, Single Access Point, Roles, Limited View, observer, page controller, common communication patterns, and more ');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Beginning ASP.Net 4.5 in C#', 'Matthew MacDonald', 'August 28th 2012', '1430242515', 'This book is the most comprehensive and up to date introduction to ASP.NET ever written. Focussing solely on C#, with no code samples duplicated in other languages, award winning author Matthew MacDonald introduces you to the very latest thinking and best practices for the ASP.NET 4.5 technology.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Mobile ASP.Net MVC 5', 'Eric Sowell', 'November 18th 2013', '1430250569', 'Mobile ASP.NET MVC 5 will take you step-by-step through the process of developing fluid content that adapts its layout to the client device using HTML, JavaScript and CSS, and responsive web design. This book introduces server-side techniques that allow you to show different content to different devices and make the most of their strengths and capabilities. Mobile ASP.NET MVC 5 includes a wide range of techniques, tips, and guidelines for dealing with some of the challenges of mobile web development, such as browser incompatibilities, varying device performance, and targeting older devices.

You''ll learn to:

Use responsive principles to build apps that display and perform well on a range of mobile devices. Leverage your server-side code to customize what you serve to the client, depending on its capabilities. Build an ASP.NET MVC custom view engine, use display modes effectively, and create reusable mobile components with custom HTML helpers. Make the most of new capabilities offered on some devices by interacting with native APIs. By the end of Mobile ASP.NET MVC 5, you should feel confident building web apps that successfully target anything from an iOS or Android device to a feature phone or an older mobile browser. Along the way, you''ll learn about the modern mobile web landscape and how to choose the approaches that are right for you, depending on your target audience.

This book is for the ASP.NET developer who knows how ASP.NET MVC works and is eager to learn how to use it for building mobile websites.

What you''ll learn Use responsive principles to build apps that display and perform well on a range of mobile devices. Leverage your server-side code to customize what you serve to the client, depending on its capabilities. Build an ASP.NET MVC custom view engine, use display modes effectively, and create reusable mobile components with custom HTML helpers. Make the most of new capabilities offered on some devices by interacting with native APIs. Learn tips and tricks for dealing with browser incompatibilities and targeting older devices. Benefit from the author''s experience as he guides you through a full range of modern mobile web strategy. Who this book is for

This book is for the ASP.NET developer who knows how ASP.NET MVC works and is eager to learn how to use it for building mobile websites. Thorough knowledge of ASP.NET MVC is not at all required but some is assumed. This book also assumes a little knowledge of HTML, CSS and JavaScript. You do not need any prior experience in mobile development. Table of ContentsChapter 1: The Basics of Responsive Web Design');




insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Memoirs of a Geisha', 'K.W. Jeter', 'April 7th 2011', '0857660977', 'HE INHERITED A WATCHMAKER''S STORE - AND A WHOLE HEAP OF TROUBLE. But idle sometime-musician George has little talent for clockwork. And when a shadowy figure tries to steal an old device from the premises, George finds himself embroiled in a mystery of time travel, music and sexual intrigue. A genuine lost classic, a steampunk original whose time has come.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Memoirs of a Geisha', 'K.W. Jeter', 'April 7th 2011', '0857660977', 'HE INHERITED A WATCHMAKER''S STORE - AND A WHOLE HEAP OF TROUBLE. But idle sometime-musician George has little talent for clockwork. And when a shadowy figure tries to steal an old device from the premises, George finds himself embroiled in a mystery of time travel, music and sexual intrigue. A genuine lost classic, a steampunk original whose time has come.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Memoirs of a Geisha', 'K.W. Jeter', 'April 7th 2011', '0857660977', 'HE INHERITED A WATCHMAKER''S STORE - AND A WHOLE HEAP OF TROUBLE. But idle sometime-musician George has little talent for clockwork. And when a shadowy figure tries to steal an old device from the premises, George finds himself embroiled in a mystery of time travel, music and sexual intrigue. A genuine lost classic, a steampunk original whose time has come.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Memoirs of a Geisha', 'K.W. Jeter', 'April 7th 2011', '0857660977', 'HE INHERITED A WATCHMAKER''S STORE - AND A WHOLE HEAP OF TROUBLE. But idle sometime-musician George has little talent for clockwork. And when a shadowy figure tries to steal an old device from the premises, George finds himself embroiled in a mystery of time travel, music and sexual intrigue. A genuine lost classic, a steampunk original whose time has come.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Memoirs of a Geisha', 'K.W. Jeter', 'April 7th 2011', '0857660977', 'HE INHERITED A WATCHMAKER''S STORE - AND A WHOLE HEAP OF TROUBLE. But idle sometime-musician George has little talent for clockwork. And when a shadowy figure tries to steal an old device from the premises, George finds himself embroiled in a mystery of time travel, music and sexual intrigue. A genuine lost classic, a steampunk original whose time has come.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Memoirs of a Geisha', 'K.W. Jeter', 'April 7th 2011', '0857660977', 'HE INHERITED A WATCHMAKER''S STORE - AND A WHOLE HEAP OF TROUBLE. But idle sometime-musician George has little talent for clockwork. And when a shadowy figure tries to steal an old device from the premises, George finds himself embroiled in a mystery of time travel, music and sexual intrigue. A genuine lost classic, a steampunk original whose time has come.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Memoirs of a Geisha', 'K.W. Jeter', 'April 7th 2011', '0857660977', 'HE INHERITED A WATCHMAKER''S STORE - AND A WHOLE HEAP OF TROUBLE. But idle sometime-musician George has little talent for clockwork. And when a shadowy figure tries to steal an old device from the premises, George finds himself embroiled in a mystery of time travel, music and sexual intrigue. A genuine lost classic, a steampunk original whose time has come.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Memoirs of a Geisha', 'K.W. Jeter', 'April 7th 2011', '0857660977', 'HE INHERITED A WATCHMAKER''S STORE - AND A WHOLE HEAP OF TROUBLE. But idle sometime-musician George has little talent for clockwork. And when a shadowy figure tries to steal an old device from the premises, George finds himself embroiled in a mystery of time travel, music and sexual intrigue. A genuine lost classic, a steampunk original whose time has come.');
insert into books(Title, Author, PublishedDate, ISBN, ContentDescription) values('Memoirs of a Geisha', 'K.W. Jeter', 'April 7th 2011', '0857660977', 'HE INHERITED A WATCHMAKER''S STORE - AND A WHOLE HEAP OF TROUBLE. But idle sometime-musician George has little talent for clockwork. And when a shadowy figure tries to steal an old device from the premises, George finds himself embroiled in a mystery of time travel, music and sexual intrigue. A genuine lost classic, a steampunk original whose time has come.');

select * from books

select * from SearchDetails;



-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/28/2016 18:00:07
-- Generated from EDMX file: D:\Co-op Project\OASystem\OA.Model\OA.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OA_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ActionInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentActionInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentActionInfo] DROP CONSTRAINT [FK_DepartmentActionInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentActionInfo_Department]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentActionInfo] DROP CONSTRAINT [FK_DepartmentActionInfo_Department];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoDepartment_Department]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoDepartment] DROP CONSTRAINT [FK_UserInfoDepartment_Department];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoDepartment_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoDepartment] DROP CONSTRAINT [FK_UserInfoDepartment_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_UserInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[book]', 'U') IS NOT NULL
    DROP TABLE [dbo].[book];
GO
IF OBJECT_ID(N'[dbo].[Department]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Department];
GO
IF OBJECT_ID(N'[dbo].[DepartmentActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentActionInfo];
GO
IF OBJECT_ID(N'[dbo].[KeyWordsRank]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KeyWordsRank];
GO
IF OBJECT_ID(N'[dbo].[R_UserInfo_ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfoActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfoActionInfo];
GO
IF OBJECT_ID(N'[dbo].[SearchDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SearchDetails];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfoDepartment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoDepartment];
GO
IF OBJECT_ID(N'[dbo].[UserInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoRoleInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ActionInfoes'

-- Creating table 'WF_Instance'
CREATE TABLE [dbo].[WF_Instance] (
    [ID] int IDENTITY(1,1) primary key NOT NULL,
    [InstanceName] nvarchar(max)  NULL,
    [SubTime] datetime  NOT NULL,
    [StartedBy] int  NOT NULL,
    [Level] smallint  NOT NULL,
    [SubForm] nvarchar(max)  NULL,
    [Status] smallint  NOT NULL,
    [Result] smallint  NOT NULL,
    [WF_TempID] int  NOT NULL,
    [ApplicationId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'WF_StepInfo'
CREATE TABLE [dbo].[WF_StepInfo] (
    [ID] int IDENTITY(1,1) primary key NOT NULL,
    [SetpName] nvarchar(32)  NOT NULL,
    [IsProcessed] bit  NOT NULL,
    [IsStartStep] bit  NOT NULL,
    [IsEndStep] bit  NOT NULL,
    [Title] nvarchar(32)  NULL,
    [Comment] nvarchar(max)  NULL,
    [StepResult] smallint  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ProcessTime] datetime  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [ProcessBy] int  NOT NULL,
    [ParentStepID] int  NOT NULL,
    [ChildStepID] int  NOT NULL,
    [WF_InstanceID] int  NOT NULL
);
GO

-- Creating table 'WF_Temp'
CREATE TABLE [dbo].[WF_Temp] (
    [ID] int IDENTITY(1,1) primary key NOT NULL,
    [TempName] nvarchar(32)  NULL,
    [SubTime] datetime  NOT NULL,
    [ModfiedOn] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(256)  NULL,
    [TempDescription] nvarchar(max)  NULL,
    [TempForm] nvarchar(max)  NOT NULL,
    [TempStatus] smallint  NOT NULL,
    [SubBy] int  NOT NULL
);
go

-- Creating foreign key on [WF_InstanceID] in table 'WF_StepInfo'
ALTER TABLE [dbo].[WF_StepInfo]
ADD CONSTRAINT [FK_WF_InstanceWF_StepInfo]
    FOREIGN KEY ([WF_InstanceID])
    REFERENCES [dbo].[WF_Instance]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_InstanceWF_StepInfo'
CREATE INDEX [IX_FK_WF_InstanceWF_StepInfo]
ON [dbo].[WF_StepInfo]
    ([WF_InstanceID]);
GO

-- Creating foreign key on [WF_TempID] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [FK_WF_TempWF_Instance]
    FOREIGN KEY ([WF_TempID])
    REFERENCES [dbo].[WF_Temp]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_TempWF_Instance'
CREATE INDEX [IX_FK_WF_TempWF_Instance]
ON [dbo].[WF_Instance]
    ([WF_TempID]);
GO


-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------