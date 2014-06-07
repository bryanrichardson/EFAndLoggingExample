using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EFAndLoggingExample.Models;

namespace EFAndLoggingExample.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        IEnumerable<Movie> GetMovieByName(string name);
        int InsertMovie(string name);
        void UpdateMovie(Movie movie);
        void DeleteMovie(int id);
    }
}