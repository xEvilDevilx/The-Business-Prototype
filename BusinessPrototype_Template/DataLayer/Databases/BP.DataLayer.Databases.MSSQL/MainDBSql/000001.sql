/*
	[Type]:        SQL Query for the Main Database

	[Description]: SQL Query for create a User table
----------------------------------------------------------------
	[Details]:	   2018/10/17 - Created, VTyagunov
*/

----------------------------------------------------------------
-- Create tables
----------------------------------------------------------------

-- USERS table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE [TABLE_SCHEMA] = 'dbo' and [TABLE_NAME] = 'USERS')
BEGIN
    CREATE TABLE [dbo].[USERS] (
        [USERID] [uniqueidentifier] NOT NULL UNIQUE,
		[USERGROUPID] [uniqueidentifier] NOT NULL,
        [LOGIN] [nvarchar](20) NOT NULL UNIQUE,
        [PASSWORD] [nvarchar](40) NOT NULL,
		[FIRSTNAME] [nvarchar](30) NOT NULL DEFAULT (''),
		[MIDDLENAME] [nvarchar](30) NOT NULL DEFAULT (''),
		[LASTNAME] [nvarchar](30) NOT NULL DEFAULT (''),
        [CREATIONDATETIME] [datetime] NOT NULL,
        [LASTLOGINDATETIME] [datetime] NULL,
		[BLOCKED] [bit] NOT NULL DEFAULT(0)
    CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED
        (
            [USERID] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = N'I_USERS_USERGROUPID')
BEGIN
    CREATE NONCLUSTERED INDEX [I_USERS_USERGROUPID]   
    ON [USERS] ([USERGROUPID]);   
END 
GO

-- USERGROUPS table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE [TABLE_SCHEMA] = 'dbo' and [TABLE_NAME] = 'USERGROUPS')
BEGIN
    CREATE TABLE [dbo].[USERGROUPS] (
        [USERGROUPID] [uniqueidentifier] NOT NULL,
        [GROUPNAME] [nvarchar](20) NOT NULL UNIQUE,
		[GROUPDESCRIPTION] [nvarchar](MAX) NOT NULL DEFAULT (''),
		[BLOCKED] [bit] NOT NULL DEFAULT(0),
		[USERGROUPLOCALIZEID] [uniqueidentifier] NOT NULL
    CONSTRAINT [PK_USERGROUPS] PRIMARY KEY CLUSTERED
        (
            [USERGROUPID] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO

-- USERGROUPLOCALIZE table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE [TABLE_SCHEMA] = 'dbo' and [TABLE_NAME] = 'USERGROUPLOCALIZE')
BEGIN
    CREATE TABLE [dbo].[USERGROUPLOCALIZE] (
        [USERGROUPLOCALIZEID] [uniqueidentifier] NOT NULL,
		[LANGUAGECODE] [nvarchar](3) NOT NULL,
		[TEXT] [nvarchar](60) NOT NULL
    CONSTRAINT [PK_USERGROUPLOCALIZE] PRIMARY KEY CLUSTERED
        (
            [USERGROUPLOCALIZEID] ASC,
			[LANGUAGECODE] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO

-- RIGHTS table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE [TABLE_SCHEMA] = 'dbo' and [TABLE_NAME] = 'RIGHTS')
BEGIN
    CREATE TABLE [dbo].[RIGHTS] (
        [RIGHTID] [uniqueidentifier] NOT NULL,
		[RIGHTNAME] [nvarchar](20) NOT NULL,
		[RIGHTLOCALIZEID] [uniqueidentifier] NOT NULL
    CONSTRAINT [PK_RIGHTS] PRIMARY KEY CLUSTERED
        (
            [RIGHTID] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO

-- RIGHTLOCALIZE table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE [TABLE_SCHEMA] = 'dbo' and [TABLE_NAME] = 'RIGHTLOCALIZE')
BEGIN
    CREATE TABLE [dbo].[RIGHTLOCALIZE] (
        [RIGHTLOCALIZEID] [uniqueidentifier] NOT NULL,
		[LANGUAGECODE] [nvarchar](3) NOT NULL,
		[TEXT] [nvarchar](60) NOT NULL
    CONSTRAINT [PK_RIGHTLOCALIZE] PRIMARY KEY CLUSTERED
        (
            [RIGHTLOCALIZEID] ASC,
			[LANGUAGECODE] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO

-- USERRIGHTS table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE [TABLE_SCHEMA] = 'dbo' and [TABLE_NAME] = 'USERRIGHTS')
BEGIN
    CREATE TABLE [dbo].[USERRIGHTS] (
        [USERID] [uniqueidentifier] NOT NULL,
        [RIGHTID] [uniqueidentifier] NOT NULL,
		[STATUS] [tinyint] NOT NULL
    CONSTRAINT [PK_USERRIGHTS] PRIMARY KEY CLUSTERED
        (
            [USERID] ASC,
			[RIGHTID] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO

-- USERGROUPRIGHTS table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE [TABLE_SCHEMA] = 'dbo' and [TABLE_NAME] = 'USERGROUPRIGHTS')
BEGIN
    CREATE TABLE [dbo].[USERGROUPRIGHTS] (
        [USERGROUPID] [uniqueidentifier] NOT NULL,
        [RIGHTID] [uniqueidentifier] NOT NULL,
		[STATUS] [tinyint] NOT NULL
    CONSTRAINT [PK_USERGROUPRIGHTS] PRIMARY KEY CLUSTERED
        (
            [USERGROUPID] ASC,
			[RIGHTID] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO

----------------------------------------------------------------
-- Insert test values
----------------------------------------------------------------

-- Add User - admin
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERS] WHERE [USERID] = '80c036f5-8272-4fb5-8593-f3122b096e7f' AND [LOGIN] = 'admin')
BEGIN
INSERT INTO [dbo].[USERS]
           ([USERID]
           ,[USERGROUPID]
           ,[LOGIN]
           ,[PASSWORD]
           ,[FIRSTNAME]
           ,[MIDDLENAME]
           ,[LASTNAME]
           ,[CREATIONDATETIME]
           ,[LASTLOGINDATETIME]
           ,[BLOCKED])
     VALUES
           ('80c036f5-8272-4fb5-8593-f3122b096e7f'
           ,'a2e0e67a-e75f-4632-acc8-4393e275f5b8'
           ,'admin'
           ,'12345'
           ,'Ad'
           ,'M'
           ,'iN'
           ,'2018-10-19T00:00:00.000'
           ,NULL
           ,0)
END
GO

-- Add User - tester
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERS] WHERE [USERID] = 'c894a4c2-9c21-4341-be4c-df0dc8a8a56e' AND [LOGIN] = 'tester')
BEGIN
INSERT INTO [dbo].[USERS]
           ([USERID]
           ,[USERGROUPID]
           ,[LOGIN]
           ,[PASSWORD]
           ,[FIRSTNAME]
           ,[MIDDLENAME]
           ,[LASTNAME]
           ,[CREATIONDATETIME]
           ,[LASTLOGINDATETIME]
           ,[BLOCKED])
     VALUES
           ('c894a4c2-9c21-4341-be4c-df0dc8a8a56e'
           ,'23a3d18c-7c58-4f5f-949e-b09693bc0761'
           ,'tester'
           ,'12345'
           ,'Te'
           ,'St'
           ,'Er'
           ,'2018-10-19T00:00:00.000'
           ,NULL
           ,0)
END
GO

-- Add Right 1
IF NOT EXISTS(SELECT 1 FROM [dbo].[RIGHTS] WHERE [RIGHTID] = 'd7e16f52-60fd-4039-a48f-af999a4faf12')
BEGIN
INSERT INTO [dbo].[RIGHTS]
           ([RIGHTID]
           ,[RIGHTNAME]
           ,[RIGHTLOCALIZEID])
     VALUES
           ('d7e16f52-60fd-4039-a48f-af999a4faf12'
           ,'TestRight1'
           ,'d50c8a5f-161e-498d-97d9-882ff1005d29')
END
GO

-- Add Right 2
IF NOT EXISTS(SELECT 1 FROM [dbo].[RIGHTS] WHERE [RIGHTID] = 'dff228b0-ee12-443a-bd93-43d28dbe858a')
BEGIN
INSERT INTO [dbo].[RIGHTS]
           ([RIGHTID]
           ,[RIGHTNAME]
           ,[RIGHTLOCALIZEID])
     VALUES
           ('dff228b0-ee12-443a-bd93-43d28dbe858a'
           ,'TestRight2'
           ,'56021d73-633f-4a23-a980-b7b1e1100db2')
END
GO

-- Add Right 3
IF NOT EXISTS(SELECT 1 FROM [dbo].[RIGHTS] WHERE [RIGHTID] = 'cd0fec5e-1b89-4fe8-a557-30555d94c174')
BEGIN
INSERT INTO [dbo].[RIGHTS]
           ([RIGHTID]
           ,[RIGHTNAME]
           ,[RIGHTLOCALIZEID])
     VALUES
           ('cd0fec5e-1b89-4fe8-a557-30555d94c174'
           ,'TestRight3'
           ,'4c310ffc-7905-44b4-9ff4-ec80dc5b2939')
END
GO

-- Add Right Localize ENG 1
IF NOT EXISTS(SELECT 1 FROM [dbo].[RIGHTLOCALIZE] WHERE [RIGHTLOCALIZEID] = 'd50c8a5f-161e-498d-97d9-882ff1005d29' AND [LANGUAGECODE] = 'ENG')
BEGIN
INSERT INTO [dbo].[RIGHTLOCALIZE]
           ([RIGHTLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('d50c8a5f-161e-498d-97d9-882ff1005d29'
           ,'ENG'
           ,'Test Right #1')
END
GO

-- Add Right Localize ENG 2
IF NOT EXISTS(SELECT 1 FROM [dbo].[RIGHTLOCALIZE] WHERE [RIGHTLOCALIZEID] = '56021d73-633f-4a23-a980-b7b1e1100db2' AND [LANGUAGECODE] = 'ENG')
BEGIN
INSERT INTO [dbo].[RIGHTLOCALIZE]
           ([RIGHTLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('56021d73-633f-4a23-a980-b7b1e1100db2'
           ,'ENG'
           ,'Test Right #2')
END
GO

-- Add Right Localize ENG 3
IF NOT EXISTS(SELECT 1 FROM [dbo].[RIGHTLOCALIZE] WHERE [RIGHTLOCALIZEID] = '4c310ffc-7905-44b4-9ff4-ec80dc5b2939' AND [LANGUAGECODE] = 'ENG')
BEGIN
INSERT INTO [dbo].[RIGHTLOCALIZE]
           ([RIGHTLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('4c310ffc-7905-44b4-9ff4-ec80dc5b2939'
           ,'ENG'
           ,'Test Right #3')
END
GO

-- Add Right Localize RUS 1
IF NOT EXISTS(SELECT 1 FROM [dbo].[RIGHTLOCALIZE] WHERE [RIGHTLOCALIZEID] = 'd50c8a5f-161e-498d-97d9-882ff1005d29' AND [LANGUAGECODE] = 'RUS')
BEGIN
INSERT INTO [dbo].[RIGHTLOCALIZE]
           ([RIGHTLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('d50c8a5f-161e-498d-97d9-882ff1005d29'
           ,'RUS'
           ,'Тестовое право №1')
END
GO

-- Add Right Localize RUS 2
IF NOT EXISTS(SELECT 1 FROM [dbo].[RIGHTLOCALIZE] WHERE [RIGHTLOCALIZEID] = '56021d73-633f-4a23-a980-b7b1e1100db2' AND [LANGUAGECODE] = 'RUS')
BEGIN
INSERT INTO [dbo].[RIGHTLOCALIZE]
           ([RIGHTLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('56021d73-633f-4a23-a980-b7b1e1100db2'
           ,'RUS'
           ,'Тестовое право №2')
END
GO

-- Add Right Localize RUS 3
IF NOT EXISTS(SELECT 1 FROM [dbo].[RIGHTLOCALIZE] WHERE [RIGHTLOCALIZEID] = '4c310ffc-7905-44b4-9ff4-ec80dc5b2939' AND [LANGUAGECODE] = 'RUS')
BEGIN
INSERT INTO [dbo].[RIGHTLOCALIZE]
           ([RIGHTLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('4c310ffc-7905-44b4-9ff4-ec80dc5b2939'
           ,'RUS'
           ,'Тестовое право №3')
END
GO

-- Add User Right 1
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERRIGHTS] WHERE [USERID] = 'c894a4c2-9c21-4341-be4c-df0dc8a8a56e' AND [RIGHTID] = 'd7e16f52-60fd-4039-a48f-af999a4faf12')
BEGIN
INSERT INTO [dbo].[USERRIGHTS]
           ([USERID]
           ,[RIGHTID]
           ,[STATUS])
     VALUES
           ('c894a4c2-9c21-4341-be4c-df0dc8a8a56e'
           ,'d7e16f52-60fd-4039-a48f-af999a4faf12'
           ,1)
END
GO

-- Add User Right 2
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERRIGHTS] WHERE [USERID] = 'c894a4c2-9c21-4341-be4c-df0dc8a8a56e' AND [RIGHTID] = 'dff228b0-ee12-443a-bd93-43d28dbe858a')
BEGIN
INSERT INTO [dbo].[USERRIGHTS]
           ([USERID]
           ,[RIGHTID]
           ,[STATUS])
     VALUES
           ('c894a4c2-9c21-4341-be4c-df0dc8a8a56e'
           ,'dff228b0-ee12-443a-bd93-43d28dbe858a'
           ,1)
END
GO

-- Add User Group Administrators
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = 'a2e0e67a-e75f-4632-acc8-4393e275f5b8' AND [GROUPNAME] = 'Administrators')
BEGIN
INSERT INTO [dbo].[USERGROUPS]
           ([USERGROUPID]
           ,[GROUPNAME]
           ,[GROUPDESCRIPTION]
		   ,[BLOCKED]
		   ,[USERGROUPLOCALIZEID])
     VALUES
           ('a2e0e67a-e75f-4632-acc8-4393e275f5b8'
           ,'Administrators'
		   ,'Group for Administrators'
		   ,0
           ,'9b8e4882-c67f-4fb1-829e-6e24e6502090')
END
GO

-- Add User Group Worker
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = '23a3d18c-7c58-4f5f-949e-b09693bc0761' AND [GROUPNAME] = 'Worker')
BEGIN
INSERT INTO [dbo].[USERGROUPS]
           ([USERGROUPID]
           ,[GROUPNAME]
           ,[GROUPDESCRIPTION]
		   ,[BLOCKED]
		   ,[USERGROUPLOCALIZEID])
     VALUES
           ('23a3d18c-7c58-4f5f-949e-b09693bc0761'
           ,'Worker'
		   ,'Group for Worker'
		   ,0
           ,'fb4fb734-5353-4809-970c-047279a466e9')
END
GO

-- Add User Group Localize ENG 1
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPLOCALIZE] WHERE [USERGROUPLOCALIZEID] = '9b8e4882-c67f-4fb1-829e-6e24e6502090' AND [LANGUAGECODE] = 'ENG')
BEGIN
INSERT INTO [dbo].[USERGROUPLOCALIZE]
           ([USERGROUPLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('9b8e4882-c67f-4fb1-829e-6e24e6502090'
           ,'ENG'
           ,'Administrators')
END
GO

-- Add User Group Localize ENG 2
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPLOCALIZE] WHERE [USERGROUPLOCALIZEID] = 'fb4fb734-5353-4809-970c-047279a466e9' AND [LANGUAGECODE] = 'ENG')
BEGIN
INSERT INTO [dbo].[USERGROUPLOCALIZE]
           ([USERGROUPLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('fb4fb734-5353-4809-970c-047279a466e9'
           ,'ENG'
           ,'Worker')
END
GO

-- Add User Group Localize RUS 1
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPLOCALIZE] WHERE [USERGROUPLOCALIZEID] = '9b8e4882-c67f-4fb1-829e-6e24e6502090' AND [LANGUAGECODE] = 'RUS')
BEGIN
INSERT INTO [dbo].[USERGROUPLOCALIZE]
           ([USERGROUPLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('9b8e4882-c67f-4fb1-829e-6e24e6502090'
           ,'RUS'
           ,'Администраторы')
END
GO

-- Add User Group Localize RUS 2
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPLOCALIZE] WHERE [USERGROUPLOCALIZEID] = 'fb4fb734-5353-4809-970c-047279a466e9' AND [LANGUAGECODE] = 'RUS')
BEGIN
INSERT INTO [dbo].[USERGROUPLOCALIZE]
           ([USERGROUPLOCALIZEID]
           ,[LANGUAGECODE]
           ,[TEXT])
     VALUES
           ('fb4fb734-5353-4809-970c-047279a466e9'
           ,'RUS'
           ,'Рабочие')
END
GO

-- Add User Group Rights 1 for Administrators
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPRIGHTS] WHERE [USERGROUPID] = 'A2E0E67A-E75F-4632-ACC8-4393E275F5B8' AND [RIGHTID] = 'D7E16F52-60FD-4039-A48F-AF999A4FAF12')
BEGIN
INSERT INTO [dbo].[USERGROUPRIGHTS]
           ([USERGROUPID]
           ,[RIGHTID]
           ,[STATUS])
     VALUES
           ('A2E0E67A-E75F-4632-ACC8-4393E275F5B8'
           ,'D7E16F52-60FD-4039-A48F-AF999A4FAF12'
		   ,1)
END
GO

-- Add User Group Rights 2 for Administrators
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPRIGHTS] WHERE [USERGROUPID] = 'A2E0E67A-E75F-4632-ACC8-4393E275F5B8' AND [RIGHTID] = 'DFF228B0-EE12-443A-BD93-43D28DBE858A')
BEGIN
INSERT INTO [dbo].[USERGROUPRIGHTS]
           ([USERGROUPID]
           ,[RIGHTID]
           ,[STATUS])
     VALUES
           ('A2E0E67A-E75F-4632-ACC8-4393E275F5B8'
           ,'DFF228B0-EE12-443A-BD93-43D28DBE858A'
		   ,1)
END
GO

-- Add User Group Rights 3 for Administrators
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPRIGHTS] WHERE [USERGROUPID] = 'A2E0E67A-E75F-4632-ACC8-4393E275F5B8' AND [RIGHTID] = 'CD0FEC5E-1B89-4FE8-A557-30555D94C174')
BEGIN
INSERT INTO [dbo].[USERGROUPRIGHTS]
           ([USERGROUPID]
           ,[RIGHTID]
           ,[STATUS])
     VALUES
           ('A2E0E67A-E75F-4632-ACC8-4393E275F5B8'
           ,'CD0FEC5E-1B89-4FE8-A557-30555D94C174'
		   ,1)
END
GO

-- Add User Group Rights 1 for Workers
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPRIGHTS] WHERE [USERGROUPID] = '23A3D18C-7C58-4F5F-949E-B09693BC0761' AND [RIGHTID] = 'D7E16F52-60FD-4039-A48F-AF999A4FAF12')
BEGIN
INSERT INTO [dbo].[USERGROUPRIGHTS]
           ([USERGROUPID]
           ,[RIGHTID]
           ,[STATUS])
     VALUES
           ('23A3D18C-7C58-4F5F-949E-B09693BC0761'
           ,'D7E16F52-60FD-4039-A48F-AF999A4FAF12'
		   ,0)
