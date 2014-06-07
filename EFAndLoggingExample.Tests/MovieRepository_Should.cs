using System.Data.Entity;
using System.Linq;
using EFAndLoggingExample.Models;
using EFAndLoggingExample.Repositories;
using log4net;
using Moq;
using NUnit.Framework;

namespace EFAndLoggingExample.Tests
{
    [TestFixture]
    public class MovieRepository_Should
    {
        [Test]
        public void Log_When_Movie_Is_Created()
        {
            var contextMock = new Mock<MovieContext>();
            var logMock = new Mock<ILog>();
            var repository = new MovieRepository(contextMock.Object,logMock.Object);

            logMock.Setup(x => x.InfoFormat("Movie with name {0} created!", "Star Wars"));
            contextMock.Setup(x => x.Movies.Add(new Movie() {Name = "Star Wars"}));
            contextMock.Setup(x => x.SaveChanges());

            repository.InsertMovie("Star Wars");

            logMock.VerifyAll();
        }

        [Test]
        public void Log_When_Movie_Is_Updated()
        {
            var contextMock = new Mock<MovieContext>();
            var logMock = new Mock<ILog>();
            var repository = new MovieRepository(contextMock.Object, logMock.Object);
            var movieToChange = new Movie() {Id = 1, Name = "Star Wars"};

            logMock.Setup(x => x.InfoFormat("Movie with id {0} updated name from {1} to {2}!", 1, "Star Wars", "Lord of the Rings"));
            contextMock.Setup(x => x.Movies.Find(1)).Returns(movieToChange);
            contextMock.Setup(x => x.SaveChanges());

            repository.UpdateMovie(new Movie(){Id = 1, Name = "Lord of the Rings"});

            logMock.VerifyAll();
            contextMock.VerifyAll();
            // TODO: need to verify that the name was actually changed
        }
    }
}
