
CREATE FUNCTION GetVisitorPoints
(
    @visitorId int
)
RETURNS INT
AS
BEGIN
    
   DECLARE @TotalMinutes INT;

    SELECT @TotalMinutes = ISNULL(SUM(m.Duration), 0)
    FROM Ticket t
    JOIN Session s ON t.SessionId = s.SessionId
    JOIN Movie m ON s.MovieId = m.MovieId
    WHERE t.VisitorId = @visitorId;

    RETURN @totalMinutes;
END


SELECT
    v.VisitorId,
    dbo.GetVisitorPoints(v.VisitorId) AS AccumulatedMinutes
FROM Visitor v;

    