END
GO

-- Add User Group Rights 2 for Workers
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPRIGHTS] WHERE [USERGROUPID] = '23A3D18C-7C58-4F5F-949E-B09693BC0761' AND [RIGHTID] = 'DFF228B0-EE12-443A-BD93-43D28DBE858A')
BEGIN
INSERT INTO [dbo].[USERGROUPRIGHTS]
           ([USERGROUPID]
           ,[RIGHTID]
           ,[STATUS])
     VALUES
           ('23A3D18C-7C58-4F5F-949E-B09693BC0761'
           ,'DFF228B0-EE12-443A-BD93-43D28DBE858A'
		   ,0)
END
GO

-- Add User Group Rights 3 for Workers
IF NOT EXISTS(SELECT 1 FROM [dbo].[USERGROUPRIGHTS] WHERE [USERGROUPID] = '23A3D18C-7C58-4F5F-949E-B09693BC0761' AND [RIGHTID] = 'CD0FEC5E-1B89-4FE8-A557-30555D94C174')
BEGIN
INSERT INTO [dbo].[USERGROUPRIGHTS]
           ([USERGROUPID]
           ,[RIGHTID]
           ,[STATUS])
     VALUES
           ('23A3D18C-7C58-4F5F-949E-B09693BC0761'
           ,'CD0FEC5E-1B89-4FE8-A557-30555D94C174'
		   ,0)
