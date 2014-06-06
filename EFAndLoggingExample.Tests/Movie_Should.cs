using NUnit.Framework;

namespace EFAndLoggingExample.Tests
{
    [TestFixture]
    public class Movie_Should
    {
        public void Create_A_Movie()
        {
            var db = new MovieContext();

            Movie movie = new Movie() {Name = "Star Wars"};
            db.Movies.Add(movie);
            db.SaveChanges();

            var query = from m in db.Movies
                where m.Name == "Star Wars"
                select m;

            Assert.AreEqual(1,query.count());
        }
    }
}
