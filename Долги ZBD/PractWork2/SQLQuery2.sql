
CREATE FUNCTION GetMoviesByGenre
(
    @genreName NVARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        m.MovieId,
        m.Name AS MovieName,
        STRING_AGG(g.Name, ', ') WITHIN GROUP (ORDER BY g.Name) AS GenreList
    FROM Movie m
    INNER JOIN GenreMovie gm ON m.MovieId = gm.MovieId
    INNER JOIN Genre g ON gm.GenreId = g.GenreId
    WHERE m.MovieId IN (
        SELECT MovieId
        FROM GenreMovie
        WHERE GenreId = (SELECT GenreId FROM Genre WHERE Name = @GenreName)
    )
    GROUP BY m.MovieId, m.Name
);

SELECT * FROM dbo.GetMoviesByGenre('Драма');