END
GO

----------------------------------------------------------------
-- Create procedures
----------------------------------------------------------------

	-- @Result cases:
	-- 0 - error
	-- 1 - success
	-- 2 - record already exists
	-- 3 - record not exists
		
------------------------------------------
-- User SPs

-- Create Procedure Add New User
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_ADDNEWUSER]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_ADDNEWUSER]
GO

CREATE PROCEDURE [dbo].[PROC_ADDNEWUSER](
	@Login NVARCHAR(20),
	@Password NVARCHAR(40),
    @UserGroupID UNIQUEIDENTIFIER, 
	@FirstName NVARCHAR(30),
	@MiddleName NVARCHAR(30),
	@LastName NVARCHAR(30),
	@Blocked BIT,
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	DECLARE @UserID UNIQUEIDENTIFIER;
	DECLARE @DefaultUserGroupID UNIQUEIDENTIFIER;
	DECLARE @CreationDataTime DATETIME;

	SET @UserID = NEWID();
	SET @DefaultUserGroupID = '23A3D18C-7C58-4F5F-949E-B09693BC0761';
	SET @CreationDataTime = CURRENT_TIMESTAMP;

	IF @Login = '' OR (@Login IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@Login parameter is empty!';
		RETURN @Result;
	END

	IF @Password = '' OR (@Password IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@Password parameter is empty!';
		RETURN @Result;
	END

    IF EXISTS(SELECT * FROM [dbo].[USERS] WHERE [LOGIN] = @Login)
	BEGIN
		SELECT @Result = 2;
		RETURN @Result;
	END
		
	IF @UserGroupID = NULL OR @UserGroupID = '00000000-0000-0000-0000-000000000000' OR @UserGroupID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserGroupID IS NULL)
	BEGIN
		IF EXISTS(SELECT * FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = @DefaultUserGroupID)
			SET @UserGroupID = @DefaultUserGroupID;
	END
	ELSE
	BEGIN
		IF NOT EXISTS(SELECT * FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = @UserGroupID)
		BEGIN
			IF EXISTS(SELECT * FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = @DefaultUserGroupID)
				SET @UserGroupID = @DefaultUserGroupID;
	END
	END
	
	IF @Blocked = NULL OR (@Blocked IS NULL)
	BEGIN
		SET @Blocked = 0;
	END

	BEGIN
		INSERT INTO [dbo].[USERS]
			   ([USERID]
			   ,[USERGROUPID]
			   ,[LOGIN]
			   ,[PASSWORD]
			   ,[FIRSTNAME]
			   ,[MIDDLENAME]
			   ,[LASTNAME]
			   ,[CREATIONDATETIME]
			   ,[LASTLOGINDATETIME]
			   ,[BLOCKED])
		 VALUES
			   (@UserID
			   ,@UserGroupID
			   ,@Login
			   ,@Password
			   ,@FirstName
			   ,@MiddleName
			   ,@LastName
			   ,@CreationDataTime
			   ,NULL
			   ,@Blocked)
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Update User
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_UPDATEUSER]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_UPDATEUSER]
GO

