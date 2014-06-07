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
            throw new NotImplementedException();
        }

        public void UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
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