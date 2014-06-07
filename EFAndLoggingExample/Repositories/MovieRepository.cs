using System;
using System.Collections.Generic;
using System.Linq;
using EFAndLoggingExample.Mail;
using EFAndLoggingExample.Models;
using log4net;

namespace EFAndLoggingExample.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private MovieContext _context;
        private ILog _log;
        private readonly IMailer _mailer;

        public MovieRepository(MovieContext ctx, ILog log, IMailer mailer)
        {
            _context = ctx;
            _log = log;
            _mailer = mailer;
        }

        public IEnumerable<Movie> GetMovies()
        {
            var movies = _context.Movies;
            _log.InfoFormat("Query for all movies occurred.");
            return movies;
        }

        public IEnumerable<Movie> GetMovieByName(string name)
        {
            var movies = _context.Movies.Where(x => x.Name == name);
            _log.InfoFormat("Query for movies with a name of {0} returned a count of {1}", name, movies.Count());
            return movies;
        }

        public int InsertMovie(string name)
        {
            _context.Movies.Add(new Movie() {Name = name});
            _context.SaveChanges();
            _log.InfoFormat("Movie with name {0} created!", name);

            var movie = _context.Movies.Where(x => x.Name==name).OrderByDescending(x => x.Id).First();
            return movie.Id;
        }

        public void UpdateMovie(Movie movie)
        {
            var movieToEdit = _context.Movies.Find(movie.Id);
            if (movieToEdit != null)
            {
                string oldName = movieToEdit.Name;
                movieToEdit.Name = movie.Name;
                _context.SaveChanges();
                _log.InfoFormat("Movie with id {0} updated name from {1} to {2}!", movie.Id, oldName, movie.Name);
            }
        }

        public void DeleteMovie(int id)
        {
            var movieToDelete = _context.Movies.Find(id);
            if (movieToDelete != null)
            {
                _context.Movies.Remove(movieToDelete);
                _context.SaveChanges();
                _log.InfoFormat("Movie with id {0} and name {1} was deleted!", movieToDelete.Id, movieToDelete.Name);
                _mailer.SendEmail("from@someplace.com", "to@someotherplace.com", string.Format("Movie with name {0} was deleted!", movieToDelete.Name));
            }
        }
    }
}