CREATE PROCEDURE [dbo].[PROC_UPDATEUSER](
	@UserID UNIQUEIDENTIFIER,
	@Login NVARCHAR(20),
	@Password NVARCHAR(40),
    @UserGroupID UNIQUEIDENTIFIER, 
	@FirstName NVARCHAR(30),
	@MiddleName NVARCHAR(30),
	@LastName NVARCHAR(30),
	@LastLoginDateTime DATETIME,
	@Blocked BIT,
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	DECLARE @UpdateUserGroupIDFlag BIT;
	SET @UpdateUserGroupIDFlag = 0;

	IF @UserID = NULL OR @UserID = '00000000-0000-0000-0000-000000000000' OR @UserID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERS] WHERE [USERID] = @UserID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END

	SELECT [u].[LOGIN], [u].[USERGROUPID], [u].[PASSWORD], [u].[FIRSTNAME], [u].[MIDDLENAME], [u].[LASTNAME], [u].[LASTLOGINDATETIME], [u].[BLOCKED]
	INTO #USERTABLETEMP
	FROM [dbo].[USERS] as [u] WHERE [u].[USERID] = @UserID;
		
	IF @Login = NULL OR @Login = '' OR (@Login IS NULL)
	BEGIN
		SET @Login = (SELECT [LOGIN] FROM #USERTABLETEMP);
	END

	IF @Password = NULL OR @Password = '' OR (@Password IS NULL)
	BEGIN
		SET @Password = (SELECT [PASSWORD] FROM #USERTABLETEMP);
	END
	
	IF @UserGroupID = NULL OR @UserGroupID = '00000000-0000-0000-0000-000000000000' OR @UserGroupID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserGroupID IS NULL)
	BEGIN
		SET @UserGroupID = (SELECT [USERGROUPID] FROM #USERTABLETEMP);
		SET @UpdateUserGroupIDFlag = 1;
	END

	IF @FirstName = NULL OR @FirstName = '' OR (@FirstName IS NULL)
	BEGIN
		SET @FirstName = (SELECT [FIRSTNAME] FROM #USERTABLETEMP);
	END

	IF @MiddleName = NULL OR @MiddleName = '' OR (@MiddleName IS NULL)
	BEGIN
		SET @MiddleName = (SELECT [MIDDLENAME] FROM #USERTABLETEMP);
	END

	IF @LastName = NULL OR @LastName = '' OR (@LastName IS NULL)
	BEGIN
		SET @LastName = (SELECT [LASTNAME] FROM #USERTABLETEMP);
	END

	IF @LastLoginDateTime = NULL OR @LastLoginDateTime = '' OR (@LastLoginDateTime IS NULL)
	BEGIN
		SET @LastLoginDateTime = (SELECT [LASTLOGINDATETIME] FROM #USERTABLETEMP);
	END
	
	IF @Blocked = NULL OR @Blocked = '' OR (@Blocked IS NULL)
	BEGIN
		SET @Blocked = (SELECT [BLOCKED] FROM #USERTABLETEMP);
	END
	
	BEGIN
		UPDATE [dbo].[USERS]
		SET [LOGIN] = @Login
		   ,[USERGROUPID] = @UserGroupID
		   ,[PASSWORD] = @Password
		   ,[FIRSTNAME] = @FirstName
		   ,[MIDDLENAME] = @MiddleName
		   ,[LASTNAME] = @LastName
		   ,[LASTLOGINDATETIME] = @LastLoginDateTime
		   ,[BLOCKED] = @Blocked
		WHERE [USERID] = @UserID;
	END

	IF @UpdateUserGroupIDFlag = 1	
	BEGIN
		DELETE FROM [dbo].[USERRIGHTS]
		WHERE [USERID] = @UserID;
	END

	DROP TABLE #USERTABLETEMP;
	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END

-- Create Procedure Get User
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_GETUSER]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_GETUSER]
GO

CREATE PROCEDURE [dbo].[PROC_GETUSER](
	@UserID UNIQUEIDENTIFIER,
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	IF @UserID = NULL OR @UserID = '00000000-0000-0000-0000-000000000000' OR @UserID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERS] WHERE [USERID] = @UserID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END

	BEGIN
		SELECT [USERID]
		  ,[USERGROUPID]
		  ,[LOGIN]
		  ,[PASSWORD]
		  ,[FIRSTNAME]
		  ,[MIDDLENAME]
		  ,[LASTNAME]
		  ,[CREATIONDATETIME]
		  ,[LASTLOGINDATETIME]
		  ,[BLOCKED]
		FROM [dbo].[USERS]
		WHERE [USERID] = @UserID;
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Get User List
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_GETUSERLIST]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_GETUSERLIST]
GO

CREATE PROCEDURE [dbo].[PROC_GETUSERLIST](
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	BEGIN
		SELECT [USERID]
		  ,[USERGROUPID]
		  ,[LOGIN]
		  ,[PASSWORD]
		  ,[FIRSTNAME]
		  ,[MIDDLENAME]
		  ,[LASTNAME]
		  ,[CREATIONDATETIME]
		  ,[LASTLOGINDATETIME]
		  ,[BLOCKED]
		FROM [dbo].[USERS];
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Delete User
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_DELETEUSER]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_DELETEUSER]
GO

CREATE PROCEDURE [dbo].[PROC_DELETEUSER](
	@UserID UNIQUEIDENTIFIER,
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	IF @UserID = NULL OR @UserID = '00000000-0000-0000-0000-000000000000' OR @UserID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERS] WHERE [USERID] = @UserID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END

	BEGIN
		DELETE FROM [dbo].[USERS]
		WHERE [USERID] = @UserID;
	END

	BEGIN
		DELETE FROM [dbo].[USERRIGHTS]
		WHERE [USERID] = @UserID;
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Search Users
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_SEARCHUSERS]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_SEARCHUSERS]
GO

CREATE PROCEDURE [dbo].[PROC_SEARCHUSERS](
	@UserID UNIQUEIDENTIFIER NULL,
	@UserGroupID UNIQUEIDENTIFIER NULL,
	@Login NVARCHAR(20) NULL,    
	@FirstName NVARCHAR(30) NULL,
	@MiddleName NVARCHAR(30) NULL,
	@LastName NVARCHAR(30) NULL,
	@CreateDateTimeFrom DATETIME NULL,
	@CreateDateTimeTo DATETIME NULL,
	@LastLoginDateTimeFrom DATETIME NULL,
	@LastLoginDateTimeTo DATETIME NULL,
	@Blocked BIT NULL,
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	BEGIN
		SELECT [USERID]
		  ,[USERGROUPID]
		  ,[LOGIN]
		  ,[PASSWORD]
		  ,[FIRSTNAME]
		  ,[MIDDLENAME]
		  ,[LASTNAME]
		  ,[CREATIONDATETIME]
		  ,[LASTLOGINDATETIME]
		  ,[BLOCKED]
		FROM [dbo].[USERS]
		WHERE (@UserID IS NULL OR [USERID] = @UserID) AND (@UserGroupID IS NULL OR [USERGROUPID] = @UserGroupID) AND (@Login IS NULL OR [LOGIN] = @Login)
		  AND (@FirstName IS NULL OR [FIRSTNAME] = @FirstName) AND (@MiddleName IS NULL OR [MIDDLENAME] = @MiddleName) AND (@LastName IS NULL OR [LASTNAME] = @LastName)
		  AND (@CreateDateTimeFrom IS NULL OR [CREATIONDATETIME] >= @CreateDateTimeFrom) AND (@CreateDateTimeTo IS NULL OR [CREATIONDATETIME] <= @CreateDateTimeTo)
		  AND (@LastLoginDateTimeFrom IS NULL OR [LASTLOGINDATETIME] >= @LastLoginDateTimeFrom) AND (@LastLoginDateTimeTo IS NULL OR [LASTLOGINDATETIME] <= @LastLoginDateTimeTo)
		  AND (@Blocked IS NULL OR [BLOCKED] = @Blocked);
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

------------------------------------------
-- User Groups SPs

-- Create Procedure Add New User Group
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_ADDNEWUSERGROUP]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_ADDNEWUSERGROUP]
GO

