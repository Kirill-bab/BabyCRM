SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
