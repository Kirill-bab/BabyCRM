SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Filial].[Filial_GetAll]
AS
BEGIN
	SELECT  f.Id, f.Name, f.PhoneNumber, f.Address, f.CreatedDate, f.CreatedBy, f.DataVersion, 
	c.Id, c.FirstName, c.LastName, c.Birthday, c.ParentFullName, c.PhoneNumber, c.EmailAddress, c.SocialNetworks, c.CreatedDate, c.CreatedBy, c.DataVersion, c.FilialId
    FROM [Filial].Filial f LEFT JOIN [Client].Client c on f.Id = c.FilialId
END
GO

CREATE PROCEDURE [Filial].[Filial_Get]
	@FilialId INT
AS
BEGIN
	SELECT  f.Id, f.Name, f.PhoneNumber, f.Address, f.CreatedDate, f.CreatedBy, f.DataVersion, 
	c.Id, c.FirstName, c.LastName, c.Birthday, c.ParentFullName, c.PhoneNumber, c.EmailAddress, c.SocialNetworks, c.CreatedDate, c.CreatedBy, c.DataVersion, c.FilialId
    FROM [Filial].Filial f LEFT JOIN [Client].Client c on f.Id = c.FilialId
    WHERE f.Id = @FilialId
END
GO

CREATE PROCEDURE [Filial].[Filial_Insert]
	@Name NVARCHAR(30),
	@PhoneNumber NVARCHAR(15),
	@Address NVARCHAR(50),
	@CreatedBy NVARCHAR(30)
AS
BEGIN
	DECLARE @CreatedDate DATETIME = GETDATE()
	DECLARE @DataVersion INT = 0
	INSERT INTO [Filial].Filial
     VALUES(
            @Name
           ,@PhoneNumber
           ,@Address
           ,@CreatedDate
           ,@CreatedBy
           ,@DataVersion
		   )
END
GO

CREATE PROCEDURE [Filial].[Filial_Update]
	@Id INT,
	@Name NVARCHAR(30),
	@PhoneNumber NVARCHAR(15),
	@Address NVARCHAR(50)
AS
BEGIN
	UPDATE [Filial].Filial
     SET
            Name = @Name
           ,PhoneNumber = @PhoneNumber
           ,Address = @Address
           ,DataVersion = DataVersion + 1
	WHERE Id = @Id;
END
GO

CREATE PROCEDURE [Filial].[Filial_Delete]
	@Id INT
AS
BEGIN
	DELETE
	FROM [Filial].Filial
	WHERE Id = @Id;
END;
