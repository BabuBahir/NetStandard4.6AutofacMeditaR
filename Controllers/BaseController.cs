using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample4._6Api.Controllers
{
    public class BaseController : Controller
    {
        public IMediator _mediator;
        public BaseController()
        {
            var builder = new ContainerBuilder();
            // this will add all your Request- and Notificationhandler
            // that are located in the same project as your program-class
            builder.AddMediatR(this.GetType().Assembly);

            var container = builder.Build();

            _mediator = container.Resolve<IMediator>();
        } 
    }
}