CREATE PROCEDURE [dbo].[PROC_ADDNEWUSERGROUP](
	@GroupName NVARCHAR(20),
	@GroupDescription NVARCHAR(200),
	@Blocked BIT,
	@LanguageCode NVARCHAR(3),
	@GroupNameTranslation NVARCHAR(60),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	DECLARE @UserGroupID UNIQUEIDENTIFIER;
	DECLARE @UserGroupTranslationID UNIQUEIDENTIFIER;

	SET @UserGroupID = NEWID();
	SET @UserGroupTranslationID = NEWID();

	IF @GroupName = '' OR (@GroupName IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@GroupName parameter is empty!';
		RETURN @Result;
	END

    IF EXISTS(SELECT * FROM [dbo].[USERGROUPS] WHERE [GROUPNAME] = @GroupName)
	BEGIN
		SELECT @Result = 2;
		RETURN @Result;
	END
	
	IF @Blocked = NULL OR (@Blocked IS NULL)
	BEGIN
		SET @Blocked = 0;
	END

	IF @LanguageCode = '' OR (@LanguageCode IS NULL)
	BEGIN
		SET @LanguageCode = 'ENG'; -- Change to: SET @LanguageCode = @DefaultLanguageCode;
	END
	
	IF @GroupNameTranslation = '' OR (@GroupNameTranslation IS NULL)
	BEGIN
		SET @GroupNameTranslation = @GroupName;
	END

	BEGIN
		INSERT INTO [dbo].[USERGROUPLOCALIZE]
			   ([USERGROUPLOCALIZEID]
			   ,[LANGUAGECODE]
			   ,[TEXT])
		 VALUES
			   (@UserGroupTranslationID
			   ,@LanguageCode
			   ,@GroupNameTranslation)
	END

	BEGIN
		INSERT INTO [dbo].[USERGROUPS]
			   ([USERGROUPID]
			   ,[GROUPNAME]
			   ,[GROUPDESCRIPTION]
			   ,[BLOCKED]
			   ,[USERGROUPLOCALIZEID])
		 VALUES
			   (@UserGroupID
			   ,@GroupName
			   ,@GroupDescription
			   ,@Blocked
			   ,@UserGroupTranslationID)
	END

	SELECT [RIGHTID] INTO #RIGHTSTEMP FROM [dbo].[RIGHTS];

	BEGIN
		INSERT INTO [dbo].[USERGROUPRIGHTS] 
			([USERGROUPID]
            ,[RIGHTID]
            ,[STATUS]) 
		SELECT @UserGroupID, [rs].[RIGHTID], 0 FROM #RIGHTSTEMP [rs]
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Update User Group
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_UPDATEUSERGROUP]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_UPDATEUSERGROUP]
GO

