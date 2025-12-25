CREATE VIEW SessionsInfo AS
SELECT 
    s.SessionId AS [Код сеанса],
    m.Name AS [Название фильма],
    h.Cinema AS [Кинотеатр],
    'Зал ' || CAST(h.HallNumber AS VARCHAR) AS [Зал],
    s.Price AS [Цена],
    s.StartDate AS [Дата и время начала],
    -- Вычисляем общее количество мест, преобразуя множители к INT
    CAST(h.RowsAmount AS INT) * CAST(h.SeatsAmount AS INT) AS [Общее количество мест]
FROM 
Session s
    INNER JOIN Movie m ON s.MovieId = m.MovieId  
    INNER JOIN Hall h ON s.HallId = h.HallId;  



SELECT * FROM SessionsInfo;
