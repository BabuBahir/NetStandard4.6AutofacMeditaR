using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Owin.Hosting.Services;
using Sample4._6Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sample4._6Api.Controllers
{
    public class HomeController : BaseController
    { 
        public async Task<ViewResult> Index()
        {  
            var result = await _mediator.Send(new GetSampleDataQuery());
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