CREATE PROCEDURE [dbo].[PROC_UPDATEUSERGROUP](
	@UserGroupID UNIQUEIDENTIFIER,
	@GroupName NVARCHAR(20),
	@GroupDescription NVARCHAR(200),
	@Blocked BIT,
	@LanguageCode NVARCHAR(3),
	@GroupNameTranslation NVARCHAR(60),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	DECLARE @UserGroupTranslationID UNIQUEIDENTIFIER;
	DECLARE @UpdateTranslation BIT;

	IF @UserGroupID = NULL OR @UserGroupID = '00000000-0000-0000-0000-000000000000' OR @UserGroupID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserGroupID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserGroupID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = @UserGroupID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END
		
	IF @GroupNameTranslation = '' OR (@GroupNameTranslation IS NULL)
	BEGIN
		SET @UpdateTranslation = 0;
	END
	ELSE
	BEGIN
		SET @UpdateTranslation = 1;
		IF @LanguageCode = '' OR (@LanguageCode IS NULL)
		BEGIN
			SELECT @Result = 0, @ErrorMessage = '@LanguageCode parameter is empty!';
			RETURN @Result;
		END
	END

	SELECT 
		[ug].[GROUPNAME] AS [GROUPNAME], 
		[ug].[GROUPDESCRIPTION] AS [GROUPDESCRIPTION], 
		[ug].[BLOCKED] AS [BLOCKED], 
		ISNULL([ugl].[USERGROUPLOCALIZEID], NULL) AS [USERGROUPLOCALIZEID], 
		ISNULL([ugl].[LANGUAGECODE], '') AS [LANGUAGECODE],
		ISNULL([ugl].[TEXT], '') AS [TEXT]
	INTO #USERGROUPTABLETEMP
	FROM [dbo].[USERGROUPS] AS [ug] 
	LEFT OUTER JOIN [USERGROUPLOCALIZE] [ugl] ON [ug].[USERGROUPLOCALIZEID] = [ugl].[USERGROUPLOCALIZEID] AND @LanguageCode = [ugl].[LANGUAGECODE]
	WHERE [ug].[USERGROUPID] = @UserGroupID;

	IF @UpdateTranslation = 1
	BEGIN
		SET @UserGroupTranslationID = (SELECT [USERGROUPLOCALIZEID] FROM #USERGROUPTABLETEMP);
	END

	IF @GroupName = NULL OR @GroupName = '' OR (@GroupName IS NULL)
	BEGIN
		SET @GroupName = (SELECT [GROUPNAME] FROM #USERGROUPTABLETEMP);
	END

	IF @GroupDescription = NULL OR (@GroupDescription IS NULL)
	BEGIN
		SET @GroupDescription = (SELECT [GROUPDESCRIPTION] FROM #USERGROUPTABLETEMP);
	END
	
	IF @Blocked = NULL OR (@Blocked IS NULL)
	BEGIN
		SET @Blocked = (SELECT [BLOCKED] FROM #USERGROUPTABLETEMP);
	END
	
	IF @UpdateTranslation = 1
	BEGIN
		UPDATE [dbo].[USERGROUPLOCALIZE]
		SET [TEXT] = @GroupNameTranslation
		WHERE [USERGROUPLOCALIZEID] = @UserGroupTranslationID AND [LANGUAGECODE] = @LanguageCode;
	END
		
	BEGIN
		UPDATE [dbo].[USERGROUPS]
		SET [GROUPNAME] = @GroupName
		   ,[GROUPDESCRIPTION] = @GroupDescription
		   ,[BLOCKED] = @Blocked
		WHERE [USERGROUPID] = @UserGroupID
	END

	DROP TABLE #USERGROUPTABLETEMP;
	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END

-- Create Procedure Get User Group
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_GETUSERGROUP]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_GETUSERGROUP]
GO

CREATE PROCEDURE [dbo].[PROC_GETUSERGROUP](
	@UserGroupID UNIQUEIDENTIFIER,
	@LanguageCode NVARCHAR(3),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	IF @UserGroupID = NULL OR @UserGroupID = '00000000-0000-0000-0000-000000000000' OR @UserGroupID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserGroupID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserGroupID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = @UserGroupID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END

	IF @LanguageCode = '' OR (@LanguageCode IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@LanguageCode parameter is empty!';
		RETURN @Result;
	END

	BEGIN
		SELECT 
			[ug].[USERGROUPID] AS [USERGROUPID],
			[ug].[GROUPNAME] AS [GROUPNAME], 
			[ug].[GROUPDESCRIPTION] AS [GROUPDESCRIPTION], 
			[ug].[BLOCKED] AS [BLOCKED], 
			ISNULL([ugl].[TEXT], NULL) AS [TEXT]
		FROM [dbo].[USERGROUPS] AS [ug] 
		LEFT OUTER JOIN [USERGROUPLOCALIZE] [ugl] ON [ug].[USERGROUPLOCALIZEID] = [ugl].[USERGROUPLOCALIZEID] AND @LanguageCode = [ugl].[LANGUAGECODE]
		WHERE [ug].[USERGROUPID] = @UserGroupID;
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Get User Group List
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_GETUSERGROUPLIST]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_GETUSERGROUPLIST]
GO

CREATE PROCEDURE [dbo].[PROC_GETUSERGROUPLIST](
	@LanguageCode NVARCHAR(3),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	IF @LanguageCode = '' OR (@LanguageCode IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@LanguageCode parameter is empty!';
		RETURN @Result;
	END

	BEGIN
		SELECT 
			[ug].[USERGROUPID] AS [USERGROUPID],
			[ug].[GROUPNAME] AS [GROUPNAME], 
			[ug].[GROUPDESCRIPTION] AS [GROUPDESCRIPTION], 
			[ug].[BLOCKED] AS [BLOCKED], 
			ISNULL([ugl].[TEXT], NULL) AS [TEXT]
		FROM [dbo].[USERGROUPS] AS [ug] 
		LEFT OUTER JOIN [USERGROUPLOCALIZE] [ugl] ON [ug].[USERGROUPLOCALIZEID] = [ugl].[USERGROUPLOCALIZEID] AND @LanguageCode = [ugl].[LANGUAGECODE]
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Delete User Group
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_DELETEUSERGROUP]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_DELETEUSERGROUP]
GO

CREATE PROCEDURE [dbo].[PROC_DELETEUSERGROUP](
	@UserGroupID UNIQUEIDENTIFIER,
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	DECLARE @UserGroupLocalizeID UNIQUEIDENTIFIER;

	IF @UserGroupID = NULL OR @UserGroupID = '00000000-0000-0000-0000-000000000000' OR @UserGroupID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserGroupID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserGroupID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = @UserGroupID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END

	SET @UserGroupLocalizeID = (SELECT [USERGROUPLOCALIZEID] FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = @UserGroupID);

	BEGIN
		DELETE FROM [dbo].[USERGROUPLOCALIZE]
		WHERE [USERGROUPLOCALIZEID] = @UserGroupLocalizeID;
	END

	BEGIN
		DELETE FROM [dbo].[USERGROUPS]
		WHERE [USERGROUPID] = @UserGroupID;
	END
	
	BEGIN
		DELETE FROM [dbo].[USERGROUPRIGHTS]
		WHERE [USERGROUPID] = @UserGroupID;
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Search User Groups
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_SEARCHUSERGROUPS]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_SEARCHUSERGROUPS]
GO

CREATE PROCEDURE [dbo].[PROC_SEARCHUSERGROUPS](
	@UserGroupID UNIQUEIDENTIFIER NULL,
	@GroupName NVARCHAR(20),
	@Blocked BIT,
	@LanguageCode NVARCHAR(3),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	IF @LanguageCode = '' OR (@LanguageCode IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@LanguageCode parameter is empty!';
		RETURN @Result;
	END
	
	BEGIN
		SELECT [ug].[USERGROUPID] AS [USERGROUPID],
			[ug].[GROUPNAME] AS [GROUPNAME], 
			[ug].[GROUPDESCRIPTION] AS [GROUPDESCRIPTION], 
			[ug].[BLOCKED] AS [BLOCKED], 
			ISNULL([ugl].[TEXT], NULL) AS [TEXT]
		FROM [dbo].[USERGROUPS] AS [ug] 
		LEFT OUTER JOIN [USERGROUPLOCALIZE] [ugl] ON [ug].[USERGROUPLOCALIZEID] = [ugl].[USERGROUPLOCALIZEID] AND @LanguageCode = [ugl].[LANGUAGECODE]
		WHERE (@UserGroupID IS NULL OR [USERGROUPID] = @UserGroupID) AND (@GroupName IS NULL OR [GROUPNAME] = @GroupName)
		  AND (@Blocked IS NULL OR [BLOCKED] = @Blocked);
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

------------------------------------------
-- User Rights SPs

-- Create Procedure Add New User Right
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_ADDNEWUSERRIGHT]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_ADDNEWUSERRIGHT]
GO

CREATE PROCEDURE [dbo].[PROC_ADDNEWUSERRIGHT](
	@RightName NVARCHAR(20),
	@LanguageCode NVARCHAR(3),
	@RightNameTranslation NVARCHAR(60),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	DECLARE @RightID UNIQUEIDENTIFIER;
	DECLARE @RightTranslationID UNIQUEIDENTIFIER;

	SET @RightID = NEWID();
	SET @RightTranslationID = NEWID();

	IF @RightName = '' OR (@RightName IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightName parameter is empty!';
		RETURN @Result;
	END

    IF EXISTS(SELECT * FROM [dbo].[RIGHTS] WHERE [RIGHTNAME] = @RightName)
	BEGIN
		SELECT @Result = 2;
		RETURN @Result;
	END

	IF @LanguageCode = '' OR (@LanguageCode IS NULL)
	BEGIN
		SET @LanguageCode = 'ENG'; -- Change to: SET @LanguageCode = @DefaultLanguageCode;
	END
	
	IF @RightNameTranslation = '' OR (@RightNameTranslation IS NULL)
	BEGIN
		SET @RightNameTranslation = @RightName;
	END

	BEGIN
		INSERT INTO [dbo].[RIGHTLOCALIZE]
			   ([RIGHTLOCALIZEID]
			   ,[LANGUAGECODE]
			   ,[TEXT])
		 VALUES
			   (@RightTranslationID
			   ,@LanguageCode
			   ,@RightNameTranslation)
	END

	BEGIN
		INSERT INTO [dbo].[RIGHTS]
			   ([RIGHTID]
			   ,[RIGHTNAME]
			   ,[RIGHTLOCALIZEID])
		 VALUES
			   (@RightID
			   ,@RightName
			   ,@RightTranslationID)
	END
		
	SELECT [USERGROUPID] INTO #USERGROUPSTEMP FROM [dbo].[USERGROUPS];

	BEGIN
		INSERT INTO [dbo].[USERGROUPRIGHTS] 
			([USERGROUPID]
            ,[RIGHTID]
            ,[STATUS]) 
		SELECT [ug].[USERGROUPID], @RightID, 0 FROM #USERGROUPSTEMP [ug]
	END
	
	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Update User Right
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_UPDATEUSERRIGHT]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_UPDATEUSERRIGHT]
GO

CREATE PROCEDURE [dbo].[PROC_UPDATEUSERRIGHT](
	@RightID UNIQUEIDENTIFIER,
	@RightName NVARCHAR(20),
	@LanguageCode NVARCHAR(3),
	@RightNameTranslation NVARCHAR(60),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	DECLARE @RightTranslationID UNIQUEIDENTIFIER;
	DECLARE @UpdateTranslation BIT;

	IF @RightID = NULL OR @RightID = '00000000-0000-0000-0000-000000000000' OR @RightID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@RightID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[RIGHTS] WHERE [RIGHTID] = @RightID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END
		
	IF @RightNameTranslation = '' OR (@RightNameTranslation IS NULL)
	BEGIN
		SET @UpdateTranslation = 0;
	END
	ELSE
	BEGIN
		SET @UpdateTranslation = 1;
		IF @LanguageCode = '' OR (@LanguageCode IS NULL)
		BEGIN
			SELECT @Result = 0, @ErrorMessage = '@LanguageCode parameter is empty!';
			RETURN @Result;
		END
	END

	SELECT 
		[r].[RIGHTNAME] AS [RIGHTNAME],  
		[r].[RIGHTLOCALIZEID] AS [RIGHTLOCALIZEID],
		ISNULL([rl].[TEXT], '') AS [TEXT]
	INTO #USERRIGHTTABLETEMP
	FROM [dbo].[RIGHTS] AS [r] 
	LEFT OUTER JOIN [RIGHTLOCALIZE] [rl] ON [r].[RIGHTLOCALIZEID] = [rl].[RIGHTLOCALIZEID] AND @LanguageCode = [rl].[LANGUAGECODE]
	WHERE [r].[RIGHTID] = @RightID;

	IF @UpdateTranslation = 1
	BEGIN
		SET @RightTranslationID = (SELECT [RIGHTLOCALIZEID] FROM #USERRIGHTTABLETEMP);
	END

	IF @RightName = NULL OR @RightName = '' OR (@RightName IS NULL)
	BEGIN
		SET @RightName = (SELECT [RIGHTNAME] FROM #USERRIGHTTABLETEMP);
	END

	IF @UpdateTranslation = 1
	BEGIN
		UPDATE [dbo].[RIGHTLOCALIZE]
		SET [TEXT] = @RightNameTranslation
		WHERE [RIGHTLOCALIZEID] = @RightTranslationID AND [LANGUAGECODE] = @LanguageCode;
	END
		
	BEGIN
		UPDATE [dbo].[RIGHTS]
		SET [RIGHTNAME] = @RightName
		WHERE [RIGHTID] = @RightID
	END

	DROP TABLE #USERRIGHTTABLETEMP;
	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END

-- Create Procedure Get User Right
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_GETUSERRIGHT]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_GETUSERRIGHT]
GO

CREATE PROCEDURE [dbo].[PROC_GETUSERRIGHT](
	@RightID UNIQUEIDENTIFIER,
	@LanguageCode NVARCHAR(3),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	IF @RightID = NULL OR @RightID = '00000000-0000-0000-0000-000000000000' OR @RightID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@RightID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[RIGHTS] WHERE [RIGHTID] = @RightID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END

	IF @LanguageCode = '' OR (@LanguageCode IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@LanguageCode parameter is empty!';
		RETURN @Result;
	END

	BEGIN
		SELECT 
			[r].[RIGHTID] AS [RIGHTID],
			[r].[RIGHTNAME] AS [RIGHTNAME], 
			ISNULL([rl].[TEXT], NULL) AS [TEXT]
		FROM [dbo].[RIGHTS] AS [r] 
		LEFT OUTER JOIN [RIGHTLOCALIZE] [rl] ON [r].[RIGHTLOCALIZEID] = [rl].[RIGHTLOCALIZEID] AND @LanguageCode = [rl].[LANGUAGECODE]
		WHERE [r].[RIGHTID] = @RightID;
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Get User Right List
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_GETUSERRIGHTLIST]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_GETUSERRIGHTLIST]
GO

CREATE PROCEDURE [dbo].[PROC_GETUSERRIGHTLIST](
	@LanguageCode NVARCHAR(3),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	IF @LanguageCode = '' OR (@LanguageCode IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@LanguageCode parameter is empty!';
		RETURN @Result;
	END

	BEGIN
		SELECT 
			[r].[RIGHTID] AS [RIGHTID],
			[r].[RIGHTNAME] AS [RIGHTNAME], 
			ISNULL([rl].[TEXT], NULL) AS [TEXT]
		FROM [dbo].[RIGHTS] as [r] 
		LEFT OUTER JOIN [RIGHTLOCALIZE] [rl] ON [r].[RIGHTLOCALIZEID] = [rl].[RIGHTLOCALIZEID] AND @LanguageCode = [rl].[LANGUAGECODE]
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Delete User Right
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_DELETEUSERRIGHT]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_DELETEUSERRIGHT]
GO

CREATE PROCEDURE [dbo].[PROC_DELETEUSERRIGHT](
	@RightID UNIQUEIDENTIFIER,
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	DECLARE @RightLocalizeID UNIQUEIDENTIFIER;

	IF @RightID = NULL OR @RightID = '00000000-0000-0000-0000-000000000000' OR @RightID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@RightID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[RIGHTS] WHERE [RIGHTID] = @RightID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END
	
	SET @RightLocalizeID = (SELECT [RIGHTLOCALIZEID] FROM [dbo].[RIGHTS] WHERE [RIGHTID] = @RightID);
	
	BEGIN
		DELETE FROM [dbo].[RIGHTLOCALIZE]
		WHERE [RIGHTLOCALIZEID] = @RightLocalizeID;
	END

	BEGIN
		DELETE FROM [dbo].[RIGHTS]
		WHERE [RIGHTID] = @RightID;
	END
	
	BEGIN
		DELETE FROM [dbo].[USERRIGHTS]
		WHERE [RIGHTID] = @RightID;
	END
	
	BEGIN
		DELETE FROM [dbo].[USERGROUPRIGHTS]
		WHERE [RIGHTID] = @RightID;
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Search Rights
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_SEARCHRIGHTS]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_SEARCHRIGHTS]
GO

CREATE PROCEDURE [dbo].[PROC_SEARCHRIGHTS](
	@RightID UNIQUEIDENTIFIER NULL,
	@RightName NVARCHAR(20),
	@LanguageCode NVARCHAR(3),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	IF @LanguageCode = '' OR (@LanguageCode IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@LanguageCode parameter is empty!';
		RETURN @Result;
	END
	
	BEGIN
		SELECT [r].[RIGHTID] AS [RIGHTID],
			[r].[RIGHTNAME] AS [RIGHTNAME],
			ISNULL([rl].[TEXT], NULL) AS [TEXT]
		FROM [dbo].[RIGHTS] AS [r] 
		LEFT OUTER JOIN [RIGHTLOCALIZE] [rl] ON [r].[RIGHTLOCALIZEID] = [rl].[RIGHTLOCALIZEID] AND @LanguageCode = [rl].[LANGUAGECODE]
		WHERE (@RightID IS NULL OR [RIGHTID] = @RightID) AND ([RIGHTNAME] IS NULL OR [RIGHTNAME] = @RightName);
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Change User Right
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_CHANGEUSERRIGHT]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_CHANGEUSERRIGHT]
GO

CREATE PROCEDURE [dbo].[PROC_CHANGEUSERRIGHT](
	@UserID UNIQUEIDENTIFIER,
	@RightID UNIQUEIDENTIFIER,
	@RightStatus TINYINT, -- User Groups can have Status 'InheritFromGroup(2)', 'Allow(1)' or 'Block(0)'. Therefore type is TINYINT
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	IF @UserID = NULL OR @UserID = '00000000-0000-0000-0000-000000000000' OR @UserID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserID parameter is empty!';
		RETURN @Result;
	END
	
	IF @RightID = NULL OR @RightID = '00000000-0000-0000-0000-000000000000' OR @RightID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@RightID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightID parameter is empty!';
		RETURN @Result;
	END

	IF @RightStatus = NULL OR (@RightStatus IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightStatus parameter is empty!';
		RETURN @Result;
	END

	IF @RightStatus > 2
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightStatus parameter can be only 0, 1 or 2';
		RETURN @Result;
	END
	
	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERS] WHERE [USERID] = @UserID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END
	
	IF NOT EXISTS (SELECT 1 FROM [dbo].[RIGHTS] WHERE [RIGHTID] = @RightID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END
		
	IF @RightStatus = 2
	BEGIN
		IF EXISTS (SELECT 1 FROM [dbo].[USERRIGHTS] WHERE [USERID] = @UserID AND [RIGHTID] = @RightID)
		BEGIN
			DELETE FROM [dbo].[USERRIGHTS]
			WHERE [USERID] = @UserID AND [RIGHTID] = @RightID
		END
	END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM [dbo].[USERRIGHTS] WHERE [USERID] = @UserID AND [RIGHTID] = @RightID)
		BEGIN
			INSERT INTO [dbo].[USERRIGHTS]
			   ([USERID]
			   ,[RIGHTID]
			   ,[STATUS])
			VALUES
			   (@UserID
			   ,@RightID
			   ,@RightStatus)
		END
		ELSE
		BEGIN
			UPDATE [dbo].[USERRIGHTS]
			SET [STATUS] = @RightStatus
			WHERE [USERID] = @UserID AND [RIGHTID] = @RightID
		END
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Change User Group Right
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_CHANGEUSERGROUPRIGHT]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_CHANGEUSERGROUPRIGHT]
GO

CREATE PROCEDURE [dbo].[PROC_CHANGEUSERGROUPRIGHT](
	@UserGroupID UNIQUEIDENTIFIER,
	@RightID UNIQUEIDENTIFIER,
	@RightStatus BIT, -- User Groups can have Status 'Allow(1)' or 'Block(0)' only!  Therefore type is BIT
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY	
	IF @UserGroupID = NULL OR @UserGroupID = '00000000-0000-0000-0000-000000000000' OR @UserGroupID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserGroupID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserGroupID parameter is empty!';
		RETURN @Result;
	END
	
	IF @RightID = NULL OR @RightID = '00000000-0000-0000-0000-000000000000' OR @RightID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@RightID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightID parameter is empty!';
		RETURN @Result;
	END

	IF @RightStatus = NULL OR (@RightStatus IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightStatus parameter is empty!';
		RETURN @Result;
	END
	
	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERGROUPS] WHERE [USERGROUPID] = @UserGroupID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END
		
	IF NOT EXISTS (SELECT 1 FROM [dbo].[RIGHTS] WHERE [RIGHTID] = @RightID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END
	
	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERGROUPRIGHTS] WHERE [USERGROUPID] = @UserGroupID AND [RIGHTID] = @RightID)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = 'There are no Right with ID[' + CONVERT(VARCHAR(38), @RightID) + '] in the Group with ID[' + CONVERT(VARCHAR(38), @UserGroupID) + ']';
		RETURN @Result;
	END
				
	BEGIN
		UPDATE [dbo].[USERGROUPRIGHTS]
		SET [STATUS] = @RightStatus
		WHERE [USERGROUPID] = @UserGroupID AND [RIGHTID] = @RightID
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Get User Right Status
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_GETUSERRIGHTSTATUS]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_GETUSERRIGHTSTATUS]
GO

