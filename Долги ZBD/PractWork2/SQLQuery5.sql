CREATE FUNCTION GetTodayMoviesByCinema
(
    @cinemaName NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        m.MovieId,
        m.[Name],
        s.StartDate,
        h.HallNumber
    FROM 
        Hall h
        INNER JOIN Session s ON h.HallId = s.HallId
        INNER JOIN Movie m ON s.MovieId = m.MovieId
    WHERE 
        h.Cinema = @cinemaName
        AND CAST(s.StartDate AS DATE) = CAST(GETDATE() AS DATE)
);

SELECT * FROM dbo.GetTodayMoviesByCinema('Титан Арена');