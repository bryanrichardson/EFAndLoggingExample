using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
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

            repository.InsertMovie("Star Wars");

            logMock.Verify(x => x.InfoFormat("Movie with name {0} created!", "Star Wars"));
        }
    }
}
