using System.Data.Entity;
using System.Linq;
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
    }
}