CREATE PROCEDURE [dbo].[PROC_GETUSERRIGHTSTATUS](
	@UserID UNIQUEIDENTIFIER,
	@RightID UNIQUEIDENTIFIER,
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	IF @UserID = NULL OR @UserID = '00000000-0000-0000-0000-000000000000' OR @UserID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserID parameter is empty!';
		RETURN @Result;
	END

	IF @RightID = NULL OR @RightID = '00000000-0000-0000-0000-000000000000' OR @RightID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@RightID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@RightID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERS] WHERE [USERID] = @UserID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END
	
	IF NOT EXISTS (SELECT 1 FROM [dbo].[RIGHTS] WHERE [RIGHTID] = @RightID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END
	
	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERRIGHTS] WHERE [USERID] = @UserID AND [RIGHTID] = @RightID)
	BEGIN
		DECLARE @UserGroupID UNIQUEIDENTIFIER;		
		SET @UserGroupID = (SELECT [USERGROUPID] FROM [dbo].[USERS] WHERE [USERID] = @UserID);
		
		SELECT [STATUS]
		FROM [dbo].[USERGROUPRIGHTS]
		WHERE [USERGROUPID] = @UserGroupID AND [RIGHTID] = @RightID;
	END
	ELSE
	BEGIN
		SELECT [STATUS]
		FROM [dbo].[USERRIGHTS]
		WHERE [USERID] = @UserID AND [RIGHTID] = @RightID
	END

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO

