--task 7

CREATE PROCEDURE dbo.GetTicketsByPhone 
    @phone char(10)
AS
    SELECT Ticket.*
	FROM Visitor INNER JOIN
	Ticket ON Visitor.VisitorId = Ticket.VisitorId
	WHERE Visitor.Phone = @phone;

--task 8

CREATE PROCEDURE dbo.AddVisitor 
    @phone char(10),
    @id int OUTPUT 
AS
	BEGIN
	INSERT INTO Visitor(Phone)
	VALUES (@phone)
	SET @id = SCOPE_IDENTITY()
	END

--task 9

CREATE FUNCTION [dbo].[GetSessionsByFilmId]
(
    @id int
)
RETURNS TABLE AS RETURN
	SELECT * 
	FROM [Session]
	WHERE FilmId = @id

