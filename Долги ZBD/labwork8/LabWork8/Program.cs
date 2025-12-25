using LabWork8;
using LabWork8.Models;
using LabWork8.Repository;
using System.Data;

//Task 1 
DatabaseContext databaseContext = new("mssql", "ispp3103", "ispp3103", "3103");
using IDbConnection connection = databaseContext.CreateConnection();

connection.Open();

VisitorRepository visitorRepository = new(databaseContext);
GenreRepository genreRepository = new(databaseContext);

//Task 3
Visitor? visitor = await visitorRepository.GetByIdAsync(5);
Genre? genre = await genreRepository.GetByIdAsync(2);
Console.WriteLine($"Почта пользователя:{visitor.Email}, Жанр:{genre.Name}");

var visitors = await visitorRepository.GetAllAsync();
var genres = await genreRepository.GetAllAsync();
