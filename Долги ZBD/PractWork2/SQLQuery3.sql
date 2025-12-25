CREATE PROCEDURE AddTicketWithVisitor
    @sessionId INT,
    @phone NVARCHAR(20),
    @row NVARCHAR(10),
    @seat NVARCHAR(10),
    @newTicketId INT OUTPUT
AS
BEGIN

    DECLARE @visitorId INT;

    -- Проверяем, есть ли посетитель с таким номером телефона
    SELECT @visitorId = VisitorId FROM Visitor WHERE Phone = @phone;
    INSERT INTO Ticket (SessionId, VisitorId, [Row], Seat)
    VALUES (@sessionId, @visitorId, @row, @seat);
    SET @newTicketId = SCOPE_IDENTITY();
END

DECLARE @ticketId INT;

EXEC AddTicketWithVisitor 
    @phone = '9115821681',
    @sessionId = 3,
    @row = '15',
    @seat = '10',
    @newTicketId = @ticketId OUTPUT;

SELECT @ticketId AS NewTicket;