-- Create Procedure Get User Rights
IF EXISTS (SELECT * FROM dbo.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[PROC_GETUSERRIGHTS]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PROC_GETUSERRIGHTS]
GO

CREATE PROCEDURE [dbo].[PROC_GETUSERRIGHTS](
	@UserID UNIQUEIDENTIFIER,
	@LanguageCode NVARCHAR(3),
	@Result TINYINT OUT, 
	@ErrorNumber INT OUT, 
	@ErrorMessage NVARCHAR(1024) OUT)
AS
BEGIN 
BEGIN TRY
	DECLARE @UserGroupID UNIQUEIDENTIFIER;
	
	IF @UserID = NULL OR @UserID = '00000000-0000-0000-0000-000000000000' OR @UserID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR (@UserID IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@UserID parameter is empty!';
		RETURN @Result;
	END

	IF NOT EXISTS (SELECT 1 FROM [dbo].[USERS] WHERE [USERID] = @UserID)
	BEGIN
		SELECT @Result = 3;
		RETURN @Result;
	END

	IF @LanguageCode = '' OR (@LanguageCode IS NULL)
	BEGIN
		SELECT @Result = 0, @ErrorMessage = '@LanguageCode parameter is empty!';
		RETURN @Result;
	END

	SELECT 
        [ur].[RIGHTID] AS [RIGHTID],
        [ur].[STATUS] AS [STATUS],
		ISNULL([r].[RIGHTNAME], NULL) AS [RIGHTNAME],
		ISNULL([rl].[TEXT], '') AS [TEXT]
	INTO #USERRIGHTSTEMP
	FROM [dbo].[USERRIGHTS] AS [ur] 
	LEFT OUTER JOIN [dbo].[RIGHTS] [r] ON [ur].[RIGHTID] = [r].[RIGHTID]
	LEFT OUTER JOIN [dbo].[RIGHTLOCALIZE] [rl] ON [r].[RIGHTLOCALIZEID] = [rl].[RIGHTLOCALIZEID] AND @LanguageCode = [rl].[LANGUAGECODE]
	WHERE [ur].[USERID] = @UserID;
				
	SET @UserGroupID = (SELECT [USERGROUPID] FROM [dbo].[USERS] WHERE [USERID] = @UserID);

	SELECT 
        [ugr].[RIGHTID] AS [RIGHTID],
        2 AS [STATUS],
		ISNULL([r].[RIGHTNAME], NULL) AS [RIGHTNAME],
		ISNULL([rl].[TEXT], '') AS [TEXT]
	INTO #USERGROUPRIGHTSTEMP
	FROM [dbo].[USERGROUPRIGHTS] AS [ugr] 
	LEFT OUTER JOIN [dbo].[RIGHTS] [r] ON [ugr].[RIGHTID] = [r].[RIGHTID]
	LEFT OUTER JOIN [dbo].[RIGHTLOCALIZE] [rl] ON [r].[RIGHTLOCALIZEID] = [rl].[RIGHTLOCALIZEID] AND @LanguageCode = [rl].[LANGUAGECODE]
	WHERE [ugr].[USERGROUPID] = @UserGroupID;

	SELECT [RIGHTID], [STATUS], [RIGHTNAME], [TEXT] FROM #USERGROUPRIGHTSTEMP
	WHERE [RIGHTID] NOT IN (SELECT [RIGHTID] FROM #USERRIGHTSTEMP)
	UNION 
	SELECT [RIGHTID], [STATUS], [RIGHTNAME], [TEXT] FROM #USERRIGHTSTEMP
	ORDER BY [RIGHTID]

	SET @Result = 1;
	RETURN @Result	
END TRY
BEGIN CATCH
	SELECT @Result = 0, @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE();
END CATCH
END
GO