using System;
using System.Collections.Generic;
using EFAndLoggingExample.Models;
using log4net;

namespace EFAndLoggingExample.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private MovieContext _context;
        private ILog _log;
        public MovieRepository(MovieContext ctx, ILog log)
        {
            _context = ctx;
            _log = log;
        }

        public IEnumerable<Movie> GetMovieByName(string name)
        {
            throw new NotImplementedException();
        }

        public void InsertMovie(string name)
        {
            _context.Movies.Add(new Movie() {Name = name});
            _context.SaveChanges();
            _log.InfoFormat("Movie with name {0} created!", name);
        }

        public void UpdateMovie(Movie movie)
        {
            var movieToEdit = _context.Movies.Find(movie.Id);
            if (movieToEdit != null)
            {
                string oldName = movieToEdit.Name;
                movieToEdit.Name = movie.Name;
                _log.InfoFormat("Movie with id {0} updated name from {1} to {2}!", movie.Id, oldName, movie.Name);
                _context.SaveChanges();
            }
        }

        public void DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}