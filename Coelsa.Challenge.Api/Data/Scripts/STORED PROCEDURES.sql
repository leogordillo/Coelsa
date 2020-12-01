CREATE PROCEDURE ContactInsert
	@FirstName VARCHAR(30),
	@LastName VARCHAR(30),
	@Company VARCHAR(75),
	@Email VARCHAR(75),
	@PhoneNumber VARCHAR(20)
AS

	INSERT INTO Contacts (FirstName, LastName, Company, Email, PhoneNumber)
	VALUES (@FirstName, @LastName, @Company, @Email, @PhoneNumber)

GO

CREATE PROCEDURE ContactDelete
	@Id int
AS
	DELETE Contacts WHERE Id = @Id
GO

CREATE PROCEDURE ContactUpdate
	@Id int,
	@FirstName VARCHAR(30),
	@LastName VARCHAR(30),
	@Company VARCHAR(75),
	@Email VARCHAR(75),
	@PhoneNumber VARCHAR(20)
AS

	UPDATE Contacts SET FirstName = @FirstName, LastName = @LastName, Company = @Company, Email = @Email, PhoneNumber = @PhoneNumber
	WHERE Id = @Id
GO


CREATE PROCEDURE ContactGetByCompany
	@CompanyName VARCHAR(75),
	@PageNumber INT,
	@RowsOfPage INT

AS
	SELECT * FROM Contacts WITH (NOLOCK) WHERE
	Company LIKE '%' + @CompanyName + '%' 
	ORDER BY Company DESC
	OFFSET (@PageNumber-1)*@RowsOfPage ROWS
	FETCH NEXT @RowsOfPage ROWS ONLY
GO
