using System;
using System.Linq;
using System.Web.Mvc;
using EFAndLoggingExample.Models;
using EFAndLoggingExample.Repositories;

namespace EFAndLoggingExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepository _repository;

        public HomeController(IMovieRepository repository)
        {
            _repository = repository;
        }

        // This action will return a page that displays all movies in the system and allows
        // adding, editing, and deleting of movies.
        public ActionResult Index()
        {
            var movies = _repository.GetMovies().ToList();  // In a production system this should probably be a view model not an entity
            return View(movies);
        }

        // This action receives the the movie to insert, does the insert,
        // and returns JSON of the newly inserted movie.
        public ActionResult Insert(string movie)
        {
            var id = _repository.InsertMovie(movie);
            return Json(new {Id = id, Name = movie});
        }

        // This action deletes the specified movie
        // and returns JSON of the deleted id
        public ActionResult Delete(string id)
        {
            _repository.DeleteMovie(Int32.Parse(id));
            return Json(new {Id = id});
        }

        // This action update the specified movie with
        // the specified name and returns the id and movie name
        public ActionResult Update(string id, string movie)
        {
            _repository.UpdateMovie(new Movie(){Id = Int32.Parse(id), Name = movie});
            return Json(new {Id = id, name = movie});
        }
    }
}