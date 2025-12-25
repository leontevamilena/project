CREATE VIEW TodaySessions AS
SELECT 
    s.SessionId AS [Код сеанса],
    m.Name AS [Название фильма],
    h.Cinema AS [Кинотеатр],
    'Зал ' + CAST(h.HallId AS VARCHAR) AS [Зал],
    s.Price AS [Цена],
    -- Время начала в формате HH:mm
    FORMAT(s.StartDate, 'HH:mm') AS [Время начала],
    -- Время окончания (начало + длительность фильма)
    FORMAT(DATEADD(minute, m.Duration, s.StartDate), 'HH:mm') AS [Время конца],
    -- Длительность фильма в формате "N ч M м"
    CAST(m.Duration / 60 AS VARCHAR) + ' ч ' + 
    CAST(m.Duration % 60 AS VARCHAR) + ' м' AS [Длительность]
FROM 
    Session s
    INNER JOIN Movie m ON s.MovieId = m.MovieId
    INNER JOIN Hall h ON s.HallId = h.HallId
WHERE 
    -- Сеансы сегодняшнего дня
    CAST(s.StartDate AS DATE) = CAST(GETDATE() AS DATE)
    -- Исключаем сеансы, которые уже начались
    AND s.StartDate > GETDATE();


SELECT * FROM TodaySessions;

