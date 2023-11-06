SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Client].[Client_GetAll]
AS
BEGIN
	SELECT Id, FirstName, LastName, Birthday, ParentFullName, PhoneNumber, EmailAddress, SocialNetworks, CreatedDate, CreatedBy, DataVersion, FilialId
	FROM [Client].[Client];
END
GO

CREATE PROCEDURE [Client].[Client_Get]
	@Id int
AS
BEGIN
	SELECT Id, FirstName, LastName, Birthday, ParentFullName, PhoneNumber, EmailAddress, SocialNetworks, CreatedDate, CreatedBy, DataVersion, FilialId 
	FROM [Client].Client c
	WHERE c.Id = @Id;
END
GO

CREATE PROCEDURE [Client].[Client_Insert]
	@FirstName NVARCHAR(30),
	@LastName NVARCHAR(30),
	@Birthday DATETIME,
	@ParentFullName NVARCHAR(50),
	@PhoneNumber NVARCHAR(15),
	@EmailAddress NVARCHAR(50),
	@SocialNetworks NVARCHAR(100),
	@CreatedBy NVARCHAR(30),
	@FilialId INT
AS
BEGIN
	DECLARE @CreatedDate DATETIME = GETDATE()
	DECLARE @DataVersion INT = 0
	INSERT INTO [Client].[Client]
     VALUES
           (@FirstName
           ,@LastName
           ,@Birthday
           ,@ParentFullName
           ,@PhoneNumber
           ,@EmailAddress
           ,@SocialNetworks
           ,@CreatedDate
           ,@CreatedBy
           ,@DataVersion
		   ,@FilialId);
END
GO

CREATE PROCEDURE [Client].[Client_Update]
	@Id int,
	@FirstName NVARCHAR(30),
	@LastName NVARCHAR(30),
	@Birthday DATETIME,
	@ParentFullName NVARCHAR(50),
	@PhoneNumber NVARCHAR(15),
	@EmailAddress NVARCHAR(50),
	@SocialNetworks NVARCHAR(100),
	@FilialId INT
AS
BEGIN
	UPDATE [Client].[Client]
     SET
            FirstName = @FirstName
           ,LastName = @LastName
           ,Birthday = @Birthday
           ,ParentFullName = @ParentFullName
           ,PhoneNumber = @PhoneNumber
           ,EmailAddress = @EmailAddress
           ,SocialNetworks = @SocialNetworks
           ,DataVersion = DataVersion + 1
		   ,FilialId = @FilialId
	WHERE Id = @Id;
END
GO

CREATE PROCEDURE [Client].[Client_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM [Client].Client 
	WHERE Id = @Id;
END;