using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using EFAndLoggingExample.Mail;
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
            var mailerMock = new Mock<IMailer>();
            var repository = new MovieRepository(contextMock.Object, logMock.Object, mailerMock.Object);

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
            var mailerMock = new Mock<IMailer>();
            var repository = new MovieRepository(contextMock.Object, logMock.Object, mailerMock.Object);
            var movieToChange = new Movie() {Id = 1, Name = "Star Wars"};

            logMock.Setup(x => x.InfoFormat("Movie with id {0} updated name from {1} to {2}!", 1, "Star Wars", "Lord of the Rings"));
            contextMock.Setup(x => x.Movies.Find(1)).Returns(movieToChange);
            contextMock.Setup(x => x.SaveChanges());

            repository.UpdateMovie(new Movie(){Id = 1, Name = "Lord of the Rings"});

            logMock.VerifyAll();
            contextMock.VerifyAll();
            // TODO: need to verify that the name was actually changed
        }

        [Test]
        public void Log_When_Movie_Is_Deleted_And_Send_An_Email()
        {
            var contextMock = new Mock<MovieContext>();
            var logMock = new Mock<ILog>();
            var mailerMock = new Mock<IMailer>();
            var repository = new MovieRepository(contextMock.Object, logMock.Object, mailerMock.Object);
            var movieToDelete = new Movie() { Id = 1, Name = "Star Wars" };

            logMock.Setup(x => x.InfoFormat("Movie with id {0} and name {1} was deleted!", 1, "Star Wars"));
            contextMock.Setup(x => x.Movies.Find(1)).Returns(movieToDelete);
            contextMock.Setup(x => x.Movies.Remove(movieToDelete));
            contextMock.Setup(x => x.SaveChanges());
            mailerMock.Setup(x => x.SendEmail("from@someplace.com", "to@someotherplace.com", "Movie with name Star Wars was deleted!"));

            repository.DeleteMovie(1);

            logMock.VerifyAll();
            contextMock.VerifyAll();
        }
    }
}
