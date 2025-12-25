CREATE VIEW MoviesInfo AS
SELECT 
    m.MovieId AS [Код],
    m.Name AS [Название],
    m.StartRental AS [Год выхода],
    -- Форматирование длительности в формат «N ч М м»
    CASE 
        WHEN m.Duration IS NULL THEN 'Длительность не указана'
        ELSE 
            CAST(m.Duration / 60 AS VARCHAR) + ' ч ' + 
            CAST(m.Duration % 60 AS VARCHAR) + ' м'
    END AS [Длительность],
    -- Объединение жанров через STRING_AGG
    ISNULL(
        (SELECT STRING_AGG(g.Name, ', ')
         FROM GenreMovie gm
         JOIN Genre g ON gm.GenreId = g.GenreId
         WHERE gm.MovieId = m.MovieId),
        'Жанр не указан'
    ) AS [Жанр],
    ISNULL(m.Description, 'Описание отсутствует') AS [Описание],
    -- Начало проката - минимальная дата сеанса для фильма
    (SELECT MIN(s.StartDate)
     FROM Session s
     WHERE s.MovieId = m.MovieId) AS [Начало проката]
FROM 
    Movie m;



SELECT * FROM MoviesInfo;