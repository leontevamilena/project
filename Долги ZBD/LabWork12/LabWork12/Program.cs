// See https://aka.ms/new-console-template for more information
using LabWork12.Contexts;
using LabWork12.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using var context = new MovieDbContext();

var movieService = new MovieService(context);

var sortedMovie = await movieService.GetMoviesAsync("Year");
sortedMovie.ForEach(m => Console.WriteLine($"{m.Name} - {m.Year}"));

var sessionService = new SessionService(context);

Console.WriteLine(await sessionService.IncreaseSessionPriceByHallIdAsync(11, 250m));

var genres = await movieService.GetGenresMoviesByIdAsync(1);
genres.ForEach(Console.WriteLine);

Console.WriteLine();

var movies = await movieService.GetMoviesByLetterRangeAsync('а', 'д');
movies.ForEach(m => Console.WriteLine($"{m.Name} - {m.MovieId}"));

Console.WriteLine();

var sessionDto = await sessionService.GetPriceInfoByMovieIdAsync(1);
Console.WriteLine($"Max - {sessionDto.MaxPrice} | Min - {sessionDto.MinPrice} | Average - {sessionDto.AveragePrice}");

var sessionTime = await sessionService.GetSessionDateAndTimeByTicketIdAsync(1);
Console.WriteLine(sessionTime.ToString());

var ticketService = new TicketService(context);

var tickets = await ticketService.GetTicketsByVisitorPhoneAsync("7892113445");
tickets.ForEach(ticket => Console.WriteLine($"Id - {ticket.TicketId} | Seat - {ticket.Seat} | VisitorId - {ticket.VisitorId}"));

var visitorService = new VisitorService(context);

Console.WriteLine($"Id - {await visitorService.AddVisitorByPhoneAsync("1457123000")}");

var sessions = await sessionService.GetSessionsByMovieIdAsync(1);
sessions.ForEach(session => Console.WriteLine(@$"MovieId - {session.MovieId} | Price - {session.Price} | SessionId - {session.SessionId} | StartDate - {session.StartDate}"));