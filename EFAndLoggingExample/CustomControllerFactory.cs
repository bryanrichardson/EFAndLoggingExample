using System;
using System.Web.Mvc;
using System.Web.Routing;
using EFAndLoggingExample.Controllers;
using EFAndLoggingExample.Mail;
using EFAndLoggingExample.Models;
using EFAndLoggingExample.Repositories;
using log4net;

namespace EFAndLoggingExample
{
    public class CustomControllerFactory: DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // Ideally we'd use a dependency injection framework such as Castle Windsor here but to 
            // keep it simple I just assume we are loading the HomeController and setting the 
            // MovieRepository as the dependency for the controller.
            IMovieRepository repository = new MovieRepository(new MovieContext(), LogManager.GetLogger(typeof(HomeController)), new DefaultMailer());
            IController controller = Activator.CreateInstance(controllerType, new object[] {repository}) as Controller;
            return controller;
        }
    }
}