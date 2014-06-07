using System.Data.Entity;
using System.Linq;
using System.Threading;
using EFAndLoggingExample.Models;
using NUnit.Framework;

namespace EFAndLoggingExample.Tests
{
    [TestFixture]
    public class Movie_Should
    {
        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<MovieContext>());
        }

        [Test]
        public void Create_A_Movie()
        {
            var db = new MovieContext();

            var movie = new Movie() {Name = "Star Wars"};
            db.Movies.Add(movie);
            db.SaveChanges();

            var query = db.Movies.Where(m => m.Name == "Star Wars");

            Assert.AreEqual(1,query.Count());
        }

        [Test]
        public void Update_A_Movie()
        {
            // Create movie to update
            var db = new MovieContext();

            var movie = new Movie() { Name = "Star Wars" };
            db.Movies.Add(movie);
            db.SaveChanges();

            // Update movie
            var movieToChange = db.Movies.First(m => m.Name == "Star Wars");
            movieToChange.Name = "Lord of the Rings";
            db.SaveChanges();
            
            // Setup Query
            var query = db.Movies.Where(m => m.Name == "Lord of the Rings");

            // Test return values
            Assert.AreEqual(1, query.Count());
        }

        [Test]
        public void Delete_A_Movie()
        {
            using (var db = new MovieContext())
            {
                // Create movie to delete
                var movie = new Movie() {Name = "Star Wars"};
                db.Movies.Add(movie);
                db.SaveChanges();

                // Delete movie
                var movieToDelete = db.Movies.First(m => m.Name == "Star Wars");
                db.Movies.Remove(movieToDelete);
                db.SaveChanges();
                
                // Test return values
                Assert.AreEqual(EntityState.Detached, db.Entry(movieToDelete).State);
            }
        }

        [Test]
        public void Retrieve_A_Movie()
        {
            using (var db = new MovieContext())
            {
                // Create movie to retrieve
                var movie = new Movie() { Name = "Star Wars" };
                db.Movies.Add(movie);
                db.SaveChanges();

                // Retrieve movie
                var movieToDelete = db.Movies.First(m => m.Name == "Star Wars");

                // Test return values
                Assert.AreEqual("Star Wars", movieToDelete.Name);
            }
        }
    }
}
